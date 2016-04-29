using UnityEngine;
using System.Collections;

public class DrawLaser : MonoBehaviour {

    public GameObject laser;
    private int vertexCount = 1;
    private Vector3 start;
    private LineRenderer lr;

    private Vector3 normalV;

    private Ray ray;
    private RaycastHit hit;
    // Use this for initialization
    void Start () {
        hit = new RaycastHit();
        ray = new Ray();
        lr = laser.GetComponent<LineRenderer>();
        start = new Vector3(-2, -1.4f, -9);
        drawLine();
        normalV = new Vector3(1.0f,0,-1.0f);
        normalV = normalV.normalized;

    }
	
	// Update is called once per frame
	void Update () {
        vertexCount = 1;
        
        ray.origin = start;
        ray.direction = new Vector3(1, 0, 0);
        
        lr.SetVertexCount(vertexCount);
        lr.SetPosition(0, start);

        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 hitpoint = hit.point;
            newCount();
            lr.SetVertexCount(vertexCount);
            lr.SetPosition(vertexCount-1, hitpoint);
            newCount();
            GameObject go = hit.collider.gameObject;
            Vector3 nextvec = Vector3.Reflect(start, normalV); //Spiegelung anhand eines Vector funktioniert noch nicht
            lr.SetPosition(vertexCount-1, nextvec);
            Debug.Log(Vector3.Angle(start, nextvec));
        }
    }

    void drawLine()
    {        
        lr.SetVertexCount(vertexCount);
        lr.SetPosition(0, start);        
    }

    private void newCount()
    {
        vertexCount++;
        lr.SetVertexCount(vertexCount);
    }
}
