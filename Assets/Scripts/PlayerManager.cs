using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public int startingHealth;
    public static int health;
    public static int featherCount;
    public Text healthText;
    public Text featherText;

    void Start()
    {
        health = startingHealth;
        healthText.text = "Health: " + health;
        featherText.text = "Feathers: " + featherCount;
    }

    private void ModifyHealth(int modifier)
    {
        health += modifier;
        healthText.text = "Health: " + health;
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
                // Trigger game over
                Debug.Log("Game Over Scrub!");
            }
        }

        if (other.tag == "Feather")
        {
            featherCount += 1;
            Destroy(other.gameObject);
            featherText.text = "Feathers: " + featherCount;
        }
    }
}
