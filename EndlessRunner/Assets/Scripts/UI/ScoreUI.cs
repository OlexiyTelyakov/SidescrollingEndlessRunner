using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private Text scoreAmountText;
        [SerializeField] private Text bestScoreAmountText;

        private Score score;

        private double displayBestScore;

        // Start is called before the first frame update
        void Start()
        {
            score = ServiceLocator.ScoreKeeper;

            //Reset score display.
            scoreAmountText.text = "0 m";
            displayBestScore = System.Math.Round(score.GetBestScore(),1);
            bestScoreAmountText.text = string.Format("{0} m", displayBestScore);
        }

        // Update is called once per frame
        void Update()
        {
            //Get score and round it for readability.
            double displayScore = System.Math.Round(score.GetScore(),1);
            //Update score text.
            scoreAmountText.text = string.Format("{0} m", displayScore);

            //Update best score if it has been exceeded.
            if(displayScore > displayBestScore)
            {
                bestScoreAmountText.text = scoreAmountText.text;
            }
        }
    }
}

