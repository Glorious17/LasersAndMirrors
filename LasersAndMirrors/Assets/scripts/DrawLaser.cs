using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLaser : MonoBehaviour {

    public GameObject laser;
    private int vertexCount = 1;
    private Vector3 start;
    private LineRenderer lr;
    private Vector3 normalV;

    Ray r;
    // Use this for initialization
    void Start () {
        lr = laser.GetComponent<LineRenderer>(); //Linerenderer zur Darstellung des Lasers
        lr.SetVertexCount(vertexCount); //Anzahl der Punkte des Lasers festlegen
        start = new Vector3(-2, 0, -13); //Startpunkt des Lasers

        r = new Ray(start, new Vector3(1.0f, 0.0f, 0.0f)); //Parameter: start -> Startposition des Rays, Vector3 -> Richtungsvektor
        startLaser();
    }

    public void drawLaser()
    {
        RaycastHit vHit;
        Vector3 nextVec = new Vector3(2,0,0);
        startLaser();
        Vector3 oldHitpoint;

        while (Physics.Raycast(r, out vHit, 40))//in den nächsten 40 Einheiten, wird überprüft, ob eine Kollision stattfindet
        {
            if(vHit.collider.gameObject.tag == "Mirror")
            {
                if (vHit.collider.gameObject.GetComponent<Degree>().angleUp())              
                    nextVec = new Vector3(10 * nextVec.z, nextVec.y, 10 * nextVec.x);               
                else
                    nextVec = new Vector3(-10 * nextVec.z, nextVec.y, -10 * nextVec.x);

                r = new Ray(vHit.point, nextVec);
                newCount(); //Anzahl der Vertices wird hoch gesetzt
                lr.SetPosition(vertexCount - 1, vHit.point); //neuer Vertex am Kollisionspunkt (hitpoint) wird vom Linerenderer gezeichnet

                Debug.Log("Schleif");
            }
        }
        newCount(); //Anzahl der Vertices wird erneut hochgesetzt, da jetzt an diesem Punkt der Laser vom Spiegel zurückgeworfen wird
        lr.SetPosition(vertexCount - 1, vHit.point + nextVec); //einen Punkt in die neue Richtung zeichnen 
        Debug.Log("nocollision! :-)");       
    }

    public void startLaser()
    {
        vertexCount = 1;
        lr.SetPosition(0, start);
        r = new Ray(start, new Vector3(1.0f, 0.0f, 0.0f));
    }

    private void newCount()
    {
        vertexCount++;
        lr.SetVertexCount(vertexCount);
    }
}
