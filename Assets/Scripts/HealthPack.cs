using UnityEngine;

public class HealthPack : MonoBehaviour
{

    public int timer = 1500;
    public int healthAmount = 25;

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

		playerScript player = other.gameObject.GetComponent<playerScript>();
		
		if(player != null)
		{
			 player.TakeDamage(-healthAmount);
		}

           

			/*ScoreManager.instance.AddScore(1);
			
			CoinSpawner spawner = FindFirstObjectByType<CoinSpawner>();
			
			if(spawner != null)
			{
				spawner.SpawnCoin();
			}*/
			
			
			Destroy(gameObject);

		}
		
	}

}
