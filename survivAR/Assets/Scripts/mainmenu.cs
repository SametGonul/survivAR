using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mainmenu : MonoBehaviour {

  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goRegisterPage()
    {
        SceneManager.LoadScene("register");
    }

    public void goLoginPage()
    {
        SceneManager.LoadScene("login");
    }
}
