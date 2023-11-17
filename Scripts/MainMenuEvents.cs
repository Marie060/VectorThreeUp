using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour {
    private void Awake() {
        Time.timeScale = 1.0f;
    }

    public void QuitGame() {
        Debug.Log("Quitting game . . .");
        Application.Quit();
    }

    public void ChangeScene(string sceneName) {
        if (GameObject.Find("LoadingText")) {
            var l = GameObject.Find("LoadingText");
            l.SetActive(true);
        }
        
        SceneManager.LoadScene(sceneName);
    }
}