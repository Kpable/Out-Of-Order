using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpSpeed = 10; 

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
        Vector2 movement = new Vector2(xInput * moveSpeed * Time.deltaTime, body.velocity.y);

        Debug.Log(movement);
        if(jump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }
}
