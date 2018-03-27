using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class requests : MonoBehaviour {

    public Text requestText;
	// Use this for initialization
	void Start () {
        WWWForm form = new WWWForm();
        int userID = PlayerPrefs.GetInt("userID");
        form.AddField("useridPost", userID);

        WWW www = new WWW("http://cng491.000webhostapp.com/viewrequests.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            Debug.Log(ResultString);
            string[] tokens1 = ResultString.Split(';');
            string[] tokens2 = tokens1[0].Split(' ');

            string requestedName = tokens2[0];
            int requestedID = int.Parse(tokens2[1]);
            int requestedPoint = int.Parse(tokens2[2]);

            requestText.text = requestedName + "   " + requestedID + "   " + requestedPoint;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
