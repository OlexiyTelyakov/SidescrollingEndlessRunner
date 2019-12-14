using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public enum GameState { Play, Pause }

    public class GameStateManager : MonoBehaviour
    {
        private GameState currentState = GameState.Play;

        public void EndGame()
        {
            ServiceLocator.ScoreKeeper.UpdateBestScore();
            currentState = GameState.Pause;
            //End the game with a reset button?
        }

        //Getter for game state for other classes.
        public GameState CurrentState()
        {
            return currentState;
        }
    }
}