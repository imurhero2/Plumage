using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private void FixedUpdate()
    {
        if (PlayerManager.featherCount == 5 || PlayerManager.health == 0)
        {
            // Level Complete / Game Over
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Quitting Application");
            Application.Quit();
        }
    }

}
