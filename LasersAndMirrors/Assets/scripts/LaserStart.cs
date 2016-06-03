using UnityEngine;
using System.Collections;

public class LaserStart : MonoBehaviour {

	public GameObject laser;
	public float secs = 3;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine ("Wait");
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds (secs);
		laser.GetComponent<LaserScript> ().enabled = true;
		//laser.SetActive (true);
	}
}
