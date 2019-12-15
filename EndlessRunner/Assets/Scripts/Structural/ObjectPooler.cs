using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    /// <summary>
    /// Generator class that will instantiate a number of objects in the level on start up and provides access to them.
    /// </summary>
    public class ObjectPooler : MonoBehaviour
    {
        //Level prefabs that will be created.
        [SerializeField] private List<GameObject> objectsToPool;
        //Number of objects to create
        [SerializeField] private int instantiateAmount = 3;

        private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

        //Create the object pool
        private void Awake()
        {
            //Iterate through objects to pool and create a separate pool for each.
            for(int i = 0; i < objectsToPool.Count; i++)
            {
                //Initialize the dictionary entry for this object type.
                objectPool.Add(objectsToPool[i].name, new Queue<GameObject>());

                for(int j = 0; j < instantiateAmount; j++)
                {
                    //Instantiate the object.
                    GameObject go = Instantiate(objectsToPool[i]);
                    go.SetActive(false);
                    //Add it to the queue.
                    objectPool[objectsToPool[i].name].Enqueue(go);
                }
            }
        }

        //Getter method that returns a game object from the pool.
        public GameObject Retrieve(string objectName)
        {
            if (objectPool.ContainsKey(objectName))
            {
                //Get the object from the queue.
                GameObject go = objectPool[objectName].Dequeue();
                //Return the object to the end of the queue so it can be reused .
                objectPool[objectName].Enqueue(go);
                go.SetActive(true);

                return go;
            }
            return null;
        }

        /// <summary>
        /// Deactivates all objects handled by the ObjectPooler so they can be completely reused.
        /// </summary>
        public void ResetObjectPool()
        {
            //Iterate through the dictionary and deactivate all objects.
            for(int i = 0; i < objectsToPool.Count; i++)
            {
                foreach(GameObject go in objectPool[objectsToPool[i].name])
                {
                    go.SetActive(false);
                }
            }
        }
    }
}