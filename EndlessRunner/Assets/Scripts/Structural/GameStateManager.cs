using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.UI;

namespace Runner
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private GameObject restartMenu;
        [SerializeField] private PauseMenuUI pauseMenu;

        //Game state change event.
        public delegate void OnStateChange(bool state);
        public event OnStateChange onStateChange;

        private enum GameState { Play, Pause }

        private GameState currentState;
        private GameState CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                //Notify event subscribers that game state has changed.
                onStateChange?.Invoke((value == GameState.Play) ? true : false);

                currentState = value;
            }
        }

        private void Start()
        {
            CurrentState = GameState.Pause;
        }

        public void StartGame()
        {
            //Set game state to play.
            CurrentState = GameState.Play;
        }

        public void PauseGame()
        {
            //Set game state to Pause
            CurrentState = GameState.Pause;
        }

        public void EndGame()
        {
            ServiceLocator.ScoreKeeper.UpdateBestScore();
            CurrentState = GameState.Pause;

            restartMenu.SetActive(true);
        }

        /// <summary>
        /// Resets the game to its starting point.
        /// </summary>
        public void RestartGame()
        {
            //Reset various systems to their starting position.
            ServiceLocator.ObjectPooler.ResetObjectPool();
            ServiceLocator.Player.ResetPlayer();
            ServiceLocator.ScoreKeeper.ResetScore();
            ServiceLocator.LevelBuilder.ResetLevel();

            CurrentState = GameState.Play;
        }

        public bool IsPaused()
        {
            return (currentState == GameState.Pause) ? true : false;
        }

        
        public void ExitGame()
        {
            Application.Quit();
        }

        //Pause the game if application is paused.
        private void OnApplicationPause(bool pause)
        {
            if(pause && currentState != GameState.Pause) pauseMenu.OpenPauseMenu();
        }
    }
}