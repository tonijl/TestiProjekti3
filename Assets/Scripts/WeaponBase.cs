using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBase : MonoBehaviour
{
    public PlayerBoost playerBoost;
    //public int boostDuration = 0;
    //public int boostPower = 1;

    public AudioClip powerUpSound;

   /* [Header("Power Up UI")]
    public Slider timerBar;*/

  /*  [Header("Player Appearance")]
    public Renderer playerRenderer;
    public Color originalColor;*/


    void Start()
    {
       /* if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }

        if (timerBar != null)
        {
            timerBar.gameObject.SetActive(false);
        }*/
    }

    public void FireRateBoost(int duration, int multiplier)
    {

        Debug.Log("FireRateBoostissa...");

        AudioManager.Instance.PlaySFX(powerUpSound, transform.position, 1f, 0.9f, 1.1f);

        playerBoost.FireRateBoost(duration, multiplier);

       /* boostDuration = duration;
        boostPower = multiplier;

        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.red;
        }

        if (timerBar != null)
        {

            timerBar.gameObject.SetActive(true);
            timerBar.maxValue = boostDuration;

        }*/
    }

    /*
    void FixedUpdate()
    {


        


        boostDuration--;
        if (boostDuration == 0)
        {
            boostPower = 1;

            if (playerRenderer != null)
            {
                playerRenderer.material.color = originalColor;
            }

            if (timerBar != null)
            {
                timerBar.gameObject.SetActive(false);
            }

        }
        else
        {
            if (timerBar != null)
            {
 
                timerBar.value = boostDuration;
            }
        }
    }

    void Update()
    {
        if (timerBar != null)
        {
            if (boostDuration > 0)
            {
                timerBar.gameObject.SetActive(true);
            }
            else
            {
                timerBar.gameObject.SetActive(false);
            }
        }
    }*/


}
