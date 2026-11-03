using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GunShoot : MonoBehaviour
{
	
	public GameObject bulletPrefab;
	public Transform firePoint;
	public TMP_Text weaponText;


	public float bulletSpeed = 15f;
	public float firerate = 10f;
	public int weaponType = 2; // 1 = pistol, 2 = shotgun
	
	public int maxAmmo = 12;
	public float reloadTime = 100f;
	public Slider ammoBar;
	
	private int currentAmmo = 12;
	private float reloadTimer = 0f;
	private float shootDelay = 10f;
	private float spreadAngle = 20f;
	
	//[Header("Ammo Bar")]
	
	
	
	void Start()
	{

		switch(weaponType)
		{
			case 1:
				//pistol
				currentAmmo = 12;
				maxAmmo = 12;
				shootDelay = 10f;
				firerate = 10f;
				bulletSpeed = 15f;
				reloadTime = 100f;
				if(weaponText != null){
					weaponText.text = "Weapon: Pistol";
				}
			break;
			case 2:
				//shotgun
				currentAmmo = 6;
				maxAmmo = 6;
				shootDelay = 30f;
				firerate = 30f;
				bulletSpeed = 15f;
				reloadTime = 100f;
				if(weaponText != null){
					weaponText.text = "Weapon: Shotgun";
				}
			break;

		}


		if(weaponText != null){
			weaponText.text = "Weapon: Pistol";
		}


		 currentAmmo = maxAmmo;
		
		if(ammoBar != null)
		{
			ammoBar.maxValue = maxAmmo;
			ammoBar.value = currentAmmo;
		}
		

	}

    // Update is called once per frame
    void Update()
    {

			if(Input.GetKeyDown(KeyCode.R))
			{
				if(currentAmmo<maxAmmo)
				{
					Reload();
				}
				
			}

			if(Input.GetKeyDown(KeyCode.Q))
			{

				ChangeWeapon();

			}



			if(currentAmmo>0  && shootDelay<=0){
		
			if(Input.GetMouseButtonDown(0))
			{
				Shoot();
				currentAmmo--;
				shootDelay = firerate;
				ammoBar.value = currentAmmo;
			}
			
			
			
			if(currentAmmo == 0)
			{
				Reload();
			}
			
		}
		
    }
	
	
	void FixedUpdate()
	{


			if(currentAmmo>0 && shootDelay<=0){
		
			if(Input.GetMouseButton(0))
			{
				Shoot();
				currentAmmo--;
				shootDelay = firerate;
				ammoBar.value = currentAmmo;
			}
			
			
			
			if(currentAmmo == 0)
			{
				Reload();
			}
			
		}
			shootDelay--;



			if(reloadTimer > 0 && currentAmmo == 0)
			{
				ammoBar.value = (1 - (reloadTimer / reloadTime)) * maxAmmo;
			}
			else
			{
				ammoBar.value = currentAmmo;
			}				

			reloadTimer--;
			
			if(reloadTimer == 0)
			{
				currentAmmo = maxAmmo;
			}
		
	}
	
	
	private void Reload()
	{
		currentAmmo = 0;
		reloadTimer = reloadTime;
		Debug.Log("reloadTimer: "+reloadTimer.ToString());
	}

	Vector2 GetMouseDirection()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = (mousePosition - firePoint.position).normalized;
		return direction;
	}

	private void ChangeWeapon()
	{

		if(weaponType == 1){
			weaponType = 2;
		}else{
			weaponType = 1;
		}

			switch(weaponType)
		{
			case 1:
				//pistol
				currentAmmo = 0;
				maxAmmo = 12;
				shootDelay = 10f;
				firerate = 10f;
				bulletSpeed = 15f;
				reloadTime = 100f;
				if(weaponText != null){
					weaponText.text = "Weapon: Pistol";
				}
			break;
			case 2:
				//shotgun
				currentAmmo = 0;
				maxAmmo = 6;
				shootDelay = 30f;
				firerate = 30f;
				bulletSpeed = 15f;
				reloadTime = 100f;
				if(weaponText != null){
					weaponText.text = "Weapon: Shotgun";
				}
			break;

		}


		if(ammoBar != null)
		{
			ammoBar.maxValue = maxAmmo;
			ammoBar.value = currentAmmo;
		}

		Reload();


	}

	
	void Shoot()
	{
		
		switch(weaponType)
		{
			case 1:
				//pistol

				GameObject bullet2 = Instantiate(
					bulletPrefab,
					firePoint.position,
					firePoint.rotation
				);
				
				Vector2 direction2 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
				
				direction2.Normalize();
				
				Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
				rb2.linearVelocity = direction2 * bulletSpeed;

			break;
			case 2:
				//shotgun

				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 baseDirection = ((Vector2)mousePosition - (Vector2)firePoint.position).normalized;
				//Vector2 baseDirection = GetMouseDirection();

				for(int i = 0; i < 5; i++)
				{

				float angle = -spreadAngle / 2f + (spreadAngle / 4f) * i;

				Vector2 direction = new Vector2(
												baseDirection.x * Mathf.Cos(angle * Mathf.Deg2Rad) -
				 								baseDirection.y * Mathf.Sin(angle * Mathf.Deg2Rad),
												baseDirection.x * Mathf.Sin(angle * Mathf.Deg2Rad) +
												baseDirection.y * Mathf.Cos(angle * Mathf.Deg2Rad)
												);


				GameObject bullet = Instantiate(
					bulletPrefab,
					firePoint.position,
					Quaternion.identity
					//Quaternion.FromToRotation(Vector3.right,direction)
				);
				
				
				bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;


				Debug.DrawRay(firePoint.position, direction * 5f, Color.red, 2f);


				//Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
				//rb.linearVelocity = direction * bulletSpeed;

				}




			break;

		}

		/*GameObject bullet = Instantiate(
			bulletPrefab,
			firePoint.position,
			firePoint.rotation
		);
		
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
		
		direction.Normalize();
		
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.linearVelocity = direction * bulletSpeed;*/
		
		
		
		
	}
	
}
