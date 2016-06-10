using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LaserScript : MonoBehaviour {
	
	private Vector3 start; //Ausgangspunkt des Lasers, später: Position der Laserkanone
	private LineRenderer lr;
	private Vector3 origin, dT, nextVec, speed = new Vector3(0, 0, 0f); //Animationsvektoren
	Ray r;
	int pointCounter;

	public float animationVelocity; //Geschwindgkeit zur Animation BITTE, später noch private machen
	private bool raycasting;
	private int vertexCount = 2;

	public int c_counter;

	public string endgame;
	
	// Use this for initialization
	void Start () {

		c_counter = PlayerPrefs.GetInt("3_points");

		//Wird schneller je mehr Punkte man besitzt
		float speedChange = 0.01f;
		animationVelocity += speedChange * c_counter;

		raycasting = true;
		pointCounter = 0;
		Vector3 laserPos = transform.position;
		lr = GetComponent<LineRenderer>(); //Linerenderer zur Darstellung des Lasers
		lr.SetVertexCount(vertexCount); //Anzahl der Punkte des Lasers festlegen
		
		//wenn Laserkanone aus anderer Richtung schießen soll, hier den Richtungsvektor ändern!
		nextVec = new Vector3(1, 0, 0); //erste Richtung des Lasers
		start = new Vector3(laserPos.x, 0, laserPos.z); //Startpunkt des Lasers
		
		r = new Ray(start, nextVec); //Parameter: start -> Startposition des Rays, nextVec -> Richtungsvektor
		lr.SetPosition(0, start);
		
		dT = nextVec * animationVelocity; //Initialgeschwindigkeit des Lasers, wird nach erster Kollision überschrieben
		Debug.Log (Vector3.Magnitude (dT) + " Länge von dem V-Vektor");
		origin = start; //Animation hat ihren Ursprung beim Startpunkt des Lasers
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		RaycastHit vHit = new RaycastHit();
		
		if (raycasting) 
		{
			if (Physics.Raycast (r, out vHit, 100)) {//in den nächsten 100 Einheiten, wird überprüft, ob eine Kollision stattfindet
				speed = speed + dT; //Animationsschritte des Lasers
				lr.SetPosition (pointCounter + 1, origin + speed); //Zeichnen des Lasers
				
				if (vHit.collider.gameObject.tag == "Mirror"
					|| vHit.collider.gameObject.tag == "Checkpoint") {
					//wenn die Länge von der zurückgelegten Strecke (speed) größer als die Länge zwischen Ursprung (origin) und Kollisionspunkt ist
					if (Vector3.Magnitude (speed) >= Vector3.Magnitude (vHit.point - origin)) {

						if (vHit.collider.gameObject.tag == "Mirror") {
							vHit.collider.gameObject.GetComponent<Degree> ().disableMirrorRot (); //wenn Laser mit Spiegel kollidiert, dann darf Spiegel nicht mehr rotiert werden
							newCount ();
							Vector3 revlector = vHit.collider.gameObject.GetComponent<Degree> ().normalVector (); //Vektor, an dem der eingehende Vektor reflektiert wird
							nextVec = (newDirection (nextVec, revlector)).normalized; //neue Richtung des Vektors
							origin = vHit.point; //alter Kollisionspunkt wird nun zum neuen Punkt, von dem aus der Laser weitergezeichnet wird
							vHit.collider.gameObject.GetComponent<Degree> ().disableMirrorRot ();
							dT = nextVec * animationVelocity; //neue Richtung berechnen und zur Animation skalieren

							Debug.Log ("Mirror");
											
							speed = new Vector3 (0, 0, 0);
							pointCounter++;
						}
						if (vHit.collider.gameObject.tag == "Checkpoint") {

							Debug.Log ("Check");
							c_counter++;
							PlayerPrefs.SetInt ("3_points", c_counter);
							Debug.Log ("Pounts " + c_counter);
						}

						r = new Ray (vHit.point, nextVec); //neuen Raycast ausrichten
					}
				}
				else
				{if (Vector3.Magnitude (speed) >= Vector3.Magnitude (vHit.point - origin))
					if (Vector3.Magnitude (speed) >= Vector3.Magnitude (vHit.point - origin)){
						raycasting = false;
						if(vHit.collider.gameObject.tag == "finalpoint"){
							Debug.Log ("Sie haben ihr Ziel erreicht!");
							//do things if you have won

							//Resette Level
							SceneManager.LoadScene(SceneManager.GetActiveScene().name);
						}
						else{
							Debug.Log ("Na das geht doch auch besser!");
							//do things if you are a disgrace to all of your ancestors and your pet

							//Zum Testen
							SceneManager.LoadScene(endgame);
						}
					}
					dT = (vHit.point - origin).normalized * animationVelocity; //Strecke zwischen dem Ausgangspunkt (origin) und dem kommenden Kollisionspunkt wird berechnet
				}
				//Debug.Log (Vector3.Magnitude (dT) + " Länge von dem V-Vektor");
				
			}
		}
		
	}
	
	private Vector3 newDirection(Vector3 incoming, Vector3 normal) //Reflektieren des Lasers an einem Spiegel
	{//Formel funktioniert, nicht mehr anfassen!
		return incoming - 2 * (Vector3.Dot(incoming, normal) / Vector3.Dot(normal, normal)) * normal;
	}
	
	private void newCount() //eine neue Strecke für den Laser möglich machen
	{
		vertexCount++;
		lr.SetVertexCount(vertexCount);
	}
}