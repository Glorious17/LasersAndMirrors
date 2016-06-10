using UnityEngine;
using System.Collections;

public class ResetPP : MonoBehaviour {

	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt ("1_points", 0);
		PlayerPrefs.SetInt ("2_points", 0);
		PlayerPrefs.SetInt ("3_points", 0);
	}
}
