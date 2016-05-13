using UnityEngine;
using System.Collections;

public class ButtonStart : MonoBehaviour
{

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
        {
            Camera.main.GetComponent<DrawLaser>().enabled = true;
        }

    }
}
