using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Runner
{
    /// <summary>
    /// Class that keeps track of the score.
    /// </summary>
    public class Score : MonoBehaviour
    {
        public delegate void ScoreAdded(float score);
        public event ScoreAdded onScoreAdded;

        private CharacterController player;
        private float oldXPos;

        private float score;
        private float bestScore;

        void Awake()
        {
            //Try to get previous best score.
            bestScore = LoadBestScore();

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
                SaveBestScore();
            }
        }

        public void AddScore(float bonus)
        {
            score += bonus;

            //Notify event subscribers that bonus score has been added.
            onScoreAdded?.Invoke(bonus);
        }

        /// <summary>
        /// Resets the score calculation for the run.
        /// </summary>
        public void ResetScore()
        {
            score = 0;
            UpdateBestScore();
            oldXPos = player.transform.position.x;
        }

        #region JsonScoreKeeping
        private float LoadBestScore()
        {
            //Create return value.
            float _bestScore = 0;
            //Get the path to 'save file'.
            string path = Application.persistentDataPath + "/bestScore.txt";

            if (File.Exists(path))
            {
                //Get json data.
                string data = File.ReadAllText(path);
                //Deserealize it into an object and get best score.
                _bestScore = JsonUtility.FromJson<BestScore>(data).bestScore;
            }
            //If no prior save file exists.
            else
            {
                //Create the container object.
                BestScore jsonScore = new BestScore(_bestScore);
                //Serialize the object to json.
                string data = JsonUtility.ToJson(jsonScore);
                //Create the 'save file'.
                File.WriteAllText(path, data);
            }

            return _bestScore;
        }

        private void SaveBestScore()
        {
            //Get the path to 'save file'.
            string path = Application.persistentDataPath + "/bestScore.txt";
            BestScore jsonScore = new BestScore(bestScore);
            //Convert bestScore to json.
            string data = JsonUtility.ToJson(jsonScore);

            //Save the data. WriteAllText creates new file if it cannot find an existing one, so Exists check isn't necessary.
            File.WriteAllText(path, data);
        }

        //Object that holds the score value for JSON serialization.
        private class BestScore
        {
            public float bestScore;

            public BestScore(float bestScore)
            {
                this.bestScore = bestScore;
            }
        }
        #endregion
    }
}