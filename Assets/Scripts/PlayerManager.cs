using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public int startingHealth;
    public static int health;
    public static int featherCount;
    public Text featherText;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Sprite emptyHeart;
    public Sprite fullHeart;


    void Start()
    {
        health = startingHealth;
        featherCount = 0;
        featherText.text = "x" + featherCount;
    }

    private void ModifyHealth(int modifier)
    {
        health += modifier;
        Debug.Log(health);

        if (health >= 1)
        {
            heart1.sprite = fullHeart;
        }
        else
        {
            heart1.sprite = emptyHeart;
        }

        if (health >= 2)
        {
            heart2.sprite = fullHeart;
        }
        else
        {
            heart2.sprite = emptyHeart;
        }

        if (health >= 3)
        {
            heart3.sprite = fullHeart;
        }
        else
        {
            heart3.sprite = emptyHeart;
        }
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
            featherText.text = "x" + featherCount.ToString();
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
