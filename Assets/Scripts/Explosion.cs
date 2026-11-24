using UnityEngine;
using System.Collections;


public class Explosion : MonoBehaviour
{

    public float explosionRadius = 3f;
    public float damage = 40f;
    public float explosionDuration = 0.5f;

    public LayerMask damageableLayer;

    [SerializeField] private SpriteRenderer spriteRenderer;



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
        StartCoroutine(FadeOut(explosionDuration));
    }

    void FixedUpdate()
    {

    }

   IEnumerator FadeOut(float duration)
    {
        float elapsed = 0f;
        Color color = spriteRenderer.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            spriteRenderer.color = color;
             yield return null;
        }

        color.a = 0f;
        spriteRenderer.color = color;


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
