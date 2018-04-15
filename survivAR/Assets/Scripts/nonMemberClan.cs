using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nonMemberClan : MonoBehaviour {

    public InputField clanName;
    string ClanName;

    public Text errorHandlingText;
    public Button joinorcreateButton;
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
            handleCreateError(ResultString);
            // 0-successfull 1-Clan name exist ,else error

        }

    
    }

    private void handleCreateError(string type)
    {
        if(int.Parse(type) == 0){
            errorHandlingText.text = "You created clan successfully.";
        }
        else if(int.Parse(type) == 1)
        {
            errorHandlingText.text = "Clan name is already taken.";
        }

        else if (int.Parse(type) == 6)
        {
            errorHandlingText.text = "Invalid character in clan name.";
        }
        else
        {
            errorHandlingText.text = "Database error.";
        }

        makeClose();
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
            errorHandler(ResultString);
            // 1clan not found, 0 success,else error // DONE

        }
    }

    private void errorHandler(string type)
    {
        if (int.Parse(type) == 0)
        {
            errorHandlingText.text = "Your request sent successfully.";
        }

        else if (int.Parse(type) == 1)
        {
            errorHandlingText.text = "Clan not found.";
        }
        else if (int.Parse(type) == 3)
        {
            errorHandlingText.text = "You already sent a request.";
        }

        
        else
        {
            errorHandlingText.text = "Database error.";
        }

        makeClose();
       
    }

    private void makeClose()
    {
        clanName.enabled = false;
        clanName.interactable = false;
        clanName.GetComponent<Image>().enabled = false;
        clanName.GetComponent<InputField>().text = "";
        clanName.GetComponent<InputField>().enabled = false;
        clanName.placeholder.GetComponent<Text>().text = "";

        joinorcreateButton.GetComponent<Image>().enabled = false;
        joinorcreateButton.enabled = false;
        joinorcreateButton.interactable = false;
        joinorcreateButton.GetComponentInChildren<Text>().enabled = false;
    }
    public void backButton()
    {
        Application.LoadLevel("dashboard");
    }
}
