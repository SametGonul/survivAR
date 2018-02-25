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

        WWW www = new WWW("http://cng491.000webhostapp.com/login.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            if (ResultString == "True")
            {
                SceneManager.LoadScene("dashboard");
         
            }

            else
                Debug.Log(ResultString);
        }
        
    }

    public void goMainmenu()
    {
        SceneManager.LoadScene("mainmenu");
    }

}
