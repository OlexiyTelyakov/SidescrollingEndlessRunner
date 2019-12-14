using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Obstacle : Interactable
    {
        protected override void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            //Fail state.
            print("End Game");
        }
    }
}