using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{

    [Header("References")]
    public EnemyShatterEffect shatterEffect;
    //public GameObject gameOverScreen;

    //FindFirstObjectByType<GameManager>().GameOver();

    [Header("Settings")]
    public float gameOverDelay = 1.5f;

    private bool isDead = false;



    public void Die()
    {
        if(isDead)
        {
            return;
        }

        isDead = true;

        StartCoroutine(DeathSequence());

    }	

    IEnumerator DeathSequence()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if(script != this && script != shatterEffect)
            {
                script.enabled = false;
            }
        }


        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }

        if(shatterEffect != null)
        {
            shatterEffect.Shatter();
        }

        yield return new WaitForSeconds(gameOverDelay);

        //gameOverScreen.SetActive(true);
        FindFirstObjectByType<GameManager>().GameOver();

        Destroy(gameObject);


    }


}
