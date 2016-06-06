using UnityEngine;
using System.Collections;

public class GUI_CarouselMenu : MonoBehaviour {

    private GameObject carousel;
    private int degCounter = 0;
    private float deg = 2.0f; //Drehgeschwindigkeit
    private bool rotate = false;
    // Use this for initialization
    void Start()
    {
        carousel = GameObject.Find("Carousel");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            if (Input.mousePosition.x <= Screen.width / 2) //zwischen Links und rechts auf dem Bildschirm unterscheiden
            {
                if (!rotate)
                {
                    rotate = true;
                    deg = 2.0f;
                }
            }
            else
            {
                if (!rotate)
                {
                    rotate = true;
                    deg = -2.0f;
                }
            }
        }

        if (rotate)
        {
            carousel.transform.Rotate(new Vector3(0, deg, 0)); //Animation der Drehung
            degCounter += (int)((deg < 0) ? deg * (-1) : deg); //Gradzahl wird zum Counter hinzugerechnet, damit nie eine 90 Gradgrenze überschritten wird
            if (degCounter >= 90)
            {
                rotate = false;
                degCounter = 0; //zurücksetzen des Counters
            }
        }

    }
}
