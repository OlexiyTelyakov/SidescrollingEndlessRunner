using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class BonusScore : Interactable
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            //Add score.
            ServiceLocator.ScoreKeeper.AddScore(50f);

            //Notify the object pooler to deactivate this object.
        }
    }
}

