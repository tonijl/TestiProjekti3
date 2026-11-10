
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RangedEnemy : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;

   // public float shootingRange = 8f;
    public float fireRate = 100;

    private float nextFireTimer = 100;

	public Transform player;
	public PathfindingGrid pathfinding;
	
	[Header("Movement")]
	public float speed = 3f;
	public float repathInterval = 0.25f;
	public float waypointDistance = 0.1f;
	
	[Header("Line of Sight")]
	public LayerMask obstacleMask;
	public float sightRange = 10f;
	
	[Header("Pathfinding Lock")]
	public float minimumPathfindingTime = 1.5f;
	private float pathfindingLockedUntil = 0f;
	
	private Rigidbody2D rb;
	private List<Vector2> path = new List<Vector2>();
	private int waypointIndex;
	private float nextRepathTime;
	
	private bool touchingWall;
	
	private bool hasLineOfSight;

	public AudioClip laserSound;
	
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		
		if(pathfinding == null)
		{
			pathfinding = FindFirstObjectByType<PathfindingGrid>();
		}
		
		
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

	
			if(playerObject != null)
			{
				player = playerObject.transform;
			}
			else
			{
				Debug.LogError("No GameObject with the Player tag was found!");
			}
		
	}



void Update()
{
    if (player == null || pathfinding == null)
        return;

    Vector2 toPlayer = (Vector2)player.position - rb.position;
    float distance = toPlayer.magnitude;

    // Check line of sight.
    hasLineOfSight = distance <= sightRange &&
        !Physics2D.Raycast(
            rb.position,
            toPlayer.normalized,
            distance,
            obstacleMask
        );
		
	/*Debug.Log(
		"LOS: " + hasLineOfSight +
		" | Distance: " + distance +
		" | Touching wall: " + touchingWall
	);*/

    // Recalculate the path if sight is blocked,
    // or the enemy is touching a wall, or still locked into pathfinding.
    bool usePathfinding =
        !hasLineOfSight ||
        touchingWall ||
        Time.time < pathfindingLockedUntil;

    if (usePathfinding && Time.time >= nextRepathTime)
    {
        List<Vector2> newPath = pathfinding.FindPath(
            rb.position,
            player.position
        );

        if (newPath != null && newPath.Count > 0)
        {
            path = newPath;
            waypointIndex = 0;
        }

        nextRepathTime = Time.time + repathInterval;
    }
}


	
	

void FixedUpdate()
{
    if (player == null || pathfinding == null)
        return;

    bool usePathfinding =
        !hasLineOfSight ||
        touchingWall ||
        Time.time < pathfindingLockedUntil;

    if (!usePathfinding)
    {
        // Direct chase toward the player's CURRENT position.
        Vector2 toPlayer = (Vector2)player.position - rb.position;

        if (toPlayer.magnitude > waypointDistance)
        {
            Vector2 nextPosition = Vector2.MoveTowards(
                rb.position,
                player.position,
                speed * Time.fixedDeltaTime
            );

            rb.MovePosition(nextPosition);
        }

        path.Clear();
        waypointIndex = 0;


        if(nextFireTimer <= 0)
        {
            Shoot();
            nextFireTimer = fireRate;
        }
        nextFireTimer--;



    }
    else
    {
        FollowPath();
    }
}

    void Shoot()
    {

		AudioManager.Instance.PlaySFX(laserSound, transform.position, 1f, 0.9f, 1.1f);

        Vector2 direction = (player.position - firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f,0f,angle);

        Instantiate(bulletPrefab, firePoint.position, rotation);

    }


	
	
		void FollowPath()
	{
		if (path == null || waypointIndex >= path.Count)
			return;

		Vector2 target = path[waypointIndex];

		// Skip waypoints that have already been reached.
		while (waypointIndex < path.Count &&
			   Vector2.Distance(rb.position, path[waypointIndex])
				   <= waypointDistance)
		{
			waypointIndex++;
		}

		if (waypointIndex >= path.Count)
			return;

		target = path[waypointIndex];

		Vector2 nextPosition = Vector2.MoveTowards(
			rb.position,
			target,
			speed * Time.fixedDeltaTime
		);

		rb.MovePosition(nextPosition);
	}
	

	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
		{
			
			//Debug.Log("Enemy colliding with wall!");
			
			touchingWall = true;
			
			// Fore path finding mode for minimum duration.
			pathfindingLockedUntil = Mathf.Max(pathfindingLockedUntil, Time.time + minimumPathfindingTime);
			
			
			// Recalculate the path immediately.
			nextRepathTime = 0f;
		}
	}
	
	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
		{
			touchingWall = false;
			nextRepathTime = 0f;
		}
	}
	
	
}
