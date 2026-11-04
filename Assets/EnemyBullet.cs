using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed = 6f;
    public float damage = 10f;
    public float lifetime = 4f;

    private Rigidbody2D rb;

  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
    
    }*/


	void OnCollisionEnter2D(Collision2D collision)
	{
		
		//EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        playerScript player = collision.gameObject.GetComponent<playerScript>();
		
		if(player != null)
		{
			//Debug.Log("Bullet collided with enemy!");
			player.TakeDamage((int)damage);
			Destroy(gameObject);
		}


		if(collision.gameObject.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}

	
	}





    // Update is called once per frame
    void Update()
    {
        
    }
}
