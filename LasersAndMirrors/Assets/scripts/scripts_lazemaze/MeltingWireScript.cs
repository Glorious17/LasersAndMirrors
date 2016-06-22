using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MeltingWireScript : MonoBehaviour {


    /**
    ALLES AUSKOMMENTIERTE IST ZUM VERKÜRZEN DES ZYLINDERS
    **/


    public GameObject cylinder, sphere; //Draht besteht aus einer Kugel und einem Zylinder

    //factor zum Skalieren der Kugel, Lerp ist ein Wert, der sich zwischen 0 und 1 befinden sollte, deltaLerp ist zum inkrementieren von Lerp
    private float factor = 0.015f, lerp = 0.01f, deltaLerp = 0.02f;
    private float sizeOfMeltedStuff = 1.6f;
    private bool stop = false; //boolescher Wert zum Stoppen der Animation

    public Color colorstart, colorend; //Farben werden festgelegt, die kaltes Metall und heißes Metall darstellen sollen
    private Renderer renderer; //Renderer ist dazu da, die Materialfarbe auf einem GameObject festzulegen

    private Vector3 posCyl, posSph, scaleCyl, scaleSph; //Speichereinheiten, für die Ursprungspositionen (zum Reset benötigt)
    // Use this for initialization
    void Start () {
        renderer = sphere.GetComponent<Renderer>(); //Renderer der Kugel zuweisen

        //Festlegen, der Ursprungspositionen, der Drahtobjekte
        //posCyl = cylinder.transform.position;
        posSph = sphere.transform.position;
        //scaleCyl = cylinder.transform.localScale;
        scaleSph = sphere.transform.localScale;
    }

    // Update is called once per frame
    void Update () {
	}



    public void melting() //melting wird aufgerufen, wenn der Draht vom Laser berührt wird (siehe Laserscript)
    {

        /*        
        if (ending <= 0.9f)
        {
            cylinder.transform.Translate(0, -0.001f, 0);
            //cylinder.transform.localScale += new Vector3(0, -0.04f, 0);

            ending += 0.04f;
        }*/
        lerp += deltaLerp; 
        if (lerp >= 0.99f)
        {
            deltaLerp = -0.005f;
        }
        if (lerp < 0.01f)
        {
            stop = true;
        }

        if (!stop) //Switchen zwischen den Farben, solange noch das ENde erreicht wurde

        {
            renderer.material.color = Color.Lerp(colorstart, colorend, lerp);
        }
        else
        {
            //Ende des Schmelzens und somit wird die aktive Szene neu geladen
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (sphere.transform.localScale.x <= sizeOfMeltedStuff && sphere.transform.localScale.y <= sizeOfMeltedStuff && sphere.transform.localScale.z <= sizeOfMeltedStuff)
        {
            sphere.transform.localScale += new Vector3(factor, factor, factor);
            //sphere.transform.Translate(0.075f, 0.0f, 0.0f);
        }

    }

    public void resetMeltingProcess()
    {
        //cylinder.transform.position = posCyl;
        //cylinder.transform.localScale = scaleCyl;

        sphere.transform.position = posSph;
        sphere.transform.localScale = scaleSph;

        lerp = 0.01f;
        deltaLerp = 0.02f;
        stop = false;
        //ending = 0;
    }
}
