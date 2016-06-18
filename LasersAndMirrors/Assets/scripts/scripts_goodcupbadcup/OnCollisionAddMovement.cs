using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
* Gameobjects on the Treadmill get Movement from Treadmill-Script
* Gameobjects who fall off the Treadmill loose Movementspeed 
*/

public class OnCollisionAddMovement : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        GameObject go = col.gameObject;
        go.AddComponent<Treadmill>();
    }

    void OnCollisionExit(Collision col)
    {
        GameObject go = col.gameObject;
        Destroy(go.GetComponent<Treadmill>());
    }
}
