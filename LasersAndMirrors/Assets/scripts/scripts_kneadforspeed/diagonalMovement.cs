using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class diagonalMovement : MonoBehaviour
{
	public GameObject signal;

	private float movementX = 3f;
	private float movementZ = 3f;

    private float patternMovementX = 3f;
    private float patternMovementZ = 3f;

    private double randomSpawn = 100;
    private float zweiSpawnHaeufigkeit = 25f;
    private int randomPattern; 				//Diese Variable wird später zufällig bestimmt und entscheidet welches "Special-Pattern" abgespielt wird
    private int randomizer; 				//Eine Zufallsvariable die nach jedem normalen "Spawn" neu bestimmt wird. Die Variable hat Einfluss auf alle "Special-Pattern"
    private float patternTime = 4;		 	//Diese Variable sorgt dafür, dass ein "Special-Pattern" nicht innerhalb der ersten 20 Frames abgespielt wird.
    private int patternCounter = 0; 		//Eine Variable die mitzählt, wie viele Spawns, während eines "Special-Pattern", bereits abgespielt wurden. 
    private bool zweiterSpawn = false;
    private bool specialPattern = false;
    private float spawntime = 3f;
    private float spawnSpeed = 5f;
    private float fasterSpawnTime = 0;
    private float fastesSpeed = 0.6f; 		//Umso kleiner diese Zahl ist, umso schneller ist die maximale Geschwindigkeit der Spawns
	//private float faktor = 500f;

	GameObject s;


    void Start()
    {
        //spawn();
    }

    void Update()
    {
		if (GameObject.Find ("Main Camera").GetComponent<GUI_Script> ().isPaused == false){
        if (!specialPattern)
        {
            if (Time.realtimeSinceStartup >= spawntime)
            {
                if (randomSpawn < zweiSpawnHaeufigkeit)
                {
                    spawn();
                    zweiterSpawn = true;
                    spawn();
                    spawntime = Time.realtimeSinceStartup + spawnSpeed;
                    zweiterSpawn = false;
                }
                else {
                    spawn();
                    Debug.Log(Time.realtimeSinceStartup);
                    spawntime = Time.realtimeSinceStartup + spawnSpeed;
                }
                if (Random.Range(0, 100) % 20 == 0)
                {
                    specialPattern = true;
                    patternTime = Time.realtimeSinceStartup + spawnSpeed;
                }
            }
            randomizer = Random.Range(0, 1001);
        }
        else
        {
            if (patternCounter == 0)
                randomPattern = Random.Range(0, 3);

            if (Time.realtimeSinceStartup >= patternTime)
            {
                pattern();
                patternTime = Time.realtimeSinceStartup + spawnSpeed;
                spawntime = Time.realtimeSinceStartup + spawnSpeed;
            }
            
        }
        
        if (fasterSpawnTime >= 10)
        {
            spawnSpeed /= 1.2f;
            if(spawnSpeed < fastesSpeed)
            {
                spawnSpeed = fastesSpeed;
            }
            fasterSpawnTime = 0;
        }
        fasterSpawnTime += Time.deltaTime;

        //spawning += Time.time;// * Time.deltaTime;
        // Debug.Log(spawning);

		}
	}

	void spawn()
	{
		randomSpawn = Random.Range (0F, 199F);
		s = Instantiate(signal);
		randPos();
		s.GetComponent<Rigidbody> ().AddForce (new Vector3 (movementX, 0, movementZ), ForceMode.Impulse);
	}

    void petternSpawn() //Der Spawn für ein "Special-Pattern"
    {
        s = Instantiate(signal);
        s.GetComponent<Rigidbody>().AddForce(new Vector3(patternMovementX, 0, patternMovementZ), ForceMode.Impulse);
    }

	void randPos()
	{
		int signum = Random.Range (0, 2) * 2 - 1; //Vorzeichen variiert zwischen -1 und 1;
		//choose between the directions, signum = Vorzeichen
		//random directions
		if (zweiterSpawn == false) {
			//signum = Random.Range (0, 2) * 2 - 1; //Vorzeichen variiert zwischen -1 und 1
			movementX *= signum; //Vorzeichenwechsel
		} else {
			movementX *= -1;
		}

		signum = Random.Range(0, 2) * 2 - 1;
		movementZ *= signum;
		/*int rand = Random.Range (0, 4);

		switch (rand) {
		case 0:
			movementX = Screen.width / faktor;
			movementZ = Screen.height / faktor;
			break;

		case 1:
			movementX = Screen.width / faktor * (-1);
			movementZ = Screen.height / faktor;
			break;

		case 2:
			movementX = Screen.width / faktor;
			movementZ = Screen.height / faktor * (-1);
			break;

		case 3:
			movementX = Screen.width / faktor * (-1);
			movementZ = Screen.height / faktor * (-1);
			break;*/
	}

    void pattern() //Anhand der zuvor zufällig bestimmten Variable "randomPattern" wird das entsprechende "Special-Pettern" bestimmt.
    {
        switch (randomPattern)
        {
            case 0:
                patternCircle();
                break;
            case 1:
                patternMultiSpawn();
                break;
            case 2:
                patternMultiSwitchSpawn();
                break;
        }
        if (patternCounter >= 20)
        {
            specialPattern = false;
            patternCounter = 0;
        }
    }

    void patternMultiSwitchSpawn() //20 Spawns werden in eine zufällige Richtung ausgeführt. Jeder 5. Spawn wird auf der gegenüberliegenden Seite ausgeführt.
    {
        if (randomizer < 250)
        {
            patternMovementX = movementX;
            patternMovementZ = movementZ;
        }
        else if (randomizer < 500)
        {
            patternMovementX = movementX * -1;
            patternMovementZ = movementZ;
        }
        else if (randomizer < 750)
        {
            patternMovementX = movementX * -1;
            patternMovementZ = movementZ * -1;
        }
        else if (randomizer < 1000)
        {
            patternMovementX = movementX;
            patternMovementZ = movementZ * -1;
        }

        if (patternCounter % 5 == 0)
            if (patternCounter % 10 == 0)
            {
                patternMovementX *= -1;
                patternMovementZ *= -1;
            }
            else
                patternMovementX *= -1;

        patternCounter++;
        petternSpawn();
    }

    void patternMultiSpawn() //20 Spawns werden in eine zufällige Richtung ausgeführt.
    {
        if(patternCounter == 0)
            if (randomizer < 250)
            {
                patternMovementX = movementX;
                patternMovementZ = movementZ;
            }
            else if (randomizer < 500)
            {
                patternMovementX =  movementX * -1;
                patternMovementZ = movementZ;
            }
            else if (randomizer < 750)
            {
                patternMovementX = movementX * -1;
                patternMovementZ = movementZ * -1;
            }
            else if (randomizer < 1000)
            {
                patternMovementX = movementX;
                patternMovementZ = movementZ * -1;
            }
        patternCounter++;
        petternSpawn();
    }

    void patternCircle() //20 Spawns werden rotierend ausgeführt.
    {
        if (randomizer < 500)
            if (patternCounter % 2 == 0)
                patternMovementX *= -1;
            else
                patternMovementZ *= -1;
        else
            if (patternCounter % 2 == 0)
                patternMovementZ *= -1;
            else
                patternMovementX *= -1;


        movementX = patternMovementX;
        movementZ = patternMovementZ;
        patternCounter++;
        petternSpawn();
    }
	
}
