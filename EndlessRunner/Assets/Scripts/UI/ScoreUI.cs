using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private Text scoreAmountText;
        private Score score;

        // Start is called before the first frame update
        void Start()
        {
            scoreAmountText.text = "0 m";
            score = ServiceLocator.ScoreKeeper;
        }

        // Update is called once per frame
        void Update()
        {
            //Get score and round it for readability.
            double displayScore = System.Math.Round(score.GetScore(),1);
            //Update score text.
            scoreAmountText.text = string.Format("{0} m", displayScore);
        }
    }
}

