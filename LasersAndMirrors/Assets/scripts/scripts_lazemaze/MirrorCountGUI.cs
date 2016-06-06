using UnityEngine;
using System.Collections;

public class MirrorCountGUI : MonoBehaviour {

	void OnGUI(){
		int count = GameObject.Find ("Plane").GetComponent<Grid>().mirrorCount;
		int max = GameObject.Find ("Plane").GetComponent<Grid>().maxMirror;

		GUI.Box (new Rect (0, 0, 100, 20), count + " von " + max);
	}
}
