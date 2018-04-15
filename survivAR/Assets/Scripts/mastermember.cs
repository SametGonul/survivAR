using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mastermember : MonoBehaviour {

    struct PlayerInfo
    {
        public string username;
        public string playerID;
        public string playerRole;
        public string playerContribution;
    }

    PlayerInfo[] playerList = new PlayerInfo[5];


    public Text[] membersInfo = new Text[5];
    public Text[] pointTexts = new Text[5];

    string requestedName;
    int requestedID;
    int requestedPoint;

    public Button[] buttons = new Button[5];
    string[] tokens1;

    public Text clanName;
    public Text clanPoint;

    public Button closeClanButton;
    public Button requestButton;
    // Use this for initialization

    public Image checkImage;
    public Text errorText;
    public Button yesButton;
    public Button noButton;
    int buttonCounter;
    void Start () {

        checkImage.enabled = false;
        errorText.enabled = false;
        errorText.GetComponentInChildren<Text>().enabled = false;

        yesButton.enabled = false;
        yesButton.interactable = false;
        yesButton.GetComponentInChildren<Text>().enabled = false;

        noButton.enabled = false;
        noButton.interactable = false;
        noButton.GetComponentInChildren<Text>().enabled = false;

        int userID = PlayerPrefs.GetInt("userID");
        WWWForm form = new WWWForm();
        form.AddField("useridPost", userID);


        WWW www = new WWW("http://cng491.000webhostapp.com/clandetails.php", form);

        while (!www.isDone)
        {

        }
        int i;
        
        int playerRole = PlayerPrefs.GetInt("userRole");

        if (www.isDone)
        {
            string ResultString = www.text;

            string[] points = ResultString.Split('*');

            string point = points[0];
            clanPoint.text = point;
            string name = points[1];
            clanName.text = name;
            string resultString2 = points[2];
            tokens1 = resultString2.Split(';');

            buttonCounter = tokens1.Length - 1;
            if (buttonCounter > 5)
            {
                buttonCounter = 5;
            }
            for (i = 0; i < buttonCounter; i++)
            {
                string[] tokens2 = tokens1[i].Split(' ');
                PlayerInfo player = new PlayerInfo();
                player.username = tokens2[0];
                player.playerID = tokens2[1];
                player.playerContribution = tokens2[2];
                player.playerRole = tokens2[3];

                playerList[i] = player;

                membersInfo[i].text = playerList[i].username;
                pointTexts[i].text = playerList[i].playerContribution;
            }
            if (playerRole == 4)
            {

                closeClanButton.enabled = true;
                closeClanButton.interactable = true;
                closeClanButton.GetComponent<Image>().enabled = true;
                closeClanButton.GetComponentInChildren<Text>().enabled = true;

                requestButton.enabled = true;
                requestButton.interactable = true;
                requestButton.GetComponent<Image>().enabled = true;
                requestButton.GetComponentInChildren<Text>().enabled = true;

                for (i = 0; i < buttonCounter; i++)
                {

                    int role = int.Parse(playerList[i].playerRole);
                    if (role == 4)
                    {
                        buttons[i].GetComponent<Image>().enabled = false;
                        buttons[i].enabled = false;
                        buttons[i].interactable = false;
                    }
                    else
                    {
                        buttons[i].GetComponent<Image>().enabled = true;
                        buttons[i].enabled = true;
                        buttons[i].interactable = true;
                    }
                }
                int a;
                for (a = i; a < 5; a++)
                {
                    buttons[a].GetComponent<Image>().enabled = false;
                    buttons[a].enabled = false;
                    buttons[a].interactable = false;
                }
            }

            if (playerRole == 1)
            {

            }
        }

        
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void backButton()
    {
        SceneManager.LoadScene("dashboard");
    }

    public void viewRequestButton()
    {
        SceneManager.LoadScene("requests");
    }

    public void kickPlayer1()
    {
        string userID = playerList[0].playerID;
        WWWForm form = new WWWForm();
        form.AddField("useridPost", userID);


        WWW www = new WWW("http://cng491.000webhostapp.com/kickmember.php", form);

        while (!www.isDone)
        {

        }

        if (www.isDone)
        {
            string ResultText = www.text;
            Debug.Log(ResultText);
            errorHandling(ResultText);
        }
    }

    public void kickPlayer2()
    {
        string userID = playerList[1].playerID;
        WWWForm form = new WWWForm();
        form.AddField("useridPost", userID);


        WWW www = new WWW("http://cng491.000webhostapp.com/kickmember.php", form);

        while (!www.isDone)
        {

        }

        if (www.isDone)
        {
            string ResultText = www.text;
            errorHandling(ResultText);
        }
    }

    public void kickPlayer3()
    {
        string userID = playerList[2].playerID;
        WWWForm form = new WWWForm();
        form.AddField("useridPost", userID);


        WWW www = new WWW("http://cng491.000webhostapp.com/kickmember.php", form);

        while (!www.isDone)
        {

        }

        if (www.isDone)
        {
            string ResultText = www.text;
            errorHandling(ResultText);
        }
    }

    public void kickPlayer4()
    {
        string userID = playerList[3].playerID;
        WWWForm form = new WWWForm();
        form.AddField("useridPost", userID);


        WWW www = new WWW("http://cng491.000webhostapp.com/kickmember.php", form);

        while (!www.isDone)
        {

        }

        if (www.isDone)
        {
            string ResultText = www.text;
            errorHandling(ResultText);
        }
    }

    public void kickPlayer5()
    {
        string userID = playerList[4].playerID;
        WWWForm form = new WWWForm();
        form.AddField("useridPost", userID);


        WWW www = new WWW("http://cng491.000webhostapp.com/kickmember.php", form);

        while (!www.isDone)
        {

        }

        if (www.isDone)
        {
            string ResultText = www.text;
            errorHandling(ResultText);
        }
    }

    private void errorHandling(string type)
    {
        if(int.Parse(type) == 0)
        {
            SceneManager.LoadScene("mastermember");
        }
    }

    public void closeClan()
    {

        int i;
        for(i = 0; i < buttonCounter; i++)
        {
            buttons[i].GetComponent<Image>().enabled = false;
            buttons[i].enabled = false;
            buttons[i].interactable = false;
        }

        checkImage.enabled = true;
        errorText.enabled = true;
        errorText.GetComponentInChildren<Text>().enabled = true;

        yesButton.enabled = true;
        yesButton.interactable = true;
        yesButton.GetComponentInChildren<Text>().enabled = true;

        noButton.enabled = true;
        noButton.interactable = true;
        noButton.GetComponentInChildren<Text>().enabled = true;

    }

    public void runYesButton()
    {
        int userID = PlayerPrefs.GetInt("userID");
        WWWForm form = new WWWForm();
        form.AddField("useridPost", userID);


        WWW www = new WWW("http://cng491.000webhostapp.com/closeclan.php", form);

        while (!www.isDone)
        {

        }

        if (www.isDone)
        {
            string ResultText = www.text;
            if (ResultText == "0")
            {
                PlayerPrefs.SetInt("userRole", 0);
                SceneManager.LoadScene("dashboard");
            }
        }
    }


    public void runNoButton()
    {
        SceneManager.LoadScene("mastermember");
    }
}
