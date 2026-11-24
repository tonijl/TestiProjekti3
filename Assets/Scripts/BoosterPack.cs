using UnityEngine;
using System.Collections;

public class BoosterPack : MonoBehaviour
{

    public int timer = 1500;
    public int boostDuration = 750;
    public int boostPower = 2; // doubles firerate and reload speed

 


       void FixedUpdate()
        {
  
            timer--;
            if(timer <= 0)
            {
			    Destroy(gameObject);
            }
        }
    

	private void OnTriggerEnter2D(Collider2D other)
	{
		

		if(other.CompareTag("Player") == true)
		{

            // GunShoot shooting = other.GetComponentInChildren<GunShoot>();

            WeaponBase weapon = other.GetComponentInChildren<WeaponBase>();

            if (weapon != null)
            {
                weapon.FireRateBoost(boostDuration, boostPower);
            }


            /*

            PistolScript shooting = other.GetComponentInChildren<PistolScript>();

            if (shooting != null)
            {
                shooting.FireRateBoost(boostDuration, boostPower);
            }
            */






            Destroy(gameObject);

		}
		
	}
}
