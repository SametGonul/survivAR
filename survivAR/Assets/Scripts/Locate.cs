using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Locate : MonoBehaviour {

    public Text latText;
    public Text lonText;

    public float firstLat;
    public float firstLon;
    // Use this for initialization
    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            //latText.text = "aaaa";
            yield break;
        }
            

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            //lonText.text = "bbbb";
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            //latText.text = "cccc";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            //lonText.text = "dddd";
            latText.text = Input.location.lastData.latitude.ToString();
            lonText.text = Input.location.lastData.longitude.ToString();
            firstLat = Input.location.lastData.latitude;
            firstLon = Input.location.lastData.longitude;
            // Access granted and location value could be retrieved
            // print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            SceneManager.LoadScene("game");
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public float getFirstLat()
    {
        return this.firstLat;
    }

    public float getFirstLon()
    {
        return this.firstLon;
    }
}
