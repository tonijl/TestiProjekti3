using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using System.Collections;


public class EnemyHealth : MonoBehaviour
{

	public Light2D hitLight;
	public float lightIntensity = 2f;
	public float lightDuration = 0.3f;
	private Coroutine lightCoroutine;
	
	[Header("Health")]
	public float maxHealth = 100;
	private float currentHealth;
	private EnemyDamageFlash damageFlash;
	
	[Header("Healt Bar")]
	public Slider healthBar;
	public GameObject healthBarObject;
	public Vector3 healthBarOffset = new Vector3(0, 0.8f, 0);
	
	private bool scoreGiven = false;

	public GameObject healthPackPrefab;
	public GameObject boosterPackPrefab;

	public AudioSource enemyAudio;

	public AudioClip deathSound;
	public AudioClip hitSound;

	public float hitSoundCooldown = 0.2f;
	private float nextHitSoundTime = 0f;
	
	
	
	void Awake()
	{
		damageFlash = GetComponent<EnemyDamageFlash>();
	}
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
		
		if(healthBar != null)
		{
			healthBar.maxValue = maxHealth;
			healthBar.value = currentHealth;
		}
		
		if(healthBarObject != null)
		{
			healthBarObject.SetActive(false);
		}
		
		
    }
	
	public void TakeDamage(float damage)
	{

		if(lightCoroutine != null)
		{
			StopCoroutine(lightCoroutine);
		}

		lightCoroutine = StartCoroutine(FlashLight());




		if(Time.time >= nextHitSoundTime)
		{

			AudioManager.Instance.PlaySFX(hitSound, this.gameObject, 1f, 0.9f, 1.1f);
			nextHitSoundTime = Time.time + hitSoundCooldown;

		}

		currentHealth -= damage;
		
		
		if(healthBar != null)
		{
			healthBar.value = currentHealth;
		}
		
		if(healthBarObject != null)
		{
			healthBarObject.SetActive(currentHealth < maxHealth);
		}
		
		
		if(damageFlash != null)
		{
			damageFlash.FlashRed();
		}
		
		
		
		//Debug.Log("EnemyHealth: "+currentHealth);
		
		if(currentHealth <= 0)
		{
			Die();
		}
		
	}


	IEnumerator FlashLight()
	{
		hitLight.intensity = lightIntensity;
		float elapsed = 0f;

		while (elapsed < lightDuration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / lightDuration;

			hitLight.intensity = Mathf.Lerp(
				lightIntensity,
				0f,
				t
			);

			yield return null;

		}

		hitLight.intensity = 0f;
		lightCoroutine = null;



	}
	
	private void LateUpdate()
	{
		if(healthBarObject != null)
		{
			healthBarObject.transform.position = transform.position + healthBarOffset;
			
			healthBarObject.transform.rotation = Quaternion.identity;
		}
	}
	
	public void Heal(float amount)
	{
		currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
		
		if (healthBar != null)
		{
			healthBar.value = currentHealth;
		}
		
		if (healthBarObject != null)
		{
			healthBarObject.SetActive(currentHealth < maxHealth);
		}
		
	}
	
	void Die()
	{


		//enemyAudio.pitch = Random.Range(0.9f, 1.1f);
		//AudioSource.PlayClipAtPoint(deathSound, transform.position);

		AudioManager.Instance.PlaySFX(deathSound, this.gameObject, 1f, 0.9f, 1.1f);
	
		
		EnemyShatterEffect shatter = GetComponent<EnemyShatterEffect>();
		
		if(shatter != null)
		{
			shatter.Shatter();
		}
		else
		{
			
			Destroy(gameObject);
		}
		
		if(scoreGiven == false){
			ScoreManager.instance.AddScore(10); //1
			scoreGiven = true;


		// 20% changce to drop powerup
			if(Random.value <= 0.2f){


					if(Random.value <= 0.5f){

						GameObject healthPack = Instantiate(
							healthPackPrefab,
							transform.position,
							Quaternion.identity
						);

					}
					else
					{
						GameObject boosterPack = Instantiate(
							boosterPackPrefab,
							transform.position,
							Quaternion.identity
						);
					}


			}




		}
		//Destroy(gameObject);
	}

}
