using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public CollisionInfo collisionInfo;

    BoxCollider2D collider;
    Bounds bounds;

    RaycastOrigins rayOrigins;
    public const float skinWidth = .015f;

    public LayerMask collisionMask;


    // Start is called before the first frame update
    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float directionX = Mathf.Sign(transform.localScale.x);
        collisionInfo.Reset();
        UpdateRaycastOrigins();

        // cast Right
        RaycastHit2D hit = Physics2D.Raycast(rayOrigins.Right, Vector2.right, 2* skinWidth, collisionMask);
        Debug.DrawRay(rayOrigins.Right, Vector2.right, Color.red);

        if(hit)
        {
            collisionInfo.Right = true;
        }

        // cast Left
        hit = Physics2D.Raycast(rayOrigins.Left, -Vector2.right, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigins.Left, -Vector2.right, Color.red);

        if (hit)
        {
            collisionInfo.Left = true;
        }

        // cast Up
        hit = Physics2D.Raycast(rayOrigins.Top, Vector2.up, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigins.Top, Vector2.up, Color.red);

        if (hit)
        {
            collisionInfo.Above = true;
        }

        // cast Down
        hit = Physics2D.Raycast(rayOrigins.Bottom, -Vector2.up, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigins.Bottom, -Vector2.up, Color.red);

        if (hit)
        {
            collisionInfo.Below = true;
        }
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        rayOrigins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayOrigins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rayOrigins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayOrigins.TopRight = new Vector2(bounds.max.x, bounds.max.y);

        rayOrigins.Bottom = new Vector2(bounds.center.x, bounds.min.y);
        rayOrigins.Right = new Vector2(bounds.max.x, bounds.center.y);
        rayOrigins.Left = new Vector2(bounds.min.x, bounds.center.y);
        rayOrigins.Top = new Vector2(bounds.center.x, bounds.max.y);
    }

    [System.Serializable]
    public struct CollisionInfo
    {
        public bool Below;
        public bool Above;
        public bool Right;
        public bool Left;

        public void Reset()
        {
            Below = Above = Right = Left = false;
        }
    }

    public struct RaycastOrigins
    {
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;

        public Vector2 Top;
        public Vector2 Right;
        public Vector2 Left;
        public Vector2 Bottom;
    }

}
