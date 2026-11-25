using UnityEngine;

public class MuzzleFlashFollow : MonoBehaviour
{
    public Transform firePoint;

    void LateUpdate()
    {
        if(firePoint == null)
        {
            return;
        }

        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;

    }

}
