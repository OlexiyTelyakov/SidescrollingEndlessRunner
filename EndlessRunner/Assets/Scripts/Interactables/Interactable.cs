using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    /// <summary>
    /// Base class for all interactable objects.
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        //Abstract method that will be overriden in inheriting classes for custom functionality.
        protected abstract void OnTriggerEnter2D(UnityEngine.Collider2D collision);
    }
}