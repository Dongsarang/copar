using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deviceHeadingManager : MonoBehaviour {

    public Text textHeading;
    public Text textLocation;


    IEnumerator Start()
    {

        textHeading.GetComponent<Text>().text = "heading set: NULL ";
        textLocation.GetComponent<Text>().text = "location set: NULL";

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("location is Enabled by user");
            textHeading.GetComponent<Text>().text = "the location is Enabled by user";
            textLocation.GetComponent<Text>().text = "the location is Enabled by user";

        }
        else
        {
            // Start service before querying location
            Input.location.Start();

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                Debug.Log("Timed out");
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
                yield break;
            }
            else
            {
                textHeading.GetComponent<Text>().text =
                    "Enable?: " + Input.compass.enabled + "\n"
                    + " MagneticHeading: " + Input.compass.magneticHeading + "\n"
                    + " headingAccuracy: " + Input.compass.headingAccuracy + "\n"
                    + " rawVector: " + Input.compass.rawVector + "\n"
                    + " trueHeading: " + Input.compass.trueHeading;

                textLocation.GetComponent<Text>().text =
                    "Lat and lon: " + Input.location.lastData.latitude + ", " + Input.location.lastData.longitude;
            }
        }

        // Stop service if there is no need to query location updates continuously
        //        Input.location.Stop();
    }

    private void calcPlayerShopVector(){

    }


    // Update is called once per frame
    void Update()
    {
        if (!Input.location.isEnabledByUser)
        {
            return;
        }
        else
        {
            textHeading.GetComponent<Text>().text =
                    "Enable?: " + Input.compass.enabled + "\n"
                    + " MagneticHeading: " + Input.compass.magneticHeading + "\n"
                    + " headingAccuracy: " + Input.compass.headingAccuracy + "\n"
                    + " rawVector: " + Input.compass.rawVector + "\n"
                    + " trueHeading: " + Input.compass.trueHeading;

            textLocation.GetComponent<Text>().text =
                "Lat and lon: " + Input.location.lastData.latitude + ", " + Input.location.lastData.longitude;
        }

    }
}
