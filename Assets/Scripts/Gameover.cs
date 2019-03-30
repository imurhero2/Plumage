using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
       public void Reset(string SampleScene)
        {
        StartCoroutine(LoadYourAsyncScene());
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Should load scene");
        }
    
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
