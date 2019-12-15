using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class SpeedBonus : Interactable
    {
        [SerializeField] private float speedBonus = 0.5f;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            ServiceLocator.Player.BoostSpeed(speedBonus);

            gameObject.SetActive(false);
        }
    }
}