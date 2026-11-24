using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class LaserWeapon : WeaponBase
{


    public TMP_Text weaponText;
    public AudioClip LaserSound;
    public AudioClip reloadSound;
    //public AudioClip powerUpSound;

    public int maxAmmo = 12;
    public float reloadTime = 100f;
    public Slider ammoBar;

    private float currentAmmo = 100;
    private float currentLaserAmmo = 100;

    private float reloadTimer = 0f;



    [Header("Laser Settings")]
    public Transform laserOrigin;
    public float laserRange = 30f;
    public float damagePerSecond = 50f;

    [Header("References")]
    public LayerMask hitLayers;
    public Camera mainCamera;

    private LineRenderer lineRenderer;
    private bool isFiring;


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

        //shotgun
        currentAmmo = 100;
        maxAmmo = 100;
        //shootDelay = 30f;
        //firerate = 30f;
        laserRange = 30f;
        damagePerSecond = 50f;

        reloadTime = 100f;
        if (weaponText != null)
        {
            weaponText.text = "Weapon: Laser";
        }


        currentAmmo = maxAmmo;

        if (ammoBar != null)
        {
            ammoBar.maxValue = maxAmmo;
            ammoBar.value = currentAmmo;
        }


    }

    void Awake()
    {

        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        lineRenderer.startWidth = 0.06f;
        lineRenderer.endWidth = 0.06f;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
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



        if(currentAmmo > 0)
        {
            if (Input.GetMouseButton(0))
            {
                ShootLaser();
                //currentAmmo--;
                currentAmmo -= (10 * Time.deltaTime);

                if (currentAmmo<0)
                {
                    currentAmmo = 0;
                }

                ammoBar.value = currentAmmo;


                if (currentAmmo == 0)
                {
                    Reload();
                }

            }
            else
            {
                StopLaser();
            }

        }




    }

    void FixedUpdate()
    {
        ammoBar.maxValue = maxAmmo;
        weaponText.text = "Weapon: Laser";


        Debug.Log("reloadTimer: " + reloadTimer.ToString());
       Debug.Log("boostPower: " + playerBoost.boostPower.ToString());

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

   /* public void FireRateBoost(int duration, int multiplier)
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
        Vector2 direction = (mousePosition - laserOrigin.position).normalized;
        return direction;
    }

    void ShootLaser()
    {
        lineRenderer.enabled = true;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 start = laserOrigin.position;
        Vector2 direction = ((Vector2)mousePosition - start).normalized;

        Vector2 end = start + direction * laserRange;

        RaycastHit2D hit = Physics2D.Raycast(
            start,
            direction,
            laserRange,
            hitLayers
        );

        if (hit.collider != null)
        {
            end = hit.point;

            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damagePerSecond * playerBoost.boostPower * Time.deltaTime);
            }

        }

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);


    }

    void StopLaser()
    {
        lineRenderer.enabled = false;
    }

    void OnDisable()
    {
        if(lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }


}
