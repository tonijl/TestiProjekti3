using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShatterLightFade : MonoBehaviour
{
    public float duration = 0.7f;
    public float startIntensity = 2.5f;

    private Light2D light2D;
    private float elapsed;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        float t = Mathf.Clamp01(elapsed / duration);

        light2D.intensity = Mathf.Lerp(
            startIntensity,
            0f,
            t
            );

        if(elapsed >= duration)
        {
            Destroy(gameObject);
        }

    }

}
