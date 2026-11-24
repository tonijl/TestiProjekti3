using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RocketLauncherScript : WeaponBase
{

    public Transform firePoint;
    public TMP_Text weaponText;

    public GameObject rocketPrefab;


    public AudioClip rocketSound;

    public AudioClip reloadSound;

   // public AudioClip powerUpSound;


    public float bulletSpeed = 15f;
    public float firerate = 10f;
   // public int weaponType = 1; // 1 = pistol, 2 = shotgun, 3 = rocket launcher

    public int maxAmmo = 12;
    public float reloadTime = 100f;
    public Slider ammoBar;

    private int currentAmmo = 12;

    private int currentRocketAmmo = 4;

    private float reloadTimer = 0f;
    private float shootDelay = 10f;
    private float spreadAngle = 20f;





    void Start()
    {

      /*  if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }

        if (timerBar != null)
        {
            timerBar.gameObject.SetActive(false);
        }*/


                //rocket launcher
                currentAmmo = 4;
                maxAmmo = 4;
                shootDelay = 30f;
                firerate = 30f;
                bulletSpeed = 15f;
                reloadTime = 100f;
                if (weaponText != null)
                {
                    weaponText.text = "Weapon: Rocket Launcher";
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

       /* if (Input.GetKeyDown(KeyCode.Q))
        {

            ChangeWeapon();

        }*/



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

        weaponText.text = "Weapon: Rocket Launcher";
        ammoBar.maxValue = maxAmmo;

        if (currentAmmo > 0 && shootDelay <= 0)
        {

            if (Input.GetMouseButton(0))
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
        // shootDelay -= 1 * boostPower;
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
            AudioManager.Instance.PlaySFX(reloadSound, transform.position, 1f, 0.9f, 1.1f);
        }

        if (reloadTimer == 0 || reloadTimer == -1)
        {
            currentAmmo = maxAmmo;
            reloadTimer = -2;
        }



       /* boostDuration--;
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
        }*/


    }

    /*public void FireRateBoost(int duration, int multiplier)
    {

        AudioManager.Instance.PlaySFX(powerUpSound, transform.position, 1f, 0.9f, 1.1f);

        boostDuration = duration;
        boostPower = multiplier;

        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.red;
        }

        if (timerBar != null)
        {
            timerBar.gameObject.SetActive(true);
            timerBar.maxValue = boostDuration;
        }
    }*/


    private void Reload()
    {
        currentAmmo = 0;
        reloadTimer = reloadTime;
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;
        return direction;
    }

    /*private void ChangeWeapon()
    {

        reloadTimer = -1;

        if (weaponType < 3)
        {
            weaponType++;
        }
        else
        {
            weaponType = 1;
        }

        switch (weaponType)
        {
            case 1:

                currentRocketAmmo = currentAmmo;

                currentAmmo = currentPistolAmmo;

                //pistol
                //currentAmmo = 0;
                maxAmmo = 12;
                shootDelay = 10f;
                firerate = 10f;
                bulletSpeed = 15f;
                reloadTime = 100f;
                if (weaponText != null)
                {
                    weaponText.text = "Weapon: Pistol";
                }
                break;
            case 2:

                currentPistolAmmo = currentAmmo;
                currentAmmo = currentShotgunAmmo;

                //shotgun
                //currentAmmo = 0;
                maxAmmo = 6;
                shootDelay = 30f;
                firerate = 30f;
                bulletSpeed = 15f;
                reloadTime = 100f;
                if (weaponText != null)
                {
                    weaponText.text = "Weapon: Shotgun";
                }
                break;
            case 3:

                currentShotgunAmmo = currentAmmo;
                currentAmmo = currentRocketAmmo;

                //rocket launcher
                //currentAmmo = 4;
                maxAmmo = 4;
                shootDelay = 30f;
                firerate = 30f;
                bulletSpeed = 15f;
                reloadTime = 100f;
                if (weaponText != null)
                {
                    weaponText.text = "Weapon: Rocket Launcher";
                }
                break;

        }


        if (ammoBar != null)
        {
            ammoBar.maxValue = maxAmmo;
            ammoBar.value = currentAmmo;
        }

        if (currentAmmo == 0)
        {
            Reload();
        }
        //Reload();


    }*/


    void Shoot()
    {

                //rocket


                AudioManager.Instance.PlaySFX(rocketSound, transform.position, 1f, 0.9f, 1.1f);

                GameObject bullet3 = Instantiate(
                    rocketPrefab,
                    firePoint.position,
                    firePoint.rotation
                );

                Vector2 direction3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;

                direction3.Normalize();

                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.linearVelocity = direction3 * bulletSpeed;






    }

}
