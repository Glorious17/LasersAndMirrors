using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour
{

    private bool countDownEnabled;
    private int countMax = 3;
    private int countDown;
    private string countDownText;

    void Start()
    {
        countDownEnabled = true;
        countMax = 3;
        StartCoroutine(GameStart(1.5f));
    }

    IEnumerator GameStart(float waitingTime)
    {
        /*  var car = gameObject.Find("NAME_OF_CAR");
          var drivingScript = car.GetComponent("NAME_OF_DRIVING_SCRIPT");
          drivingScript.enabled = false;*/

        for (countDown = countMax; countDown >= 0; countDown--)
        {
            countDownText = countDown.ToString();
            yield return new WaitForSeconds(1);
        }

        countDownEnabled = false;
        //drivingScript.enabled = true;

    }

    void OnGUI()
    {
        if (countDownEnabled)
        {
            if (countDown > 0)
            {
                GUI.color = Color.red;
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.width / 2, 200, 175), "GET READY");

                // display countdown    
                GUI.color = Color.white;
                GUI.Box(new Rect(Screen.width / 2 - 90, Screen.width / 2 + 25, 180, 140), countDownText);
            }
            else
            {
                GUI.Box(new Rect(Screen.width / 2 - 90, Screen.width / 2 + 25, 180, 140), "GO");
				Camera.main.GetComponent<diagonalMovement>().enabled = true;
            }
        }


    }
}

