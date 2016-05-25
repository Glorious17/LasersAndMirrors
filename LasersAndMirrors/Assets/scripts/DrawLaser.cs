﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLaser : MonoBehaviour
{
    int pointCounter = 0;
    public GameObject laser;
    private int vertexCount = 2;
    private Vector3 start;
    private LineRenderer lr;
    private Vector3 normalV, origin, dT;
    private Vector3 speed = new Vector3(0, 0, 0f);
    List<RaycastHit> points;
    Ray r;

    // Use this for initialization
    void Start()
    {
        dT = new Vector3(0.01f, 0, 0);
        points = new List<RaycastHit>();
        lr = laser.GetComponent<LineRenderer>(); //Linerenderer zur Darstellung des Lasers
        lr.SetVertexCount(vertexCount); //Anzahl der Punkte des Lasers festlegen
        //start = new Vector3(-2, 0, -13); //Startpunkt des Lasers
		start = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z); //Startpunkt des Lasers
        origin = start;
        r = new Ray(start, new Vector3(1.0f, 0.0f, 0.0f)); //Parameter: start -> Startposition des Rays, Vector3 -> Richtungsvektor
        collisionLaser();
    }

    public void collisionLaser()
    {
        startLaser();
        RaycastHit vHit;
        Vector3 nextVec = new Vector3(2, 0, 0);

        while (Physics.Raycast(r, out vHit, 40))//in den nächsten 40 Einheiten, wird überprüft, ob eine Kollision stattfindet
        {
            if (vHit.collider.gameObject.tag == "Mirror")
            {
                points.Add(vHit);
                if (vHit.collider.gameObject.GetComponent<Degree>().angleUp())
                    nextVec = new Vector3(10 * nextVec.z, nextVec.y, 10 * nextVec.x);
                else
                    nextVec = new Vector3(-10 * nextVec.z, nextVec.y, -10 * nextVec.x);

                r = new Ray(vHit.point, nextVec);
                //newCount(); //Anzahl der Vertices wird hoch gesetzt
                //lr.SetPosition(vertexCount - 1, vHit.point); //neuer Vertex am Kollisionspunkt (hitpoint) wird vom Linerenderer gezeichnet
            }
        }
        //newCount();
        //lr.SetPosition(vertexCount - 1, vHit.point + nextVec); //einen Punkt in die neue Richtung zeichnen 
    }

    public void startLaser()
    {
        lr.SetPosition(0, start);
        r = new Ray(start, new Vector3(1.0f, 0.0f, 0.0f));
    }

    private void newCount()
    {
        vertexCount++;
        lr.SetVertexCount(vertexCount);
    }

    private void changeSpeed(RaycastHit hit)
    {
        speed = new Vector3(0, 0, 0);
        if (hit.collider.gameObject.GetComponent<Degree>().angleUp())
            dT = new Vector3(dT.z, dT.y, dT.x);
        else
            dT = new Vector3(-dT.z, dT.y, -dT.x);
    }
    public void FixedUpdate()
    {

        speed = speed + dT;
        lr.SetPosition(pointCounter + 1, origin + speed);
        if (Vector3.Magnitude(speed) >= Vector3.Magnitude(points[pointCounter].point - origin))
        {
            newCount();
            origin = points[pointCounter].point;
            changeSpeed(points[pointCounter]);
            pointCounter++;
        }
    }
}
