using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    /// <summary>
    /// Service locator class that holds references to other game system for data exchange.
    /// </summary>
    public class PointerManager : MonoBehaviour
    {
        #region Singleton
        private static PointerManager instance;
        public static PointerManager Instance
        {
            get
            {
                if(instance == null)
                {
                    //Attempt to locate an object.
                    instance = FindObjectOfType<PointerManager>();
                    if(instance == null)
                    {
                        //Lazy instantiation.
                        GameObject go = new GameObject("PointerManager");
                        instance = go.AddComponent<PointerManager>();
                    }
                }
                return instance;
            }
        }
        #endregion

        //Score system locator.
        private Score scoreKeeper;
        public Score ScoreKeeper
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
        private PlayerInput player;
        public PlayerInput Player
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

