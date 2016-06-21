using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
*Spawning Patterns over Time with increasing Difficulty
*/

public class patternDrop : MonoBehaviour {

	public GameObject goodCup, badCup;
	public GameObject [] powerUp; //PowerUp Array
	public List<GameObject> pattern = new List<GameObject>(); //Gamobject of one Pattern will be saved in this list to delete them later on

	//Number of Objects in one Pattern
	public int level = 3; 
	private int levelCounter = 0;
	public int maxLevel = 18;
	private int counter = 0;

	//Measures of the pattern-grid
	private int gridWidth = 2;
    private int gridMinX = -2;
    private int gridMaxX = 3;

    private int gridMinZ = 0;
    private int gridMaxZ = 5;

	private GameObject go; //Filler object 
	public bool alreadyExists = false; 

	public float time = 0.0f;

	//Position of GameObject in the Pattern-Grid
	float xPos = 0F;
	float yPos = 5F;
	float zPos = 0F;
	float faktor = 4F; //Factor which decides the percentage of bad cups in one pattern

	public int roundInSeconds=0;
	float zahl=0f;

	void Start () {
		//First Spawn
		spawnPattern ();
	}

	void Update () {

		zahl = zahl + Time.deltaTime;

		if (zahl >= 1.0f) {
			zahl = 0;
			roundInSeconds+=1;
		}

		//if round takes too long(12sec), pattern will be deleted and a new pattern drops
		if (roundInSeconds >= 12) {
			GameObject.Find("UltimateCollider").GetComponent<CupCounter>().setCounter(0);
			deletePattern ();
			GameObject.Find("Treadmill").GetComponent<patternDrop>().alreadyExists = false;
			GameObject.Find("Treadmill").GetComponent<patternDrop>().spawnPattern();
			roundInSeconds = 0;
		}
	}

	//Called in Update() or CupCounter.cs
	public void spawnPattern(){
		GUI_ScriptGB.wave++;
		//time = 0.0f;
		levelCounter++;
		if (levelCounter == 4 && level<=maxLevel) {
			level++; //Difficulty increases with every fourth spawn (max 18)
			levelCounter = 0;
		}


		//spawn one Pattern
		while(counter < level) {

			//Random Position in Grid 
			xPos = Random.Range(gridMinX,gridMaxX) * gridWidth;
			zPos = Random.Range(gridMinZ,gridMaxZ) * gridWidth;


			//Powerup spawn (randomized, only when time is an even number)
			if(isFreeSpace()){

				if(!alreadyExists && GUI_ScriptGB.timeAnz > 0 && GUI_ScriptGB.timeAnz%3==0){

					//PowerUp-Spawn, immer zu Zeitpunkten, die durch einen gewissen Faktor geteilt, den Rest 0 besitzen
					go = Instantiate(powerUp[(int)Random.Range(0f,powerUp.Length)], new Vector3(xPos, yPos, zPos), Quaternion.Euler(0,Random.Range(0,359),0)) as GameObject;
					go.AddComponent<CollisionCounter>();
					pattern.Add (go); //add instance of GameObject to the List
					alreadyExists = true;
					counter++;
				}
			}

			//GoodCup/BadCup spawn
			if(isFreeSpace()){

				if(counter < Random.Range (0,level)){
					go = Instantiate(badCup, new Vector3(xPos, yPos, zPos), Quaternion.Euler(0,Random.Range(0,359),0)) as GameObject;
				
				}else{
					go = Instantiate(goodCup, new Vector3(xPos, yPos, zPos), Quaternion.Euler(0,Random.Range(0,359),0)) as GameObject;
				}
				pattern.Add(go);

				counter++;
			}
		}
		counter = 0;
	}

	//check if Position in Grid is already occupied
	bool isFreeSpace(){

		foreach (GameObject p in pattern) {

			if(xPos == p.transform.position.x && zPos == p.transform.position.z){
				return false;
			}

		}

		return true;
	}

	//Deletes all GameObjects in pattern
	void deletePattern(){

		foreach (var p in pattern) {
			Destroy (p);
		}
		pattern.Clear ();

	}
}
