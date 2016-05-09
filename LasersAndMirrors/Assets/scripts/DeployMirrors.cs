using UnityEngine;
using System.Collections;

public class DeployMirrors : MonoBehaviour {

	//wird nicht mehr benötigt!
    public GameObject mirror;
    private float z = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !Camera.main.GetComponent<RotateMirrors>().Rotation)
        {
            Vector3 spawnPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
            Vector3 pos = Camera.main.ScreenToWorldPoint(spawnPos);
            Instantiate(mirror, pos, Quaternion.Euler(0, 45, 0));

            Camera.main.GetComponent<DrawLaser>().drawLaser();
        }
	}
}
