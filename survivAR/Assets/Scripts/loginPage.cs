using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loginPage : MonoBehaviour {

    // Use this for initialization

    public InputField username;
    public InputField password;
    public string Username;
    public string Password;

    public Button loginButton;
    public Text errorText;

    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void enterUsernameAndPassword()
    {

        // get username and password
        Username = username.text;
        Password = password.text;

        // sending to database the information
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", Username);
        form.AddField("passwordPost", Password);
        form.AddField("codePost", "+*!");

        WWW www = new WWW("http://cng491.000webhostapp.com/login4.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            resultHandler(ResultString); ;
                
        }
        
    }

    private void resultHandler(string resultString)
    {

        if (resultString == "1")
        {
            errorText.GetComponent<Text>().enabled = true;
            errorText.text = "Invalid username.";
        }

        else if(resultString == "2")
        {
            errorText.GetComponent<Text>().enabled = true;
            errorText.text = "Invalid password.";
        }

        else if(resultString == "3")
        {
            errorText.GetComponent<Text>().enabled = true;
            errorText.text = "Database error.";
        }

        else
        {
            string[] tokens = resultString.Split(';');
            int userID = int.Parse(tokens[0]);
            PlayerPrefs.SetInt("userID",userID);
            int userRole = int.Parse(tokens[1]);
            PlayerPrefs.SetInt("userRole", userRole);
            

            SceneManager.LoadScene("dashboard");
        }

        username.GetComponent<InputField>().text = "";
        username.placeholder.GetComponent<Text>().enabled = false;
        username.GetComponent<Image>().enabled = false;
        username.GetComponent<InputField>().enabled = false;

        password.GetComponent<InputField>().text = "";
        password.placeholder.GetComponent<Text>().enabled = false;
        password.GetComponent<Image>().enabled = false;
        password.GetComponent<InputField>().enabled = false;

        loginButton.interactable = false;
        loginButton.GetComponent<Image>().enabled = false;
        loginButton.GetComponent<Button>().enabled = false;
        loginButton.GetComponentInChildren<Text>().enabled = false;

       
    }

    public void goMainmenu()
    {

        SceneManager.LoadScene("mainmenu");
    }

}
