using UnityEngine;
using System.Collections;

public class IdleFloat : MonoBehaviour {

    float y = 0;
    public bool sine = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if(sine)
        transform.position = new Vector3(transform.position.x, 0.3f*Mathf.Sin(y) , transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, 0.35f * Mathf.Cos(y), transform.position.z);

        y += 0.04f;
	}
}
