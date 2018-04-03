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

    string requestedName;
    int requestedID;
    int requestedPoint;

    // Use this for initialization
    void Start () {
        int userID = PlayerPrefs.GetInt("userID");
        WWWForm form = new WWWForm();
        form.AddField("useridPost", userID);


        WWW www = new WWW("http://cng491.000webhostapp.com/clandetails.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            Debug.Log(ResultString);
            string[] tokens1 = ResultString.Split(';');
            int i;
            for(i = 0; i < tokens1.Length; i++)
            {
                string[] tokens2 = tokens1[i].Split(' ');
                PlayerInfo player = new PlayerInfo();
                player.username = tokens2[0];
                player.playerID = tokens2[1];
                player.playerContribution = tokens2[2];
                player.playerRole = tokens2[3];

                playerList[i] = player;

                membersInfo[i].text = playerList[i].username + "   " + playerList[i].playerContribution + "    " + playerList[i].playerRole;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void viewRequestButton()
    {
        SceneManager.LoadScene("requests");
    }

    
}
