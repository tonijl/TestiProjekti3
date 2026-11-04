using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float explosionRadius = 3f;
    public float damage = 40f;
    public float explosionDuration = 0.5f;

    public LayerMask damageableLayer;

    void Start()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(
            transform.position,
            explosionRadius,
            damageableLayer
        );

        foreach (Collider2D target in targets)
        {

		//EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        EnemyHealth health = target.GetComponent<EnemyHealth>();
		
		if(health != null)
		{
			health.TakeDamage((int)damage);
		}


        playerScript player = target.gameObject.GetComponent<playerScript>();

        if(player != null)
        {
            player.TakeDamage((int)damage);
        }



        }
        Destroy(gameObject, explosionDuration);
    }


	/*private void OnCollisionEnter2D(Collision2D collision)
	{
		
		//PlayerHealth player = collision.GetComponent<PlayerHealth>();
		playerScript player = collision.gameObject.GetComponent<playerScript>();
		
		if(player != null)
		{
			player.TakeDamage((int)damage);
		}
		
	}'*/



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }



}
