using UnityEngine;
using System.Collections;

public class Degree : MonoBehaviour {

    //Anhand der Rotation ausgehend davon, dass nur in 45 Grad rotiert wird, wird der Normalenvektor bestimmt
    public Vector3 getNormal()
    {
        Quaternion deg = transform.rotation;

        if (Quaternion.Euler(0, 0.0f, 0) == deg || Quaternion.Euler(0, 360.0f, 0) == deg || Quaternion.Euler(0, 180.0f, 0) == deg)
            return new Vector3(-10.0f, 0.0f, 0.0f);
        else if (Quaternion.Euler(0, 45.0f, 0) == deg || Quaternion.Euler(0, 225.0f, 0) == deg)
            return new Vector3(0.0f, 0.0f, 10.0f);
        else if (Quaternion.Euler(0, 90.0f, 0) == deg || Quaternion.Euler(0, 270.0f, 0) == deg)
            return new Vector3(10.0f, 0.0f, 0.0f);
        else if (Quaternion.Euler(0, 135.0f, 0) == deg || Quaternion.Euler(0, 315.0f, 0) == deg)
            return new Vector3(0.0f, 0.0f, -10.0f);
        else
            return new Vector3(0.0f, 0.0f, 0.0f);
    }
}
