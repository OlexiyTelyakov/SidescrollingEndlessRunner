using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    /// <summary>
    /// Class that keeps track of the score.
    /// </summary>
    public class Score : MonoBehaviour
    {
        private CharacterController player;
        private float oldXPos;

        private float score;
        private float bestScore;

        void Awake()
        {
            //Attempt to get best score or create it.
            if (!PlayerPrefs.HasKey("BestScore"))
            {
                PlayerPrefs.SetFloat("BestScore", 0);
            }
            else
            {
                bestScore = PlayerPrefs.GetFloat("BestScore");
            }

            //Reset the score.
            score = 0;
            //Player is used consistently in this class, so it is cashed into a variable, rather than constantly calling service locator.
            player = ServiceLocator.Player;
            oldXPos = player.transform.position.x;
        }

        // Update is called once per frame
        void Update()
        {
            //Calculate the score based on the distance travelled by the player.
            float dst = player.transform.position.x - oldXPos;
            score += dst;
            oldXPos = player.transform.position.x;

            UpdateBestScore();
        }

        //Getter function for other classes to access the score value.
        public float GetScore()
        {
            return score;
        }

        //Getter function for other classes to access the players best score.
        public float GetBestScore()
        {
            return bestScore;
        }

        //Called on end-state to update previous score.
        public void UpdateBestScore()
        {
            //Check best score
            if (bestScore <= score)
            {
                bestScore = score;
                PlayerPrefs.SetFloat("BestScore", bestScore);
            }
        }

        public void AddScore(float bonus)
        {
            score += bonus;
        }

        /// <summary>
        /// Resets the score calculation for the run.
        /// </summary>
        public void ResetScore()
        {
            score = 0;
            oldXPos = player.transform.position.x;
        }
    }
}