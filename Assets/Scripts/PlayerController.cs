using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpSpeed = 10;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float wallSlideMultiplier = 0.4f;

    float accelerationTimeGrounded = .1f;
    float accelerationTimeAirborne = .2f;
    float smoothing;
    float xInput;
    bool jump;

    public bool wallSliding = false;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    float wallLeapTimer;
    float timeToUnstick;
    public float wallStickTime = .25f;
    bool canWallSlide;

    Rigidbody2D body;
    Animator anim;

    PlayerCollisions playerCollisions;

    public int playerId = 0;
    private Player player; // The Rewired Player


    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerCollisions = GetComponent<PlayerCollisions>();
        anim = GetComponent<Animator>();
        player = ReInput.players.GetPlayer(playerId);

    }

    // Update is called once per frame
    void Update()
    {
        xInput = player.GetAxis("Move Horizontal");
        if (xInput < 0) xInput = -1;
        if (xInput > 0) xInput = 1;
        if ((xInput > -1 && xInput < 0) && (xInput < 1 && xInput > 0)) xInput = 0;
        Debug.Log("Xinput:" + xInput);
        jump = player.GetButtonDown("Jump");
        
        if(player.GetButtonDown("Reset"))
        {
            SceneTransitioner.Instance.RestartScene();
        }
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

        if (playerCollisions.info.Below)
        {
            canWallSlide = true;
            timeToUnstick = wallStickTime;            
        }

        HandleJump();

        GravityAdjustment();

        Flip();
        HandleAnimations();
    }

    private void HandleJump()
    {
        if (jump && playerCollisions.info.Below)
        {
            body.velocity = Vector2.up * jumpSpeed;
        }

        // if wall to left or right, is in the air, and is falling
        if ((playerCollisions.info.Left || playerCollisions.info.Right) && !playerCollisions.info.Below && body.velocity.y < 0 && canWallSlide)
        {
            wallSliding = true;
           // if (playerCollisions.info.Left && xInput == -1 || playerCollisions.info.Right && xInput == 1)
            {
                if (wallLeapTimer > 0)
                {
                    body.velocity = new Vector2(0, body.velocity.y);

                    if (xInput != 0 && ((xInput == -1 && playerCollisions.info.Right) || (xInput == 1 && playerCollisions.info.Left)))
                    {
                        wallLeapTimer -= Time.deltaTime;
                    }
                    else
                        wallLeapTimer = wallStickTime;
                }
                else
                {
                    wallLeapTimer = wallStickTime;
                }
            }            
        }
        else
        {
            wallSliding = false;
        }

        if(wallSliding)
        {
            timeToUnstick -= Time.deltaTime;
            if(timeToUnstick<= 0)
            {
                timeToUnstick = wallStickTime;
                canWallSlide = false;
                wallSliding = false;
            }
        }
        else
        {
            canWallSlide = true;
        }



        if (jump && wallSliding)
        {
            // leap off wall to other side
            if ((xInput == 1 && playerCollisions.info.Left) || (xInput == -1 && playerCollisions.info.Right))
            {
                Vector2 vel = wallLeap;
                vel.x *= ((playerCollisions.info.Left) ? 1 : -1);
                body.velocity = vel;
            }
            // Wall Jump climb
            else if((playerCollisions.info.Left && xInput == -1 ) || (playerCollisions.info.Right && xInput == 1))
            {
                Vector2 vel = wallJumpClimb;
                vel.x *= ((playerCollisions.info.Left) ? 1 : -1);
                body.velocity = vel;
            }
            else
            {
                // hop off wall
                
                Vector2 vel = wallJumpOff;
                vel.x *= ((playerCollisions.info.Left) ? 1 : -1);
                body.velocity = vel;                
            }
        }
    }

    private void GravityAdjustment()
    {
        if (body.velocity.y < 0)
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * ((wallSliding) ? wallSlideMultiplier -1 : fallMultiplier - 1) * Time.deltaTime;

        }
        else
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Flip()

    {
        var localScale = transform.localScale;
        if(body.velocity.x !=0) 
            localScale.x = Mathf.Sign(body.velocity.x);
        transform.localScale = localScale;
    }

    private void HandleAnimations()
    {
        anim.SetFloat("yVelocity", body.velocity.y);
        anim.SetBool("Moving", xInput != 0);
        if (jump) anim.SetTrigger("Jump");
        anim.SetBool("OnGround", playerCollisions.info.Below);
    }
}
