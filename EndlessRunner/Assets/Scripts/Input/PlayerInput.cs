using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.UI;

namespace Runner
{
    public class PlayerInput : InputReceiver
    {
        [SerializeField] private PauseMenuUI pauseMenu;
        private CharacterController charController;

        private void Awake()
        {
            //Register the input receiver.
            ServiceLocator.InputManager.AddReceiver(this);

            charController = GetComponent<CharacterController>();
        }

        #region InputOverrides
        public override void OnJumpKey()
        {
            charController.OnJumpKey();
        }

        public override void OnJumpKeyUp()
        {
            charController.OnJumpKeyUp();
        }

        public override void OnExitKeyUp()
        {
            //Attempt to open the pause menu if the game isn't paused already.
            if (!ServiceLocator.GameManager.IsPaused())
            {
                pauseMenu.OpenPauseMenu();
            }
        }

        #endregion
    }
}