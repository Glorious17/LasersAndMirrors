using UnityEngine;
using System.Collections;

public class Degree : MonoBehaviour {

    //Anhand der Rotation ausgehend davon, dass nur in 45 Grad rotiert wird, wird der Normalenvektor bestimmt
    public bool angleUp()
    {
        Quaternion deg = transform.rotation;
        bool result = false;

        if (Quaternion.Euler(0, 45.0f, 0) == deg || Quaternion.Euler(0, 225.0f, 0) == deg)
            result = true;
        //return new Vector3(0.0f, 0.0f, 10.0f);;
        //return new Vector3(0.0f, 0.0f, -10.0f);

        return result;
    }
}
