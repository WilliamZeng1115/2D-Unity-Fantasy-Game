using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    // can load scene with data and pass it to new scene
    public void LoadLevel(string name) {
        SceneManager.LoadScene(name);
    }

    public void LoadLevelAdditive(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }


	public void QuitRequest(){
		Application.Quit();
	}

}
