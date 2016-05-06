using UnityEngine;
using System.Collections;

public class RotateMirrors : MonoBehaviour {

    private GameObject go;
    private bool rotation;


    public bool Rotation
    {
        get
        {
            return rotation;
        }
        set
        {
            rotation = value;
        } 
    }
	// Use this for initialization
	void Start () {
        rotation = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            rotation = false;
            RaycastHit vHit = new RaycastHit();
            Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(vRay, out vHit, 1000)){
                if (vHit.collider.gameObject.tag == "MirrorTrigger")
                {
                    go = vHit.collider.gameObject;
                    rotation = true;
                    go.transform.parent.Rotate(0, 90.0f, 0);
                }
            }
            Camera.main.GetComponent<DrawLaser>().startLaser();
            Camera.main.GetComponent<DrawLaser>().drawLaser();
        }
    }


}
