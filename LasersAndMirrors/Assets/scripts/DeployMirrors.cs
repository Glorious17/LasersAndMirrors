using UnityEngine;
using System.Collections;

public class DeployMirrors : MonoBehaviour {

    public GameObject mirror;
    private float z = 2.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !Camera.main.GetComponent<RotateMirrors>().Rotation)
        {
            Vector3 spawnPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
            Vector3 pos = Camera.main.ScreenToWorldPoint(spawnPos);
            Instantiate(mirror, pos, Quaternion.identity);

            //Camera.main.getComponent<RotateMirrors>().Rotation = false;
        }
	}
}
