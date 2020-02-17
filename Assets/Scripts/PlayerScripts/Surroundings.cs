﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surroundings : MonoBehaviour
{
    public PlayerController PC;
    public BoxCollider2D BC;

    public bool FacingRight = true;
    public bool Grounded;
    public bool Walking;
    public bool CanJump;
    public bool TouchingWall;
    public bool LedgeDetected;
    public bool WallSliding;
    public bool AbleToWallJump;

    public float ExtraHeight;
    public float SkinWidth;
    public float WallCheckDistance;
    public float WallJumpCheckDistance;
    public float WallJumpAngle;

    public int FacingDirection = 1;

    public Color DetectingColor;
    public Color NotDetectingColor;

    public LayerMask PlatformLayerMask;

    public Vector2 Direction;
    void Start()
    {
        PC = GetComponent<PlayerController>();
        BC = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Direction = GetDirectionVector2D(WallJumpAngle) * new Vector2(-FacingDirection, 1);
        CheckingSurroundings();
    }

    public void CheckingSurroundings()
    {
        CheckMovementDirection();
        IsWalking();
        IsTouchingWall();
        IsAbleToWallJump();
        IsWallSliding();
        Grounded = IsGrounded();
        Walking = IsWalking();
        TouchingWall = IsTouchingWall();
        AbleToWallJump = IsAbleToWallJump();
        WallSliding = IsWallSliding();
        LedgeDetected = IsDetectingLedge();
    }

    public void CheckMovementDirection()
    {
        if (FacingRight && PC.MovementInputDirection < 0)
        {
            Flip();
        }
        else if (!FacingRight && PC.MovementInputDirection > 0)
        {
            Flip();
        }
    }
    public void Flip()
    {
        if (!WallSliding)
        {
            FacingDirection *= -1;
            FacingRight = !FacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D Hit = Physics2D.BoxCast(new Vector2(BC.bounds.center.x, BC.bounds.min.y), new Vector2(BC.bounds.size.x - SkinWidth, BC.bounds.size.y / 8), 0f, Vector2.down, ExtraHeight, PlatformLayerMask);
        Color RayColor;
        if (Hit.collider != null)
        {
            RayColor = DetectingColor;
        }
        else
        {
            RayColor = NotDetectingColor;
        }
        Debug.DrawRay(new Vector3(BC.bounds.center.x, BC.bounds.min.y) + new Vector3(BC.bounds.extents.x, 0), Vector2.down * (BC.bounds.extents.y / 8 + ExtraHeight), RayColor);
        Debug.DrawRay(new Vector3(BC.bounds.center.x, BC.bounds.min.y) - new Vector3(BC.bounds.extents.x, 0), Vector2.down * (BC.bounds.extents.y / 8 + ExtraHeight), RayColor);
        Debug.DrawRay(new Vector3(BC.bounds.center.x, BC.bounds.min.y) - new Vector3(BC.bounds.extents.x, BC.bounds.extents.y / 8 + ExtraHeight), Vector2.right * BC.bounds.extents * 2, RayColor);
        return Hit.collider != null;
    }

    public bool IsTouchingWall()
    {
        RaycastHit2D Hit;
        if (FacingRight)
        {
            Hit = Physics2D.Raycast(BC.bounds.center, Vector2.right, WallCheckDistance, PlatformLayerMask);
        }
        else
        {
            Hit = Physics2D.Raycast(BC.bounds.center, Vector2.left, WallCheckDistance, PlatformLayerMask);
        }
        Color RayColor;
        if (Hit.collider != null)
        {
            RayColor = DetectingColor;
        }
        else
        {
            RayColor = NotDetectingColor;
        }
        if (FacingRight)
        {
            Debug.DrawRay(BC.bounds.center, Vector2.right * WallCheckDistance, RayColor);
        }
        else
        {
            Debug.DrawRay(BC.bounds.center, Vector2.left * WallCheckDistance, RayColor);
        }
        return Hit.collider != null;
    }

    public bool IsAbleToWallJump() 
    {
        RaycastHit2D Hit;
        if (FacingRight)
        {
            Hit = Physics2D.Raycast(BC.bounds.center, Direction, WallJumpCheckDistance, PlatformLayerMask);
        }
        else
        {
            Hit = Physics2D.Raycast(BC.bounds.center, Direction, WallJumpCheckDistance, PlatformLayerMask);
        }
        Color RayColor;
        if (TouchingWall && !Grounded)
        {
            RayColor = DetectingColor;
        }
        else
        {
            RayColor = NotDetectingColor;
        }
        if (FacingRight)
        {
            Debug.DrawRay(BC.bounds.center, Direction * WallJumpCheckDistance, RayColor);
        }
        else
        {
            Debug.DrawRay(BC.bounds.center, Direction * WallJumpCheckDistance, RayColor);
        }
        if (TouchingWall && !Grounded)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool IsDetectingLedge()    
    {
        RaycastHit2D Hit;
        if (FacingRight)
        {
            Hit = Physics2D.Raycast(new Vector2(BC.bounds.center.x, BC.bounds.max.y), Vector2.right, WallCheckDistance, PlatformLayerMask);
        }
        else
        {
            Hit = Physics2D.Raycast(new Vector2(BC.bounds.center.x, BC.bounds.max.y), Vector2.left, WallCheckDistance, PlatformLayerMask);
        }
        Color RayColor;
        if (Hit.collider == null && IsTouchingWall())
        {
            RayColor = DetectingColor;
        }
        else
        {
            RayColor = NotDetectingColor;
        }
        if (FacingRight)
        {
            Debug.DrawRay(new Vector2(BC.bounds.center.x, BC.bounds.max.y), Vector2.right * WallCheckDistance, RayColor);
        }
        else
        {
            Debug.DrawRay(new Vector2(BC.bounds.center.x, BC.bounds.max.y), Vector2.left * WallCheckDistance, RayColor);
        }
        if (Hit.collider == null && IsTouchingWall())
        {
            return true;
        }
        else 
        {
            return false;
        }
        
    }


    public bool IsWallSliding()
    {
        if (TouchingWall && !Grounded && PC.RB.velocity.y < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsWalking()
    {
        if (PC.Velocity.x != 0 && Grounded && PC.MovementInputDirection != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 GetDirectionVector2D(float Angle)
    {
        return new Vector2(Mathf.Cos(Angle * Mathf.Deg2Rad), Mathf.Sin(Angle * Mathf.Deg2Rad)).normalized;
    }
}
