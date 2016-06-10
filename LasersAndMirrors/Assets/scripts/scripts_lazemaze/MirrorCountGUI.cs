using UnityEngine;
using System.Collections;

public class MirrorCountGUI : MonoBehaviour {

	private float faktorx;
	private float faktory;

	private float padding;

	public GUIStyle customGui;

	void Start()
	{
		faktorx = Screen.width / 100;
		faktory = Screen.height / 50;

		padding = Screen.width / 1.2f;

		customGui.fontSize = Screen.width / 30;
	}

	void OnGUI(){
		
		int count = GameObject.Find ("Plane").GetComponent<Grid>().mirrorCount;
		int max = GameObject.Find ("Plane").GetComponent<Grid>().maxMirror;
		int c_count = GameObject.Find ("LaserStart").GetComponent<LaserScript>().c_counter;

		GUI.Box (new Rect (faktory, faktorx, 0, 0), count + " von " + max, customGui);
		GUI.Box (new Rect (padding, faktorx, 0, 0), "Punkte: " + c_count, customGui);
	}
}
