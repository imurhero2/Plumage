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

    public void NewGame (string Level)
    {
        StartCoroutine("Restart");
        Time.timeScale = 1f;
    }

    IEnumerator Restart()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        // change this to whatever string the level 1 is called
        SceneManager.LoadScene("Level1");
    }
}
