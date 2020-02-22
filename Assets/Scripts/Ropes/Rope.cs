using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public LineRenderer LR;
    public Transform StartPos;

    public List<RopeSegment> RopeSegments = new List<RopeSegment>();
    public Vector2 GravityForce = new Vector2(0, -1);

    public float SegmentLength = 0.25f;
    public float LineWidth = 0.1f;

    public int RopeLength = 35;

    public void Start()
    {
        LR = GetComponent<LineRenderer>();

        Vector2 RopeStartPos = StartPos.position;

        for (int i = 0; i < RopeLength; i++) 
        {
            RopeSegments.Add(new RopeSegment(RopeStartPos));
            RopeStartPos.y -= SegmentLength;
        }
    }

    public void Update()
    {
        DrawRope();
    }

    public void Simulate() 
    {
        for (int i = 0; i < RopeLength; i ++) 
        {
            RopeSegment TheSegment = RopeSegments[i];
            Vector2 Velocity = TheSegment.CurrentPos - TheSegment.OldPos;

            TheSegment.OldPos = TheSegment.CurrentPos;
            TheSegment.CurrentPos += Velocity;
            TheSegment.CurrentPos += GravityForce * Time.deltaTime;
            RopeSegments[i] = TheSegment;
        }
    }

    public void ApplyConstraints() 
    { 
    }

    public void DrawRope() 
    {
        LR.startWidth = LineWidth;
        LR.endWidth = LineWidth;

        Vector3[] RopePoses = new Vector3[RopeLength];

        for (int i = 0; i < RopeLength; i++) 
        {
            RopePoses[i] = RopeSegments[i].CurrentPos;
        }

        LR.positionCount = RopePoses.Length;
        LR.SetPositions(RopePoses);
    }

    public struct RopeSegment
    {
        public Vector2 CurrentPos;
        public Vector2 OldPos;

        public RopeSegment(Vector2 Pos) 
        {
            this.CurrentPos = Pos;
            this.OldPos = Pos;
        }
    }
}
