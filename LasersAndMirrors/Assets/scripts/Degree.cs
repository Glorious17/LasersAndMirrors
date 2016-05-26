using UnityEngine;
using System.Collections;

public class Degree : MonoBehaviour
{

    //Anhand der Rotation ausgehend davon, dass nur in 45 Grad rotiert wird, wird der Normalenvektor bestimmt
    public bool angleUp()
    {
        Quaternion deg = transform.rotation;
        bool result = false;

        if (Quaternion.Euler(0, 45.0f, 0) == deg || Quaternion.Euler(0, 225.0f, 0) == deg)
            result = true;
        //return new Vector3(0.0f, 0.0f, 10.0f);
        //return new Vector3(0.0f, 0.0f, -10.0f);

        return result;
    }


    public Vector3 normalVector()
    {
        Quaternion deg = transform.rotation;
        if (Quaternion.Euler(0, 0.0f, 0) == deg || Quaternion.Euler(0, 180, 0) == deg)
            return new Vector3(-1, 0, 0);
        else if (Quaternion.Euler(0, 22.5f, 0) == deg || Quaternion.Euler(0, 202.5f, 0) == deg)
            return new Vector3(-2.5f, 0, 1);
        else if (Quaternion.Euler(0, 45.0f, 0) == deg || Quaternion.Euler(0, 225.0f, 0) == deg)
            return new Vector3(-1, 0, 1);
        else if (Quaternion.Euler(0, 67.5f, 0) == deg || Quaternion.Euler(0, 247.5f, 0) == deg)
            return new Vector3(-1, 0, 2.5f);
        else if (Quaternion.Euler(0, 90.0f, 0) == deg || Quaternion.Euler(0, 270f, 0) == deg)
            return new Vector3(0, 0, 1);
        else if (Quaternion.Euler(0, 112.5f, 0) == deg || Quaternion.Euler(0, 292.5f, 0) == deg)
            return new Vector3(1, 0, 2.5f);
        else if (Quaternion.Euler(0, 135.0f, 0) == deg || Quaternion.Euler(0, 315f, 0) == deg)
            return new Vector3(1, 0, 1);
        else if (Quaternion.Euler(0, 157.5f, 0) == deg || Quaternion.Euler(0, 337.5f, 0) == deg)
            return new Vector3(2.5f, 0, 1);
        else
            return new Vector3(0, 0, 0);
    }
}
