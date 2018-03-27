using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nonMemberClan : MonoBehaviour {

    public InputField clanName;
    string ClanName;

	// Use this for initialization
	void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void createClanButton()
    {
        SceneManager.LoadScene("creatingClan");
    }
    public void createClan()
    {
        ClanName = clanName.text;

        WWWForm form = new WWWForm();
        int userID = PlayerPrefs.GetInt("userID");
   
        form.AddField("useridPost", userID);
        form.AddField("clannamePost", ClanName);

        PlayerPrefs.SetInt("userRole", 4);

        WWW www = new WWW("http://cng491.000webhostapp.com/createclan.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            Debug.Log(ResultString);

        }

    
    }

    public void joinButton()
    {
        SceneManager.LoadScene("joiningClan");
    }
    public void joinClan()
    {
        ClanName = clanName.text;

        WWWForm form = new WWWForm();
        int userID = PlayerPrefs.GetInt("userID");

        form.AddField("useridPost", userID);
        form.AddField("clannamePost", ClanName);

        WWW www = new WWW("http://cng491.000webhostapp.com/joinclan.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            Debug.Log(ResultString);

        }
    }
}
