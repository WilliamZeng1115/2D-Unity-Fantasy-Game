using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private GameObject spawnPoint;

    // can load scene with data and pass it to new scene
    public void LoadLevel(string name) {
        SceneManager.LoadScene(name);
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    public void LoadLevelAdditive(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }


	public void QuitRequest(){
		Application.Quit();
	}

    public GameObject getSpawnPoint()
    {
        return spawnPoint;
    }

}
