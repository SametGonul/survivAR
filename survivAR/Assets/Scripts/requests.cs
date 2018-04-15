using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class requests : MonoBehaviour {

   
    public Text requestText1;
    public Text requestText2;
    public Text requestText3;
    public Text requestText4;
    public Text requestText5;

    private Text[] requestTexts = new Text[5];

    public string[] tokens1;
    string[] tokens2;
    int masterID;
    // Use this for initialization
    public Button[] buttons = new Button[10];

    void Start () {

        masterID = PlayerPrefs.GetInt("userID");
        int j;

        requestTexts[0] = requestText1;
        requestTexts[1] = requestText2;
        requestTexts[2] = requestText3;
        requestTexts[3] = requestText4;
        requestTexts[4] = requestText5;
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
            tokens1 = ResultString.Split(';');
          
            int i;
            
            for (i = 0; i < tokens1.Length-1; i++)
            {
                tokens2 = tokens1[i].Split(' ');
                string requestedName = tokens2[0];
                int requestedID = int.Parse(tokens2[1]);
                int requestedPoint = int.Parse(tokens2[2]);
                requestTexts[i].text = requestedName + "          " + requestedPoint;
                buttons[(2 * i)].GetComponent<Image>().enabled = true;
                buttons[(2 * i)].enabled = true;
                buttons[(2 * i)].interactable = true;

                buttons[(2 * i) + 1].GetComponent<Image>().enabled = true;
                buttons[(2 * i) + 1].enabled = true;
                buttons[(2 * i) + 1].interactable = true;
            }
     
            int a;
            for(a = i; a < 5; a++)
            {
                buttons[(2 * a)].GetComponent<Image>().enabled = false;
                buttons[(2 * a)].enabled = false;
                buttons[(2 * a)].interactable = false;

                buttons[(2 * a) + 1].GetComponent<Image>().enabled = false;
                buttons[(2 * a) + 1].enabled = false;
                buttons[(2 * a) + 1].interactable = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void acceptUser1()
    {
        Debug.Log("samet1");
        tokens2 = tokens1[0].Split(' ');
        WWWForm form = new WWWForm();
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/acceptmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            // 0 success,1 database error

        }
        Application.LoadLevel("requests");

    }
    public void acceptUser2()
    {
        Debug.Log("samet2");
        tokens2 = tokens1[1].Split(' ');
        WWWForm form = new WWWForm();
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/acceptmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            // 0 success,1 database error

        }
        Application.LoadLevel("requests");
    }
    public void acceptUser3()
    {
        tokens2 = tokens1[2].Split(' ');
        WWWForm form = new WWWForm();
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/acceptmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            // 0 success,1 database error

        }
        Application.LoadLevel("requests");
    }
    public void acceptUser4()
    {
        tokens2 = tokens1[3].Split(' ');
        WWWForm form = new WWWForm();
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/acceptmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            // 0 success,1 database error

        }
        Application.LoadLevel("requests");
    }
    public void acceptUser5()
    {
        tokens2 = tokens1[4].Split(' ');
        WWWForm form = new WWWForm();
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/acceptmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            // 0 success,1 database error

        }
        Application.LoadLevel("requests");
    }

    public void rejectUser1()
    {
        Debug.Log("samet3");
        tokens2 = tokens1[0].Split(' ');
        WWWForm form = new WWWForm();
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/rejectmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            // 0 success,1 database error

        }
        Application.LoadLevel("requests");
    }
    public void rejectUser2()
    {
        tokens2 = tokens1[1].Split(' ');
        
        WWWForm form = new WWWForm();
        Debug.Log(masterID);
        Debug.Log(tokens2[1]);
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/rejectmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            Debug.Log(ResultString);

        }
        Application.LoadLevel("requests");
    }
    public void rejectUser3()
    {
        tokens2 = tokens1[2].Split(' ');
        WWWForm form = new WWWForm();
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/rejectmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            // 0 success,1 database error

        }
        Application.LoadLevel("requests");
    }
    public void rejectUser4()
    {
        tokens2 = tokens1[3].Split(' ');
        WWWForm form = new WWWForm();
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/rejectmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            // 0 success,1 database error

        }
        Application.LoadLevel("requests");
    }
    public void rejectUser5()
    {
        tokens2 = tokens1[4].Split(' ');
        WWWForm form = new WWWForm();
        form.AddField("masteridPost", masterID);
        form.AddField("useridPost", tokens2[1]);

        WWW www = new WWW("http://cng491.000webhostapp.com/rejectmember2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            // 0 success,1 database error

        }
        Application.LoadLevel("requests");
    }

    public void backButton()
    {
        SceneManager.LoadScene("mastermember");
    }

}
