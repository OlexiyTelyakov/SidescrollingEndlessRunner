using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    /// <summary>
    /// Service locator class that holds references to other game system for data exchange.
    /// </summary>
    public abstract class ServiceLocator : MonoBehaviour
    {

        //Score system locator.
        private static Score scoreKeeper;
        public static Score ScoreKeeper
        {
            get
            {
                if(scoreKeeper == null)
                {
                    scoreKeeper = FindObjectOfType<Score>();
                    if(scoreKeeper == null)
                    {
                        Debug.LogError("ERROR: Score component is missing in the scene.");
                    }
                }
                return scoreKeeper;
            }
        }

        //Player locator.
        private static PlayerInput player;
        public static PlayerInput Player
        {
            get
            {
                if (player == null)
                {
                    player = FindObjectOfType<PlayerInput>();
                    if (player == null)
                    {
                        Debug.LogError("ERROR: Player is missing in the scene.");
                    }
                }
                return player;
            }
        }
    }
}

