using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class LevelPrefab : MonoBehaviour
    {
        [SerializeField] private Transform itemSpawnPoint;

        public void OnActivate()
        {
            if (itemSpawnPoint == null) return;
            //Get an interactable
            GameObject go = ServiceLocator.LevelBuilder.GetItem();
            go.transform.position = itemSpawnPoint.position;
        }
    }
}