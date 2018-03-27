using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dashboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startGame()
    {
        SceneManager.LoadScene("game");
    }

    public void logout()
    {
        SceneManager.LoadScene("mainmenu");
    }

    public void goClanInfo()
    {
        int userRole = PlayerPrefs.GetInt("userRole");
        if (userRole == 0)
        {
            SceneManager.LoadScene("NonMemberClanOption");
        }

        else if (userRole == 4)
        {
            SceneManager.LoadScene("mastermember");
        }
    }
}
