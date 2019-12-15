using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [Header("Header")]
        [SerializeField] private Text scoreAmountText;
        [SerializeField] private Text bestScoreAmountText;

        private Score score;

        private double displayBestScore;
        [Header("Bonus score display")]
        [SerializeField] private Text bonusScoreText;
        [SerializeField] private float bonusScoreDuration = 1.75f;
        private float bonusScoreTimer;

        // Start is called before the first frame update
        void Start()
        {
            //Score is consistently queried for data, so  it is cashed as opposed to using Service locator every time.
            score = ServiceLocator.ScoreKeeper;
            //Subscribe to the bonus score event list.
            score.onScoreAdded += OnScoreAdded;

            //Reset score display.
            scoreAmountText.text = "0 m";
            displayBestScore = System.Math.Round(score.GetBestScore(),1);
            bestScoreAmountText.text = string.Format("{0} m", displayBestScore);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateScoreBoard();
            UpdateBonusScore();
        }

        private void UpdateScoreBoard()
        {
            // Get score and round it for readability.
             double displayScore = System.Math.Round(score.GetScore(), 1);
             //Update score text.
             scoreAmountText.text = string.Format("{0} m", displayScore);

            //Update best score if it has been exceeded.
            displayBestScore = System.Math.Round(score.GetBestScore(), 1);
            if (displayScore >= displayBestScore)
            {
                bestScoreAmountText.text = scoreAmountText.text;
            }
        }

        private void UpdateBonusScore()
        {
            if (bonusScoreText.gameObject.activeInHierarchy)
            {
                //Increment the timer
                bonusScoreTimer += Time.deltaTime;
                if (bonusScoreTimer >= bonusScoreDuration)
                {
                    //Disable the timer once its duration expired.
                    bonusScoreText.gameObject.SetActive(false);
                }
            }
        }

        private void OnScoreAdded(float bonus)
        {
            //Enable text and set it to display the bonus.
            bonusScoreText.gameObject.SetActive(true);
            bonusScoreText.text = string.Format("+{0}", bonus);
            //Reset the timer.
            bonusScoreTimer = 0;
        }
    }
}

