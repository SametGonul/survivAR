using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class registerPage : MonoBehaviour
{

    public InputField username;
    public InputField password;
    public string Username;
    public string Password;

    public Text errorHandlingText;
    public Button registerButton;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void register()
    {

        Username = username.text;
        Password = password.text;
        Debug.Log(Username);
        Debug.Log(Password);

        WWWForm form = new WWWForm();
        form.AddField("usernamePost", Username);
        form.AddField("pointsPost", 0);
        form.AddField("passwordPost", Password);
        

        WWW www = new WWW("http://cng491.000webhostapp.com/register2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            handleError(ResultString);
   
        }
    }

    private void handleError(string type)
    {
        if (type == "0")
        {
            errorHandlingText.text = "You registered successfully.";
            PlayerPrefs.SetInt("Point", 0);

        }
        else if(type == "1")
        {
            errorHandlingText.text = "Username must be 3-15 chars.";
        }
        else if (type == "2")
        {
            errorHandlingText.text = "Password must be 3-15 chars.";
        }
        else if (type == "4")
        {
            errorHandlingText.text = "Username has taken.";
        }
        else if (type == "5")
        {
            errorHandlingText.text = "Database error.";
        }

        username.GetComponent<InputField>().text = "";
        username.placeholder.GetComponent<Text>().enabled = false;
        username.GetComponent<Image>().enabled = false;
        username.GetComponent<InputField>().enabled = false;

        password.GetComponent<InputField>().text = "";
        password.placeholder.GetComponent<Text>().enabled = false;
        password.GetComponent<Image>().enabled = false;
        password.GetComponent<InputField>().enabled = false;

        registerButton.interactable = false;
        registerButton.GetComponent<Image>().enabled = false;
        registerButton.GetComponent<Button>().enabled = false;
        registerButton.GetComponentInChildren<Text>().enabled = false;

        errorHandlingText.GetComponent<Text>().enabled = true;
        
    }
    public void goMainmenu()
    {
        SceneManager.LoadScene("mainmenu");
    }

}
