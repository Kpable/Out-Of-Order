using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpSpeed = 10;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    float accelerationTimeGrounded = .1f;
    float accelerationTimeAirborne = .2f;
    float smoothing;
    float xInput;
    bool jump;

    Rigidbody2D body;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        jump = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(xInput * moveSpeed * Time.deltaTime, 0);

        float targetVelocityX = xInput * moveSpeed * Time.deltaTime;
        //Vector2 movement  = Vector2.right * Mathf.SmoothDamp(body.velocity.x, targetVelocityX, ref smoothing, accelerationTimeGrounded );
        if (movement.x != 0)
            body.velocity += movement;
        else
            body.velocity = Vector2.up * body.velocity.y;
        Debug.Log(movement);
        if (jump)
        {
            body.velocity = Vector2.up * jumpSpeed;
        }

        if (body.velocity.y < 0)
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        Flip();
    }

    void Flip()

    {
        var localScale = transform.localScale;
        if(body.velocity.x !=0) 
            localScale.x = Mathf.Sign(body.velocity.x);
        transform.localScale = localScale;
    }
}
