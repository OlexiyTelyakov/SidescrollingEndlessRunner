using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    /// <summary>
    /// Handles player input and character behavior.
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        private MovementController moveController;

        private Vector2 velocity;

        [SerializeField] private float moveSpeed = 1f;

        //Gravity and jump variables.
        [SerializeField] private float jumpHeight = 5f;
        [SerializeField] private float timeToApex = 1f;
        private float jumpVelocity;
        private float gravity;

        private void Start()
        {
            moveController = GetComponent<MovementController>();
            //Gravity is solved as a product of jump height and jump time so it can be adjusted easier in editor.
            gravity = -(2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
            //Jump velocity is also solved from gravity and timing.
            jumpVelocity = Mathf.Abs(gravity) * timeToApex;
        }

        private void Update()
        {
            //Set velocity;
            velocity.x = moveSpeed;
            velocity.y += gravity * Time.deltaTime;

            //Check for jumping.
            if (Input.GetKeyDown(KeyCode.Space) && moveController.grounded)
            {
                velocity.y = jumpVelocity;
            }

            moveController.CalculateMovement(velocity * Time.deltaTime);

            //Y velocity is reset while on the ground or hitting something overhead to prevent the value from compounding.
            if(moveController.grounded || moveController.headCollision)
            {
                velocity.y = 0;
            }
        }
    }
}


