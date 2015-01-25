using UnityEngine;
using System.Collections;

public class DeathGrill : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !Level.ShouldRestart)
        {
            Level.ShouldRestart = true;
            // could do coroutine here to fade out or whatever
        }
    }
}
