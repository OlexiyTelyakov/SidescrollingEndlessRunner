using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.UI
{
    public class PauseMenuUI : InputReceiver
    {
        [SerializeField] private GameObject pauseMenuButton;
        [SerializeField] private GameObject pauseMenu;

        private void Start()
        {
            //Subscribe to the changeState event list.
            ServiceLocator.GameManager.onStateChange += SetPauseButtonActive;
        }

        public void OpenPauseMenu()
        {
            //Add this as input receiver.
            ServiceLocator.InputManager.AddReceiver(this);
            ServiceLocator.GameManager.PauseGame();

            //Activate menu.
            pauseMenu.SetActive(true);
        }

        public void ClosePauseMenu()
        {
            //Remove the receiver and unpause the game.
            ServiceLocator.InputManager.RemoveReceiver(this);
            ServiceLocator.GameManager.StartGame();

            //Deactivate menu.
            pauseMenu.SetActive(false);
            //Reenable the pauseMenuButton
            pauseMenuButton.SetActive(true);
        }

        public void SetPauseButtonActive(bool status)
        {
            pauseMenuButton.SetActive(status);
        }

        //Input override.
        public override void OnExitKeyUp()
        {
            ClosePauseMenu();
        }
    }
}