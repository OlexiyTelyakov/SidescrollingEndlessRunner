using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class LevelBuilder : MonoBehaviour
    {
        [Header("Level Geometry")]
        [SerializeField] private GameObject[] levelPrefabs;
        [SerializeField] private Transform startPoint;
        [SerializeField] private int startingPlatforms = 3;

        [SerializeField] private GameObject[] itemPrefabs;

        private CharacterController player;

        private Vector3 spawnPos;

        // Start is called before the first frame update
        void Start()
        {
            player = ServiceLocator.Player;

            //Spawn position is start point + 10 units (standard length of a level piece).
            spawnPos = startPoint.position + new Vector3(10,0,0);

            //Generate the first 3 platforms.
            for(int i = 0; i < startingPlatforms; i++)
            {
                AddLevel();
            }
        }

        //Builder method that adds platforms to the runner.
        private void AddLevel()
        {
            //Get name of the object for the ObjectPooler.
            int rng = Random.Range(0, levelPrefabs.Length);
            string key = levelPrefabs[rng].name;
            //Place the object in the correct position.
            GameObject levelPrefab = ServiceLocator.ObjectPooler.Retrieve(key);
            levelPrefab.transform.position = spawnPos;

            //Activate platform specific logic.
            levelPrefab.GetComponent<LevelPrefab>().OnActivate();

            //Move the new spawn position.
            spawnPos.x += 10f;
        }

        private void Update()
        {
            //If player is within 1.5 platforms away, add the next level.
            if(Mathf.Abs(spawnPos.x - player.transform.position.x) < 15f)
            {
                AddLevel();
            }
        }

        //Returns an interactable from a list of items.
        public GameObject GetItem()
        {
            int rng = Random.Range(0, itemPrefabs.Length);
            string key = itemPrefabs[rng].name;
            GameObject item = ServiceLocator.ObjectPooler.Retrieve(key);
            return item;
        }

        /// <summary>
        /// Resets the level so it can be generated from the start.
        /// </summary>
        public void ResetLevel()
        {
            spawnPos = startPoint.position + new Vector3(10, 0, 0);

            //Generate the first 3 platforms.
            for (int i = 0; i < startingPlatforms; i++)
            {
                AddLevel();
            }
        }
    }
}