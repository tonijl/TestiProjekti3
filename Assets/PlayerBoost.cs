using UnityEngine;
using UnityEngine.UI;

public class PlayerBoost : MonoBehaviour
{
    public Renderer playerRenderer;
    public Slider timerBar;

    private Color originalColor;

    public int boostDuration = 0;
    public int boostPower = 1;

    void Start()
    {
        originalColor = playerRenderer.material.color;
        timerBar.gameObject.SetActive(false);
    }

    public void FireRateBoost(int duration, int multiplier)
    {
        boostDuration = duration;
        boostPower = multiplier;

        playerRenderer.material.color = Color.red;

        timerBar.gameObject.SetActive(true);
        timerBar.maxValue = duration;
        timerBar.value = duration;
    }

    void FixedUpdate()
    {
        if (boostDuration > 0)
        {
            boostDuration -= 1; // See timing note below

            timerBar.value = boostDuration;

            if (boostDuration <= 0)
            {
                boostDuration = 0;
                boostPower = 1;

                playerRenderer.material.color = originalColor;
                timerBar.gameObject.SetActive(false);
            }
        }
    }
}