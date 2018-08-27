using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StageManager : Manager {

    private delegate void ButtonDelegate<T>(T value);
    private ButtonDelegate<int> methodOnClick;
    private readonly DefaultControls.Resources uiResources = new DefaultControls.Resources();

    // colors
    private Color selectedColor;
    private Color defaultColor;

    // Attributes
    private int currStage;
    private Dictionary<int, int> enemyTypesAtStage;
    private Dictionary<int, int> bossTypesAtStage;
    private Dictionary<int, string> descriptions;

    // UI
    private GameObject parent;
    private Dictionary<int, GameObject> stages;
    private GameObject description;

    // Use this for initialization
    void Start () {
        // Manager
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        // Colors
        selectedColor = Color.white;
        defaultColor = Color.gray;

        // UI
        parent = GameObject.Find("StageTransition");
        stages = new Dictionary<int, GameObject>();

        // Callbacks
        methodOnClick = OnStageClick;

        // Attributes
        currStage = 0; 
        enemyTypesAtStage = new Dictionary<int, int>();
        // <stage + 1,prefab index>
        // Add young master at stage 1
        enemyTypesAtStage.Add(0, 0);
        // Add ninja at stage 2
        enemyTypesAtStage.Add(1, 2);
        bossTypesAtStage = new Dictionary<int, int>();
        // temp... maybe find a better way to refactor this to be more abstract TODO
        descriptions = new Dictionary<int, string>();
        descriptions.Add(0, "This is stage one. Full of arrogant young masters that need some face slapping. Time to teach them some lessons!!");
        descriptions.Add(1, "The arrogant young masters hired some ninja. These ninja are expert in melee combats; Don't get to close.");
    }

    private void OnStageClick(int stage)
    {
        stages[currStage].GetComponent<Image>().color = defaultColor;
        stages[stage].GetComponent<Image>().color = selectedColor;
        currStage = stage;
        UpdateDescription();
    }
	
    public void RenderStages()
    {
        var left = 50;
        var right = -800;
        var numOfStages = levelManager.getNumOfStages();
        for (var i = 0; i < numOfStages; i++)
        {
            var panel = CreateUIPanelWithFunction<int>(left, -20, right, 410, parent, i, methodOnClick);
            var text = CreateUIText("Stage-" + (i + 1), 14, 50, 0, panel);
            stages.Add(i, panel);
            left += 150;
            right += 150;
        }
    }

    public void RenderDescription()
    {
        var currentDescription = descriptions[currStage];
        var panel = CreateUIPanel(50, -150, -50, 75, parent);
        var verticalGroup = panel.AddComponent<VerticalLayoutGroup>();
        verticalGroup.childControlHeight = true;
        verticalGroup.childControlWidth = true;
        verticalGroup.childForceExpandHeight = true;
        verticalGroup.childForceExpandWidth = true;
        var text = CreateUIText(currentDescription, 16, 0, 0, panel);
        text.GetComponent<Text>().color = Color.white;

        description = panel;
    }

    private void UpdateDescription()
    {
        var text = description.transform.Find("Text");
        text.GetComponent<Text>().text = descriptions[currStage];
    }
    
    private GameObject CreateUIText(string value, int fontsize, float xPos, float yPos, GameObject contentHolder)
    {
        var uiTextElement = DefaultControls.CreateText(uiResources);
        uiTextElement.transform.SetParent(contentHolder.transform, false);
        uiTextElement.GetComponent<RectTransform>().localPosition = new Vector2(xPos, yPos);
        uiTextElement.GetComponent<Text>().text = value;
        uiTextElement.GetComponent<Text>().fontSize = fontsize;
        return uiTextElement;
    }

    private GameObject CreateUIPanel(float left, float top, float right, float bottom, GameObject contentHolder)
    {
        var uiPanel = DefaultControls.CreatePanel(uiResources);
        uiPanel.transform.SetParent(contentHolder.transform, false);

        uiPanel.GetComponent<RectTransform>().offsetMin = new Vector2(left, bottom);//left-bottom;
        uiPanel.GetComponent<RectTransform>().offsetMax = new Vector2(right, top);//right-top

        uiPanel.GetComponent<Image>().color = defaultColor;
        return uiPanel;
    }

    private GameObject CreateUIPanelWithFunction<T>(float left, float top, float right, float bottom, GameObject contentHolder, T value, ButtonDelegate<T> func)
    {
        var uiPanel = CreateUIPanel(left, top, right, bottom, contentHolder);
        var button = uiPanel.AddComponent<Button>();
        button.onClick.AddListener(() => func(value));
        var colorBlock = button.colors;
        button.colors = colorBlock;
        return uiPanel;
    }

    public int[] getEnemyBasedOnStage()
    {
        return enemyTypesAtStage.Where(e => e.Key <= currStage).Select(e => e.Value).ToArray();
    }

    public int[] getBossBasedOnStage()
    {
        return bossTypesAtStage.Where(e => e.Key == currStage).Select(e => e.Value).ToArray();
    }

    public int getCurrentStage()
    {
        return currStage;
    }
}
