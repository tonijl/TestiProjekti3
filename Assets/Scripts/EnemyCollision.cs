using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

	public int damage = 25;
	public float collisionDelay = 2f;
	private float collisionTimer = 0f;
	//private int collisionDelay = 100;
	//private int collisionTimer = 0; 
	
	/*void FixedUpdate()
	{
		collisionTimer--;
	}*/

	void Update()
	{
		if(collisionTimer > 0)
		{
			collisionTimer -= Time.deltaTime;
		}
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {

		DealDamage(collision);
       /* if (collisionTimer <= 0)
        {

            playerScript player = collision.gameObject.GetComponent<playerScript>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }

            collisionTimer = collisionDelay;
        }*/


    }



    //private void OnCollisionEnter2D(Collision2D collision)
    private void OnCollisionStay2D(Collision2D collision)
	{

        DealDamage(collision);

    }


	private void DealDamage(Collision2D collision)
	{
		if(collisionTimer > 0)
		{
			return;
		}

		playerScript player = collision.gameObject.GetComponent<playerScript>();

		if (player != null)
		{
			player.TakeDamage(damage);
			collisionTimer = collisionDelay;
		}

	}



}
