using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerScript : MonoBehaviour
{

	[Header("Health")]
	public float maxHealth = 3;
	private float currentHealth;
	public Slider healthBar;
	
	[Header("Invincibility Frames")]
	public float invincibilityDuration = 1f;
	private bool isInvincible = false;
	
	[Header("Flashing Effect")]
	public float flashInterval = 0.1f;
	private SpriteRenderer spriteRenderer;

	public Camera mainCamera;

	public GameObject minigunObject;


	public float movementSpeed = 5f;

	public float minMovementMultiplier = 0.5f;
	public float chargeLevel = 1f;

	private Rigidbody2D rb;
	private Vector2 movement;

	public AudioClip getHitSound;
	public AudioClip healSound;
	public AudioClip deathSound;

	public PlayerDash playerDash;

	[SerializeField] private CameraShake cameraShake;








	
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

		mainCamera = Camera.main;

        currentHealth = maxHealth;
		
		if(healthBar != null)
		{
			healthBar.maxValue = maxHealth;
			healthBar.value = currentHealth;
		}

		spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	public void TakeDamage(float damage)
	{
		if((isInvincible || playerDash.IsInvulnerable) && damage > 0)
		{
			return;
		}
		
		

		

		currentHealth -= damage;
		//Debug.Log("Player health: "+ currentHealth);

		if(currentHealth>maxHealth)
		{
			currentHealth = maxHealth;
		}

		
		if(healthBar != null)
		{
			healthBar.value = currentHealth;
		}
		
		
		if(currentHealth <= 0)
		{
			Die();
			return;
		}
		

		if(damage>0){
			AudioManager.Instance.PlaySFX(getHitSound, this.gameObject, 1f, 0.9f, 1.1f);
			cameraShake.Shake(0.2f, 0.15f);
			StartCoroutine(InvincibilityFrames());
		}
		else
		{
			AudioManager.Instance.PlaySFX(healSound, this.gameObject, 1f, 0.9f, 1.1f);
		}
		
	}
	
	private IEnumerator InvincibilityFrames()
	{
		isInvincible = true;
		
		float elapsed = 0f;
		
		while (elapsed < invincibilityDuration)
		{
			if(spriteRenderer != null)
			{
				spriteRenderer.enabled = !spriteRenderer.enabled;
			}
				
			yield return new WaitForSeconds(flashInterval);
			elapsed += flashInterval;
		}
		
		if(spriteRenderer!= null)
		{
			spriteRenderer.enabled = true;
		}
		
		isInvincible = false;
		
	}
		
	private void Die()
	{



		AudioManager.Instance.PlaySFX(deathSound, this.gameObject, 1f, 0.9f, 1.1f);
	
		
		EnemyShatterEffect shatter = GetComponent<EnemyShatterEffect>();
		
		if(shatter != null)
		{
			shatter.Shatter();
			//StartCoroutine(deathDelay());
			
		}
		else
		{
			
			Destroy(gameObject);
		}

		FindFirstObjectByType<GameManager>().GameOver();	
	}

	/*private IEnumerator deathDelay()
	{

		yield return new WaitForSeconds(2f);

		FindFirstObjectByType<GameManager>().GameOver();	
	}*/
	
	

    // Update is called once per frame
    void Update()
    {

		Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		if(mousePosition.x < transform.position.x)
		{
			//Debug.Log("flipX = true");
			spriteRenderer.flipX = true;
		}
		else
		{
			//Debug.Log("flipX = false");
			spriteRenderer.flipX = false;
		}




        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
		
       //FixedUpdate();

		if(playerDash.IsDashing())
		{
			return;
		}




		//rb.linearVelocity = movement * movementSpeed;

    }
	
	void FixedUpdate()
	{


        //if (otherObject.activeInHierarchy)
        if (minigunObject.activeInHierarchy == false)
        {
            chargeLevel = 1f;
            Debug.Log("Object is active in the scene!");
        }



        if (playerDash.IsDashing())
		{
			return;
		}

		float normalizedCharge = Mathf.InverseLerp(1f, 4f, chargeLevel);

		float currentSpeed = movementSpeed * Mathf.Lerp(1f, minMovementMultiplier, normalizedCharge);

		rb.linearVelocity = movement * currentSpeed;
	}
	
	
}
