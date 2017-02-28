using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour {

    public float sunRate = 10f;
    public int sunOffset = 20;
    public Light sunlight;
    public Color sunColorDay = new Color(1.0f, 0.957f, 0.839f);
    public Color sunColorRise = new Color(1.0f, 1.0f, 1.0f);
    public GameObject hourHand;
    public GameObject minuteHand;

    float sunPosition;

	// Use this for initialization
	void Start () {
        sunPosition = transform.eulerAngles.x;
        MoveSun();
	}

    public void MoveSun()
    {

        //move sun through sky
        if (sunPosition <= 190 || (sunPosition >= 350 && sunPosition < 360))
            sunPosition += sunRate;
        else if (sunPosition < 350 && sunPosition > 190)
            sunPosition = 350;
        else
            sunPosition = sunPosition - 360;

        hourHand.transform.eulerAngles = new Vector3((2*sunPosition)+180, 180, 0);
        minuteHand.transform.eulerAngles = new Vector3((24 * sunPosition), 180, 0);

        transform.eulerAngles = new Vector3((90 - sunOffset) * Mathf.Sin(Mathf.Deg2Rad * sunPosition), sunPosition, 0);

        //dim sun at night, brighten during the day
        if (sunPosition <= 180)
        {
            sunlight.intensity = 1;
            //RenderSettings.ambientIntensity = 1;
        }
        else if (sunPosition <= 190 && sunPosition > 180)
        {
            sunlight.intensity = 1 - ((sunPosition - 180) / 10);
            //RenderSettings.ambientIntensity = 1 - ((sunPosition - 180) / 10);
        }
        else if (sunPosition >= 350)
        {
            sunlight.intensity = ((sunPosition - 350) / 10);
            //RenderSettings.ambientIntensity = ((sunPosition - 350) / 10);
        }
        else
        {
            sunlight.intensity = 0;
            //RenderSettings.ambientIntensity = 0;
        }

        //change colour at dawn & dusk
        if (sunPosition < 10)
            sunlight.color = Color.Lerp(sunColorRise, sunColorDay, sunPosition / 10);
        else
            sunlight.color = Color.Lerp(sunColorDay, sunColorRise, (sunPosition - 170) / 10);
    }
}
