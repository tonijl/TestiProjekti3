using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MinigunScript : WeaponBase
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public TMP_Text weaponText;

    public GameObject muzzleFlashPrefab;


    public AudioClip pistolSound;


    public AudioClip reloadSound;

    //public AudioClip powerUpSound;


    public float bulletSpeed = 15f;
    public float firerate = 20f; // reduced to 5f when firing
    private float chargeLevel = 1; // 1 - 4
    // public int weaponType = 1; // 1 = pistol, 2 = shotgun, 3 = rocket launcher

    public int maxAmmo = 50;
    public float reloadTime = 100f;
    public Slider ammoBar;

    private int currentAmmo = 50;
 


    private float reloadTimer = 0f;
    private float shootDelay = 10f;

    public playerScript playerMovement;






    void Start()
    {


        //pistol
        currentAmmo = 50;
        maxAmmo = 50;
        shootDelay = 10f;
        firerate = 20f;
        bulletSpeed = 15f;
        reloadTime = 100f;
        if (weaponText != null)
        {
            weaponText.text = "Weapon: Minigun";
        }


        currentAmmo = maxAmmo;

        if (ammoBar != null)
        {
            ammoBar.maxValue = maxAmmo;
            ammoBar.value = currentAmmo;
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentAmmo < maxAmmo)
            {
                Reload();
            }

        }



        if (currentAmmo > 0 && shootDelay <= 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                currentAmmo--;
                shootDelay = firerate;
                ammoBar.value = currentAmmo;
            }



            if (currentAmmo == 0)
            {
                Reload();
            }

        }

    }


    void FixedUpdate()
    {

        //Debug.Log("maxAmmo: " + maxAmmo.ToString());

        weaponText.text = "Weapon: Minigun";
        ammoBar.maxValue = maxAmmo;


        if (Input.GetMouseButton(0))
        {
            if(currentAmmo > 0)
            {
                chargeLevel += 0.025f;
          

                Debug.Log("chargeLevel: " + chargeLevel.ToString());

                if (chargeLevel > 4)
                {
                    chargeLevel = 4;
                }
  

            }
            else
            {
                chargeLevel -= 0.1f;
                if (chargeLevel < 1)
                {
                    chargeLevel = 1;
                }
            }
        }
        else
        {
            chargeLevel -= 0.1f;
            if (chargeLevel < 1)
            {
                chargeLevel = 1;
            }
        }

        playerMovement.chargeLevel = chargeLevel;

        if (currentAmmo > 0 && shootDelay <= 0)
        {



            if (Input.GetMouseButton(0))
            {
                Shoot();
                currentAmmo--;
                //shootDelay = firerate;
                shootDelay = firerate / chargeLevel;
                ammoBar.value = currentAmmo;
            }



            if (currentAmmo == 0)
            {
                Reload();
            }

        }
        
        shootDelay -= 1 * playerBoost.boostPower;



        if (reloadTimer > 0 && currentAmmo == 0)
        {
            ammoBar.value = (1 - (reloadTimer / reloadTime)) * maxAmmo;
        }
        else
        {
            ammoBar.value = currentAmmo;
        }

        reloadTimer -= 1 * playerBoost.boostPower;


        if (reloadTimer == (int)(reloadTime / 2))
        {
            AudioManager.Instance.PlaySFX(reloadSound, this.gameObject, 1f, 0.9f, 1.1f);
        }

        if (reloadTimer == 0 || reloadTimer == -1)
        {
            currentAmmo = maxAmmo;
            reloadTimer = -2;
        }





    }



    private void Reload()
    {
        currentAmmo = 0;
        reloadTimer = reloadTime;
        //Debug.Log("reloadTimer: "+reloadTimer.ToString());
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;
        return direction;
    }



    void Shoot()
    {

        float randomRotation = Random.Range(-5f*chargeLevel, 5f*chargeLevel);

        Quaternion flashRotation = firePoint.rotation * Quaternion.Euler(0, 0, randomRotation);


        GameObject flash = Instantiate(
            muzzleFlashPrefab,
            firePoint.position,
            flashRotation
            );

        MuzzleFlashFollow follow = flash.GetComponent<MuzzleFlashFollow>();

        if (follow != null)
        {
            follow.firePoint = firePoint;
        }


        Destroy(flash, 0.2f);


        AudioManager.Instance.PlaySFX(pistolSound, this.gameObject, 1f, 0.9f, 1.1f);


        GameObject bullet2 = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;

        direction.Normalize();

        direction = Quaternion.Euler(0, 0, randomRotation) * direction;

        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.linearVelocity = direction * bulletSpeed;


    }

}
