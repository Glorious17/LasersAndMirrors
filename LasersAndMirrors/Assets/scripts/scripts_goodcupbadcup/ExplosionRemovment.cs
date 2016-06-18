using UnityEngine;
using System.Collections;

public class ExplosionRemovment : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "Bomb(Clone)") {
          //  Debug.Log("Removed!");
            Destroy(other.gameObject.GetComponent<ExplosionScript>());
			//other.gameObject.GetComponent<ExplosionScript>().enabled = false;
		}
	}
}
