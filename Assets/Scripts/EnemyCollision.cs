using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

	public int damage = 25;
	private int collisionDelay = 100;
	private int collisionTimer = 0; 
	
	void FixedUpdate()
	{
		collisionTimer--;
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionTimer <= 0)
        {

            playerScript player = collision.gameObject.GetComponent<playerScript>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }

            collisionTimer = collisionDelay;
        }


    }



    //private void OnCollisionEnter2D(Collision2D collision)
    private void OnCollisionStay2D(Collision2D collision)
	{
		if(collisionTimer <= 0){
		
			playerScript player = collision.gameObject.GetComponent<playerScript>();
			
			if(player != null)
			{
				player.TakeDamage(damage);
			}

			collisionTimer = collisionDelay;
		}
		
		
	}

}
