using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class BonusScore : Interactable
    {
        [SerializeField] private float bonusScore = 25;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            //Add score.
            ServiceLocator.ScoreKeeper.AddScore(bonusScore);

            //Turn this object off.
            //Items would be pooled and re-used, so it doesn't need to be destroyed.
            gameObject.SetActive(false);
        }
    }
}

