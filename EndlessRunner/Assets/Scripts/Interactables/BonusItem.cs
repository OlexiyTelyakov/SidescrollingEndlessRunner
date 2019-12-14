using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class BonusItem : Interactable
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            //Add score.
            print("Added score");
        }
    }
}

