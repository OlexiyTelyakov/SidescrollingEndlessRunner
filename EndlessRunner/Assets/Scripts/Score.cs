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
        [SerializeField] private PlayerInput input;
        private float score;

        // Start is called before the first frame update
        void Start()
        {
            score = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}