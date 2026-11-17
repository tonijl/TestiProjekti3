using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserWeapon : MonoBehaviour
{

    [Header("Laser Settings")]
    public Transform laserOrigin;
    public float laserRange = 30f;
    public float damagePerSecond = 50f;

    [Header("References")]
    public LayerMask hitLayers;
    public Camera mainCamera;

    private LineRenderer lineRenderer;
    private bool isFiring;

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
        if(Input.GetMouseButton(0))
        {
            ShootLaser();
        }
        else
        {
            StopLaser();
        }
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
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
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
