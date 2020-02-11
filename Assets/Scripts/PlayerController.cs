using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D RB;
    public Surroundings SR;

    public Vector2 Velocity;
    public Vector2 WallJumpDirection;

    public float MovementInputDirection;
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float JumpForce;
    public float JumpHeightMultiplier = 0.5f;
    public float WallSlideSpeed;
    public float WallJumpForce;

    public int ExtraJumpsValue;
    public int ExtraJumps;

    public bool IsJumping;
    void Start()
    {
        ExtraJumps = ExtraJumpsValue;
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<Surroundings>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckingInput();
    }

    private void FixedUpdate()
    {
        Movement();
        Velocity = new Vector2(RB.velocity.x, RB.velocity.y);
    }

    public void CheckingInput()
    {
        MovementInputDirection = Input.GetAxisRaw("Horizontal");
        Jump();
        WallJump();
    }


    public void Movement()
    {
        Move();
        MoveCap();
        JumpCap();
        WallSlide();
    }

    public void Move()
    {
        if (MovementInputDirection > 0)
        {
            RB.velocity = new Vector2(RB.velocity.x + (MaxSpeed * Acceleration * Time.deltaTime), RB.velocity.y);
        }
        else if (MovementInputDirection < 0)
        {
            RB.velocity = new Vector2(RB.velocity.x + (-MaxSpeed * Acceleration * Time.deltaTime), RB.velocity.y);
        }
        else
        {
            if (RB.velocity.x > 0)
            {
                RB.velocity = new Vector2(RB.velocity.x - (MaxSpeed * Deceleration * Time.deltaTime), RB.velocity.y);
                if (RB.velocity.x <= 0)
                {
                    RB.velocity = new Vector2(0, RB.velocity.y);
                }
            }
            else if (RB.velocity.x < 0)
            {
                RB.velocity = new Vector2(RB.velocity.x + (MaxSpeed * Deceleration * Time.deltaTime), RB.velocity.y);
                if (RB.velocity.x >= 0)
                {
                    RB.velocity = new Vector2(0, RB.velocity.y);
                }
            }
        }
    }

    void MoveCap()
    {
        if (RB.velocity.x > MaxSpeed)
        {
            RB.velocity = new Vector2(MaxSpeed, RB.velocity.y);
        }
        else if (RB.velocity.x < -MaxSpeed)
        {
            RB.velocity = new Vector2(-MaxSpeed, RB.velocity.y);
        }
    }

    public void Jump()
    {
        if (!SR.WallSliding)
        {
            if (SR.Grounded)
            {
                ExtraJumps = ExtraJumpsValue;
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && ExtraJumps > 0)
            {
                IsJumping = true;
                RB.velocity = new Vector2(RB.velocity.x, JumpForce);
                ExtraJumps--;
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
            {
                if (RB.velocity.y > 0)
                {
                    RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * JumpHeightMultiplier);
                }
            }

            //else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && ExtraJumps == 0 && SR.Grounded) 
            //{
            //    RB.velocity = new Vector2(RB.velocity.x, JumpForce);
            //}
        }
    }

    public void JumpCap()
    {
        if (SR.WallSliding)
        {
            ExtraJumps = 0;
        }
    }

    public void WallJump()
    {
        if (SR.WallSliding && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))) 
        {
            WallJumpDirection = new Vector2(WallJumpDirection.x * -SR.FacingDirection, WallJumpForce);
            RB.AddForce(WallJumpDirection, ForceMode2D.Impulse);
        }
    }

    public void WallSlide()
    {
        if (SR.WallSliding)
        {
            if (RB.velocity.y < -WallSlideSpeed)
            {
                RB.velocity = new Vector2(RB.velocity.x, -WallSlideSpeed);
            }
        }
    }

}
