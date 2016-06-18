using UnityEngine;
using System.Collections;

public class Treadmill : MonoBehaviour {

	/*
	* This script emulates movement of the treadmill and every object that collides with the treadmill gets this scipt
	*/

    private float publicSpeed = 0;

	void Start () {
		publicSpeed = GameObject.Find("Treadmill").GetComponent<TreadmillRegulator>().publicSpeed;
	}
	
	void Update () {
        publicSpeed = GameObject.Find("Treadmill").GetComponent<TreadmillRegulator>().publicSpeed;
        transform.position = new Vector3(transform.position.x + publicSpeed*Time.deltaTime, transform.position.y, transform.position.z);
	}

}
