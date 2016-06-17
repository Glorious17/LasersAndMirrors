using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerScript : MonoBehaviour {

    private List<GameObject> signals = new List<GameObject>();
    private GameObject currentBolzen;
    private GameObject lifebar;
	private GameObject Licht;
	public int id;

    void Start()
    {
		Licht = GameObject.Find ("Lights");
        lifebar = GameObject.Find("Lifebar");
    }

    public List<GameObject> Signals   // Accessor mit einem getter
    {
        get
        {
			return signals;
        }
    }

    void OnTriggerEnter(Collider col) //wenn Signale den Trigger betreten, werden sie der Liste beigefügt
    {
        if(col.gameObject.tag == "Signal")
            signals.Add(col.gameObject);

        if (col.gameObject.tag == "Bolzen")
            if (col.gameObject.tag == "Bolzen")
                currentBolzen = col.gameObject;
    }

    void OnTriggerExit(Collider col) //wenn die Signale den Trigger verlassen, werden sie gelöscht und aus der Liste entfernt
    {
        if (col.gameObject.tag == "Signal")
        {
            lifebar.GetComponent<Solidity>().hit(2);
            GUI_Script.failed();

            signals.Remove(col.gameObject);
			Destroy (col.gameObject);

			Licht.GetComponent<Lights>().red [id] = true;
			GameObject.Find ("Main Camera").GetComponent<Touchscript> ().calculatePoints(1.4f);
        }
    }

    public GameObject getCurrentBolzen()
    {
        return currentBolzen;
    }
}
