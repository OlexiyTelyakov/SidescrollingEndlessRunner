using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    /// <summary>
    /// Handles player input processing and character behavior.
    /// </summary>
    public class PlayerInput : InputReceiver
    {
        private GameStateManager stateManager;
        private MovementController moveController;

        private Vector2 velocity;

        [SerializeField] private Transform startPosition;

        [SerializeField] private float baseMoveSpeed = 1f;
        [SerializeField] private float maxSpeed = 12f;
        private float moveSpeed;


        //Gravity and jump variables.
        [SerializeField] private float maxJumpHeight = 4f;
        [SerializeField] private float minJumpHeight = 2f;
        [SerializeField] private float timeToApex = 1f;
        private float maxJumpVelocity;
        private float minJumpVelocity;
        private float gravity;

        private void Awake()
        {
            //Register the input receiver.
            ServiceLocator.InputManager.AddReceiver(this);
        }

        private void Start()
        {
            stateManager = ServiceLocator.GameManager;

            moveController = GetComponent<MovementController>();
            //Gravity is solved as a product of jump height and jump time so it can be adjusted easier in editor.
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            //Jump velocity is also solved from gravity and timing.
            maxJumpVelocity = Mathf.Abs(gravity) * timeToApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

            moveSpeed = baseMoveSpeed;
        }

        private void Update()
        {
            //Stop movement during pause.
            if (stateManager.CurrentState() == GameState.Pause) return;

            //Set velocity;
            velocity.x = moveSpeed;
            velocity.y += gravity * Time.deltaTime;

            //Check for jumping.
            if (Input.GetKey(KeyCode.Space) && moveController.grounded)
            {
                velocity.y = maxJumpVelocity;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if(velocity.y > minJumpVelocity)
                {
                    velocity.y = minJumpVelocity;
                }
            }

            moveController.CalculateMovement(velocity * Time.deltaTime);

            //Y velocity is reset while on the ground or hitting something overhead to prevent the value from compounding.
            if(moveController.grounded || moveController.headCollision)
            {
                velocity.y = 0;
            }
        }

        /// <summary>
        /// Increases movement speed for this run.
        /// </summary>
        /// <param name="boost"></param>
        public void BoostSpeed(float boost)
        {
            moveSpeed += boost;
            moveSpeed = Mathf.Clamp(moveSpeed, 0, maxSpeed);
        }

        /// <summary>
        /// Resets player speed and returns them to the start of the game.
        /// </summary>
        public void ResetPlayer()
        {
            transform.position = startPosition.position;

            moveSpeed = baseMoveSpeed;
        }

        #region InputOverrides 
        public override void OnJumpKey()
        {
            if (moveController.grounded)
            {
                velocity.y = maxJumpVelocity;
            }
        }

        public override void OnJumpKeyUp()
        {
            if(velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }
        #endregion
    }
}