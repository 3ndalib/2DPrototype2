using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnemy : Enemy
{
    public float MoveSpeed;
    public float ExtraHeight;

    public Transform GroundDetection;

    public int FacingDirection = 1;

    public LayerMask PlatformLayerMask;

    public bool Grounded;
    public bool FacingRight = true;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        CheckingSurroundings();
    }

    public override void FixedUpdate()
    {
        Movement();
    }

    public void Movement() 
    {
        Move();
    }

    public void CheckingSurroundings()
    {
        IsGrounded();
        CheckMovementDirection();
        Grounded = IsGrounded();
    }

    public void Move()
    {
        RB.velocity = new Vector2(MoveSpeed * FacingDirection * Time.deltaTime,RB.velocity.y);
    }

    public bool IsGrounded()
    {
        RaycastHit2D Hit = Physics2D.Raycast(GroundDetection.position, Vector2.down, ExtraHeight, PlatformLayerMask);
        Color RayColor;
        if (Hit.collider != null)
        {
            RayColor = DetectingColor;
        }
        else
        {
            RayColor = NotDetectingColor;
        }
        Debug.DrawRay(GroundDetection.position, Vector2.down * ExtraHeight, RayColor);
        return Hit.collider != null;
    }

    public void CheckMovementDirection()
    {
        if (!Grounded)
        {
            Flip();
        }
    }
    public void Flip()
    {       
        FacingDirection *= -1;
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);     
    }
}
