using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float bulletSpeed = 12f;
    public float lifetime = 5f;
    public int damage = 35;

    public GameObject explosionPrefab;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * bulletSpeed;
        Destroy(gameObject, lifetime);
    }

    	void OnCollisionEnter2D(Collision2D collision)
	{
		
		EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
		
		if(enemy != null)
		{
			//Debug.Log("Bullet collided with enemy!");
			enemy.TakeDamage(damage);
            Explode();
			//Destroy(gameObject);
		}


		if(collision.gameObject.CompareTag("Wall"))
		{
            Explode();
			//Destroy(gameObject);
		}

	
	}

   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            return;
        }

        Explode();
    }*/

    void Explode()
    {
        Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );

        Destroy(gameObject);
    }


}
