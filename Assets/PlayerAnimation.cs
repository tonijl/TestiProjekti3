using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float speed = Mathf.Abs(rb.linearVelocity.x);
        float speed = rb.linearVelocity.magnitude;


       // Debug.Log("speed: "+speed.ToString());

        animator.SetFloat("Speed", speed);
    }
}
