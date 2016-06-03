using UnityEngine;
using System.Collections;

public class LockRotation : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
	}
}
