using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    /// <summary>
    /// Base class that handles raycast positions on various entities in the game.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class RaycastController : MonoBehaviour
    {
        [SerializeField] protected LayerMask collisionMask;

        BoxCollider2D boxCollider;

        protected RayOrigins origins;

        [SerializeField] protected int horizontalRayCount;
        [SerializeField] protected int verticalRayCount;

        //Determines the amount by which base bounds are reduced to provide better collision detection.
        protected float skinWidth = 0.02f;

        protected float horizontalRaySpacing;
        protected float verticalRaySpacing;

        private void Awake()
        {
            //Acquire the collider.
            boxCollider = GetComponent<BoxCollider2D>();
        }

        protected virtual void Start()
        {
            CalculateRaySpacing();
        }

        private void CalculateRaySpacing()
        {
            //Collider bounds are reduced so the raycast starts inside the entity, rather than on colliders edge which can lead to it firing inside the obstacle and not registering it correctly.
            Bounds bounds = boxCollider.bounds;
            bounds.Expand(-skinWidth * 2);

            horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
            verticalRaySpacing = bounds.size.x / (verticalRayCount -1);
        }

        /// <summary>
        /// Recalculate world position of the ray origins.
        /// </summary>
        protected void UpdateRayOrigins()
        {
            //Collider bounds are reduced so the raycast starts inside the entity, rather than on colliders edge which can lead to it firing inside the obstacle and not registering it correctly.
            Bounds bounds = boxCollider.bounds;
            bounds.Expand(-skinWidth * 2);

            //Set the origins.
            origins.botLeft = new Vector2(bounds.min.x, bounds.min.y);
            origins.botRight = new Vector2(bounds.max.x, bounds.min.y);
            origins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
            origins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        }

        //Struct to hold calculated points on the collider for ease of access/readability in other classes.
        public struct RayOrigins
        {
            public Vector2 topLeft, topRight;
            public Vector2 botLeft, botRight;
        }
    }
}
