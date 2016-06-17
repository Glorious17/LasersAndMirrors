using UnityEngine;
using System.Collections;

public class BolzenRotation : MonoBehaviour {

	public float speed;
	
	/*
	* objects which are attached with this script will rotate
	*/

	void Update () 
	{
		transform.Rotate(0, speed*Time.deltaTime, 0);
	}
}
