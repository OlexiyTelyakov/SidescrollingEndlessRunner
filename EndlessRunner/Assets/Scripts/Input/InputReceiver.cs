using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{

}
public abstract class InputReceiver : MonoBehaviour
{
    public virtual void OnJumpKey() { }

    public virtual void OnJumpKeyUp() { }
}
