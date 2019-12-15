using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runner
{
    public class InputManager : MonoBehaviour
    {
        //Where should the input manager ignore touch commands.
        [SerializeField] private RectTransform touchDeadZone;

        private Stack<InputReceiver> receivers = new Stack<InputReceiver>();
        private InputReceiver currentReceiver;

        private bool ignoreInputNextFrame = false;

        #region ReceiverRegistration
        /// <summary>
        /// Add a receiver to the stack, routing future input to it.
        /// </summary>
        /// <param name="receiver"></param>
        public void AddReceiver(InputReceiver receiver)
        {
            receivers.Push(receiver);

            //Update current receiver to the top item.
            currentReceiver = receivers.Peek();

            //Ignore input for a frame after input receiver changes
            ignoreInputNextFrame = true;
        }

        /// <summary>
        /// Remove a receiver from the stack if it is the top item.
        /// </summary>
        /// <param name="receiver"></param>
        public void RemoveReceiver(InputReceiver receiver)
        {
            if(receivers.Peek() == receiver)
            {
                receivers.Pop();
                //Update current receiver to the top item.
                currentReceiver = receivers.Peek();

                //Ignore input for a frame after input receiver changes
                ignoreInputNextFrame = true;
            }
        }
        #endregion

        private void Update()
        {
            //Skip input check for the frame.
            if (ignoreInputNextFrame)
            {
                ignoreInputNextFrame = false;
                return;
            }

#if UNITY_IOS || UNITY_ANDROID
            MobileInput();
#else
            WinInput();
#endif
        }

        private void MobileInput()
        {
            if(Input.touchCount > 0)
            {

                Touch t = Input.GetTouch(0);
                //Ignore input from deadzone.
                if(RectTransformUtility.RectangleContainsScreenPoint(touchDeadZone, t.position))
                {
                    return;
                }

                if(t.phase == TouchPhase.Ended)
                {
                    currentReceiver.OnJumpKeyUp();
                }
                else
                {
                    currentReceiver.OnJumpKey();
                }
            }
        }

        private void WinInput()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                currentReceiver.OnJumpKey();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                currentReceiver.OnJumpKeyUp();
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                currentReceiver.OnExitKeyUp();
            }
        }
    }
}