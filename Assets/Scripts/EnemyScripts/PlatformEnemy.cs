using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnemy : Enemy
{
    public float SkinWidth;
    public float ExtraHeight;

    public LayerMask PlatformLayerMask;

    public bool Grounded;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        IsGrounded();
        Grounded = IsGrounded();
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
}
