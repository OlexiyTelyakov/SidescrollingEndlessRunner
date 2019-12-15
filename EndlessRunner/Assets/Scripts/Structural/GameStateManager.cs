using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public enum GameState { Play, Pause }

    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private GameObject restartMenu;

        private GameState currentState = GameState.Pause;

        public void Start()
        {
            currentState = GameState.Pause;
        }

        public void StartGame()
        {
            //Set game state to play.
            currentState = GameState.Play;
        }

        public void EndGame()
        {
            ServiceLocator.ScoreKeeper.UpdateBestScore();
            currentState = GameState.Pause;


        }

        //Getter for game state for other classes.
        public GameState CurrentState()
        {
            return currentState;
        }
    }
}