using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public CollisionInfo info;

    Collider2D collider;
    Bounds bounds;

    RaycastOrigins rayOrigins;
    public const float skinWidth = .015f;

    public LayerMask collisionMask;


    // Start is called before the first frame update
    void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float directionX = Mathf.Sign(transform.localScale.x);
        info.Reset();
        UpdateRaycastOrigins();

        // cast Right
        Vector2 rayOrigin = rayOrigins.Right;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, 2* skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, Vector2.right, Color.red);

        if(hit)
        {
            info.Right = true;
        }

        rayOrigin = rayOrigins.TopRight;
        rayOrigin.y -= skinWidth;
        hit = Physics2D.Raycast(rayOrigin, Vector2.right, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, Vector2.right, Color.red);

        if (hit)
        {
            info.Right = true;
        }

        rayOrigin = rayOrigins.BottomRight;
        rayOrigin.y += skinWidth;
        hit = Physics2D.Raycast(rayOrigin, Vector2.right, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, Vector2.right, Color.red);

        if (hit)
        {
            info.Right = true;
        }

        // cast Left
        rayOrigin = rayOrigins.Left;
        hit = Physics2D.Raycast(rayOrigin, -Vector2.right, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, -Vector2.right, Color.red);

        if (hit)
        {
            info.Left = true;
        }

        rayOrigin = rayOrigins.BottomLeft;
        rayOrigin.y += skinWidth;
        hit = Physics2D.Raycast(rayOrigin, -Vector2.right, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, -Vector2.right, Color.red);

        if (hit)
        {
            info.Left = true;
        }

        rayOrigin = rayOrigins.TopLeft;
        rayOrigin.y -= skinWidth;
        hit = Physics2D.Raycast(rayOrigin, -Vector2.right, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, -Vector2.right, Color.red);

        if (hit)
        {
            info.Left = true;
        }

        // cast Up
        rayOrigin = rayOrigins.Top;
        hit = Physics2D.Raycast(rayOrigin, Vector2.up, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, Vector2.up, Color.red);

        if (hit)
        {
            info.Above = true;
        }

        rayOrigin = rayOrigins.TopLeft;
        rayOrigin.x += skinWidth;
        hit = Physics2D.Raycast(rayOrigin, Vector2.up, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, Vector2.up, Color.red);

        if (hit)
        {
            info.Above = true;
        }

        rayOrigin = rayOrigins.TopRight;
        rayOrigin.x -= skinWidth;
        hit = Physics2D.Raycast(rayOrigin, Vector2.up, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, Vector2.up, Color.red);

        if (hit)
        {
            info.Above = true;
        }

        // cast Down
        rayOrigin = rayOrigins.Bottom;
        hit = Physics2D.Raycast(rayOrigin, -Vector2.up, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, -Vector2.up, Color.red);

        if (hit)
        {
            info.Below = true;
        }

        rayOrigin = rayOrigins.BottomLeft;
        rayOrigin.x += skinWidth;
        hit = Physics2D.Raycast(rayOrigin, -Vector2.up, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, -Vector2.up, Color.red);

        if (hit)
        {
            info.Below = true;
        }

        rayOrigin = rayOrigins.BottomRight;
        rayOrigin.x -= skinWidth;
        hit = Physics2D.Raycast(rayOrigin, -Vector2.up, 2 * skinWidth, collisionMask);
        Debug.DrawRay(rayOrigin, -Vector2.up, Color.red);

        if (hit)
        {
            info.Below = true;
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
