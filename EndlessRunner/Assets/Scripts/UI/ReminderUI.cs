using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class ReminderUI : MonoBehaviour
    {
        [SerializeField] private GameObject mobileReminder;
        [SerializeField] private GameObject WinReminder;

        // Start is called before the first frame update
        void Start()
        {
#if UNITY_IOS || UNITY_ANDROID
            mobileReminder.SetActive(true);
#else
            WinReminder.SetActive(true);
#endif
        }
    }
}