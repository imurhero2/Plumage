using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public int startingHealth;
    public static int health;
    public static int featherCount;
    public Text healthText;
    public Text featherText;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;

    void Start()
    {
        health = startingHealth;
        featherCount = 0;
        healthText.text = "Health: " + health;
        featherText.text = "Feathers: " + featherCount;
    }

    private void ModifyHealth(int modifier)
    {
        health += modifier;
        healthText.text = "Health: " + health;
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= -20)
        {
            // Start Coroutine
            // Lock Camera
            // Game Over at end of Coroutine
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            if (health > 0)
            {
                ModifyHealth(-1);
            }
            if (health == 0)
            {
                GameOver();
            }
        }

        if (other.tag == "Feather")
        {
            featherCount += 1;
            Destroy(other.gameObject);
            featherText.text = "Feathers: " + featherCount;
            //PlayerPrefs.SetInt("Feathers", featherCount);
            if (featherCount > PlayerPrefs.GetInt("FeatherHighscore"))
            {
                PlayerPrefs.SetInt("FeatherHighscore", featherCount);
                Debug.Log("PlayerPrefs FeatherHighscore changed to: " + PlayerPrefs.GetInt("FeatherHighscore"));
            }
        }

        if (other.tag == "Finish")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            youWinPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
