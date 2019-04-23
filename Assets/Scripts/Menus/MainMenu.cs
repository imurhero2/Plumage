using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // To use this script you should have an empty game object in the scene, call it button manager or the like.
    // Throw this script on the empty gameobject and then whatever UI element (buttons etc.) throw it inside the On Click()

    public Text highScoreText;

    public void Start()
    {
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("FeatherHighscore").ToString();
    }

    public void QuitGame()
    {
        Debug.Log("Game Quits");
        Application.Quit();
    }

    public void NewGame ()
    {
        PlayerManager.featherCount = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
