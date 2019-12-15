using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private CharacterController target;
        [SerializeField] private float horizontalOffset;

        private void LateUpdate()
        {
            //Very simple camera follow as camera stays fixed on the Y and does not need to adjust for facing and movement in different directions.
            transform.position = new Vector3(target.transform.position.x + horizontalOffset, transform.position.y, transform.position.z);
        }
    }
}