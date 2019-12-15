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
                if (scoreKeeper == null)
                {
                    scoreKeeper = FindObjectOfType<Score>();
                    if (scoreKeeper == null)
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

        //GameStateManager locator
        private static GameStateManager gameManager;
        public static GameStateManager GameManager
        {
            get
            {
                if (gameManager == null)
                {
                    gameManager = FindObjectOfType<GameStateManager>();
                    if (gameManager == null)
                    {
                        Debug.LogError("ERROR: GameStateManager is missing in the scene.");
                    }
                }
                return gameManager;
            }
        }

        //ObjectPooler locator
        private static ObjectPooler objectPooler;
        public static ObjectPooler ObjectPooler
        {
            get
            {
                if (objectPooler == null)
                {
                    objectPooler = FindObjectOfType<ObjectPooler>();
                    if (objectPooler == null)
                    {
                        Debug.LogError("ERROR: ObjectPooler is missing in the scene.");
                    }
                }
                return objectPooler;
            }
        }

        //Level builder
        private static LevelBuilder levelBuilder;
        public static LevelBuilder LevelBuilder
        {
            get
            {
                if(levelBuilder == null)
                {
                    levelBuilder = FindObjectOfType<LevelBuilder>();
                    if(levelBuilder == null)
                    {
                        Debug.LogError("ERROR: LevelBuilder is missing in the scene.");
                    }
                }
                return levelBuilder;
            }
        }
    }
}

