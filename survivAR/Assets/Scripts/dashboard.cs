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
        int userID = PlayerPrefs.GetInt("userID");

        WWWForm form = new WWWForm();
        form.AddField("useridPost", userID);

        WWW www = new WWW("http://cng491.000webhostapp.com/refreshuserinfo.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            PlayerPrefs.SetInt("userRole", int.Parse(ResultString));

        }
        int userRole = PlayerPrefs.GetInt("userRole");
        if (userRole == 0)
        {
            SceneManager.LoadScene("NonMemberClanOption");
        }

        else if (userRole == 1)
        {
            SceneManager.LoadScene("mastermember");
        }
        else if (userRole == 4)
        {
            SceneManager.LoadScene("mastermember");
        }
    }
}
