using UnityEngine;
using System.Collections;

public class ButtonStart : MonoBehaviour
{
	public GameObject laserStart;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
        {
			laserStart.GetComponent<DrawLaser>().enabled = true;
			//Camera.main.GetComponent<DrawLaser>().enabled = true;
        }

    }
}
