using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DrawLaser : MonoBehaviour
{
    int pointCounter = 0;
    Vector3 nextVec;

	/*
	 * Bugs: Checkpoints verhalten sich wie Hidnernisse
	 */

    public GameObject laser; //später Laserkanone, zur Zeit leeres GameObject. Muss einen Linerenderer besitzen
    private int vertexCount = 2; //für den Linerenderer nötig, gibt an wie viele Punkte(Strecken) der Laser besitzt
    private Vector3 start; //Ausgangspunkt des Lasers, später: Position der Laserkanone
    private LineRenderer lr;
    private Vector3 origin, dT; //Animationsvektoren
    float scaling;
    private Vector3 speed = new Vector3(0, 0, 0f);
    List<RaycastHit> points; //Liste mit Kollisionspunkten
    Ray r;
    // Use this for initialization
    void Start()
    {
        Vector3 laserPos = (GameObject.Find("LaserStart")).transform.position;
        lr = laser.GetComponent<LineRenderer>(); //Linerenderer zur Darstellung des Lasers
        lr.SetVertexCount(vertexCount); //Anzahl der Punkte des Lasers festlegen

        //wenn Laserkanone aus anderer Richtung schießen soll, hier den Richtungsvektor ändern!
        nextVec = new Vector3(2, 0, 0); //erste Richtung des Lasers
        start = new Vector3(laserPos.x + 1, 0, laserPos.z); //Startpunkt des Lasers
        r = new Ray(start, nextVec); //Parameter: start -> Startposition des Rays, nextVec -> Richtungsvektor

        scaling = 0.015f; //wenn der Laser schneller animiert werden soll, hier rumschrauben
        dT = new Vector3(0.04f, 0, 0); //Initialgeschwindigkeit des Lasers, wird nach erster Kollision überschrieben
        origin = start; //Animation hat ihren Ursprung beim Startpunkt des Lasers

        points = new List<RaycastHit>();
        collisionLaser();//Kollisionsberechnung des Laserstrahls (kein Zeichnen)
        //Zeichnen des Lasers findet in der FixedUpdate()-Methode statt
    }

    private Vector3 newDirection(Vector3 incoming, Vector3 normal) //Reflektieren des Lasers an einem Spiegel
    {//Formel funktioniert, nicht mehr anfassen!
        return incoming - 2 * (Vector3.Dot(incoming, normal) / Vector3.Dot(normal, normal)) * normal;
    }
    public void startLaser()
    {
        lr.SetPosition(0, start);
        r = new Ray(start, new Vector3(1.0f, 0.0f, 0.0f));
    }
    private void newCount() //eine neue Strecke für den Laser möglich machen
    {
        vertexCount++;
        lr.SetVertexCount(vertexCount);
    }
    public void resetLaser()
    {
        scaling = 0.015f;
        dT = new Vector3(0.04f, 0, 0);
        points = new List<RaycastHit>();
        lr = laser.GetComponent<LineRenderer>(); //Linerenderer zur Darstellung des Lasers
        lr.SetVertexCount(vertexCount); //Anzahl der Punkte des Lasers festlegen
        start = new Vector3(-6, 0, -13); //Startpunkt des Lasers
        origin = start;
        r = new Ray(start, new Vector3(1.0f, 0.0f, 0.0f)); //Parameter: start -> Startposition des Rays, Vector3 -> Richtungsvektor
    } //Zurücksetzen aller Variablen im Skript (neues Zeichnen eines Laserstrahls möglich)

    public void collisionLaser()
    {
        startLaser();
        RaycastHit vHit;

        points = new List<RaycastHit>();

        while (Physics.Raycast(r, out vHit, 40))//in den nächsten 40 Einheiten, wird überprüft, ob eine Kollision stattfindet
        {
            if (vHit.collider.gameObject.tag == "Mirror")
            {
                points.Add(vHit);
                Vector3 revlector = vHit.collider.gameObject.GetComponent<Degree>().normalVector(); //Vektor, an dem der eingehende Vektor reflektiert wird
                nextVec = newDirection(nextVec, revlector); //neue Richtung des Vektors
                /*alte Collsionsabfrage mit nur zwei Winkelpositionen
                if (vHit.collider.gameObject.GetComponent<Degree>().angleUp())
                    nextVec = new Vector3(10 * nextVec.z, nextVec.y, 10 * nextVec.x);
                else
                    nextVec = new Vector3(-10 * nextVec.z, nextVec.y, -10 * nextVec.x);*/

                r = new Ray(vHit.point, nextVec);

                //wenn der Laser schon während dem Platzieren der Spiegel gezeichnet werden soll, 
                //die nächsten beiden Zeilen auskommentieren und weiter unten noch was

                //newCount(); //Anzahl der Vertices wird hoch gesetzt
                //lr.SetPosition(vertexCount - 1, vHit.point); //neuer Vertex am Kollisionspunkt (hitpoint) wird vom Linerenderer gezeichnet
            }
            else
            {
                points.Add(vHit);
                if (vHit.collider.gameObject.tag == "finalpoint") //Endpunkt wurde erreicht
                    Debug.Log("SUCCESSSS!!!");
                else //alles außer Spiegel oder Endpunkt wurden errecicht = FAIL
                    Debug.Log("loser!");
                break; //Schleifenabbruch, da sonst Endlosschleife und Programmabsturz
            }
        }
        //GENAU DAS HIER noch :-)
        //newCount();
        //lr.SetPosition(vertexCount - 1, vHit.point + nextVec); //einen Punkt in die neue Richtung zeichnen 
    }

    private void changeSpeed(RaycastHit hit)
    {
        Vector3 normal = new Vector3(0, 0, 0);
        if (hit.collider.gameObject.tag == "Mirror") //nur wenn ein Spiegel getroffen wird, muss eine neue Richtung berechnet werden
        {
            normal = hit.collider.gameObject.GetComponent<Degree>().normalVector(); //Normalenvektor der Fläche bestimmen
            dT = newDirection(speed, normal) * scaling; //neue Richtung berechnen und zur Animation skalieren
            //der dT-Vektor ist aber nur eine skalierte Version des Vektors, da dT zur Animation der Strecke dient
        }
		else if(hit.collider.gameObject.tag == "Wall")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if(hit.collider.gameObject.tag == "Checkpoint")
		{
			//do something
		}
        else
        {
            dT = (hit.point - origin) * scaling; //Strecke zwischen dem Ausgangspunkt (origin) und dem kommenden Kollisionspunkt wird berechnet
            //der dT-Vektor ist aber nur eine skalierte Version des Vektors, da dT zur Animation der Strecke dient
        }
        speed = new Vector3(0, 0, 0); //reset des Speedvectors
    }
    public void FixedUpdate()
    {
        if (pointCounter < points.Count) //solange noch Kollisionspunkte vorhanden sind 
        {
            speed = speed + dT;
            lr.SetPosition(pointCounter + 1, origin + speed); //Zeichnen des Lasers

            //wenn die Länge von der zurückgelegten Strecke (speed) größer als die Länge zwischen Ursprung (origin) und Kollisionspunkt ist
            if (Vector3.Magnitude(speed) >= Vector3.Magnitude(points[pointCounter].point - origin))
            {
                newCount();
                changeSpeed(points[pointCounter]); //Richtung des Geschwindgkeitsvektors ändern
                origin = points[pointCounter].point; //alter Kollisionspunkt wird nun zum neuen Punkt, von dem aus der Laser weitergezeichnet wird
                pointCounter++;
            }
        }
        else //letzte Strecke (kein Spiegel mehr) 
        {
            lr.SetPosition(pointCounter + 1, points[points.Count - 1].point);
            GameObject.Find("LaserStart").GetComponent<DrawLaser>().enabled = false; //Laserskript ausschalten (nichts wird mehr gezeichnet)
            resetLaser(); //Laservariablen zurücksetzen (kann noch unvollständig sein)
        }
    }
}
