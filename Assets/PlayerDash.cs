using UnityEngine;
using System.Collections;


public class PlayerDash : MonoBehaviour
{

    [Header("Dash Settings")]

    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 1f;

    [Header("References")]
    public Rigidbody2D rb;

    private bool isDashing = false;
    private bool canDash = true;

    public bool IsInvulnerable { get; private set; } = false;

    private Vector2 movementInput;
    private Vector2 lastDirection = Vector2.right;

    public AudioClip dashSound;

    public int playerLayer;
    public int enemyLayer;

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput = movementInput.normalized;

        if (movementInput != Vector2.zero)
        {
            lastDirection = movementInput;
        }


        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Debug.Log("Dash pressed!");

            StartCoroutine(Dash());
        }

    }

    IEnumerator Dash()
    {

        AudioManager.Instance.PlaySFX(dashSound, transform.position, 1f, 0.9f, 1.1f);

        canDash = false;
        isDashing = true;
        IsInvulnerable = true;

        Vector2 dashDirection = movementInput != Vector2.zero
            ? movementInput
            : lastDirection;


            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

            rb.linearVelocity = dashDirection * dashSpeed;

            Debug.Log("rb.linearVelocity: "+rb.linearVelocity.ToString());

            yield return new WaitForSeconds(dashDuration);


            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);

            rb.linearVelocity = Vector2.zero;

            isDashing = false;
            IsInvulnerable = false;

            yield return new WaitForSeconds(dashCooldown);

            canDash = true;


    }

    public bool IsDashing()
    {
        return isDashing;
    }



}
