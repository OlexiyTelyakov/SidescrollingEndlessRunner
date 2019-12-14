using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    /// <summary>
    /// Solves collision and movement for the player.
    /// </summary>
    public class MovementController : RaycastController
    {
        public bool grounded = false;
        public bool headCollision = false;

        protected override void Start()
        {
            //Calculate RayOrigins.
            base.Start();
        }

        public void CalculateMovement(Vector2 velocity)
        {
            //Reset collision states.
            grounded = false;
            headCollision = false;

            UpdateRayOrigins();

            //Check collisions.
            HorizontalCollision(ref velocity);
            VerticalCollisions(ref velocity);

            //Move the character.
            transform.Translate(velocity);
        }

        private void HorizontalCollision(ref Vector2 velocity)
        {
            //Horizontal calculation is simpler than vertical due to runners only ever moving to the right, hence no calculation is necessary to adjust for direction.

            float rayLength = velocity.x + skinWidth;

            //Loop through each ray.
            for(int i = 0;i < horizontalRayCount; i++)
            {
                //Visualization assist for use in Editor.
                Debug.DrawRay(origins.botRight + Vector2.up * i * horizontalRaySpacing, Vector2.right * rayLength, Color.red);

                RaycastHit2D hit = Physics2D.Raycast(origins.botRight + Vector2.up * i * horizontalRaySpacing, Vector2.right, rayLength, collisionMask) ;

                //If collision is detected:
                if (hit)
                {
                    //Velocity is set to the length of the ray - player cannot warp into a wall since hit distance will be precisely to the wall.
                    velocity.x = hit.distance - skinWidth;
                    //Consequent rays can be shorter as base line for collision has been established.
                    rayLength = hit.distance;
                }
            }
        }

        private void VerticalCollisions(ref Vector2 velocity)
        {
            int moveDir = (int)Mathf.Sign(velocity.y);
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;
            Vector2 rayOrigin = (moveDir == 1) ? origins.topLeft : origins.botLeft;

            //Loop through each ray.
            for (int i = 0; i < verticalRayCount; i++)
            {
                //Visualization assist for use in Editor.
                Debug.DrawRay(rayOrigin + Vector2.right * i * verticalRaySpacing, Vector2.up * moveDir * rayLength, Color.red);

                RaycastHit2D hit = Physics2D.Raycast(rayOrigin + Vector2.right * i * verticalRaySpacing, Vector2.up * moveDir, rayLength, collisionMask);

                //If collision is detected:
                if (hit)
                {
                    //Velocity is set to the length of the ray - player cannot warp into a wall since hit distance will be precisely to the wall.
                    velocity.y = (hit.distance - skinWidth) * moveDir;
                    //Consequent rays can be shorter as base line for collision has been established.
                    rayLength = hit.distance;

                    //When velocity is negative (ie moving down), the character is grounded.
                    if(moveDir == -1)
                    {
                        grounded = true;
                    }
                    else
                    {
                        headCollision = true;
                    }
                }
            }
        }
    }
}

