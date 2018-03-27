using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mastermember : MonoBehaviour {

    public Text requestText;

    string requestedName;
    int requestedID;
    int requestedPoint;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void viewRequestButton()
    {
        SceneManager.LoadScene("requests");
    }

    
}
