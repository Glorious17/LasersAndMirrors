using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Solidity : MonoBehaviour {

    private float strength = 100;
    private float goodHitCombo = 0;
    private float scale;

	// Use this for initialization
	void Start () {
        scale = gameObject.transform.localScale.x;
    }

    public void hit(int i) //input 0 = perfekt; 1 = ok; 2 = fail; 
    {
        switch (i)
        {
            case 0:

             
                if (goodHitCombo == 3 && strength!= 100)
                {
                    strength += 20;
                    goodHitCombo = 0;
                }
                else
                {
                    goodHitCombo++;
                }
                    break;
            case 1:
                
                strength -= 0;
                goodHitCombo = 0;
                break;
            case 2:
                
                strength -= 20;
                goodHitCombo = 0;
                break;
        }
        transform.localScale = new Vector3(scale * (strength / 100), 1, 0.5f);

        // Debug, falls die Skalierung zu groß wird
        if (transform.localScale.x <= 0) //wenn die Skalierung der Lebensleiste gen 0 geht
        {
            transform.localScale = new Vector3(0, 0, 0);
            SceneManager.LoadScene("kneadforspeed_end");
        }

      //  Debug.Log("LocalScale: " + transform.localScale.x + " Strength: " + strength);
    }

    public float getStrength() //besseres C# -> Accessor schreiben mit einem getter
    {
        return strength;
    }
}
