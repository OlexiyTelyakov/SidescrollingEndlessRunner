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
        private PlayerInput player;
        private float oldXPos;

        private float score;

        // Start is called before the first frame update
        void Start()
        {
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
        }

        //Getter function for other classes to access the score value.
        public float GetScore()
        {
            return score;
        }
    }
}