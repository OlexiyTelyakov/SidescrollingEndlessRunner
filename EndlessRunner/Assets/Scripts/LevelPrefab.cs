using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class LevelPrefab : MonoBehaviour
    {
        [SerializeField] private Transform itemSpawnPoint;
        [SerializeField] private List<GameObject> itemsToSpawn;

        private void OnEnable()
        {
            int rng = Random.Range(0, itemsToSpawn.Count);

        }
    }
}