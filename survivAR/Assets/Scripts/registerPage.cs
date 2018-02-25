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
        

        WWW www = new WWW("http://cng491.000webhostapp.com/test2.php", form);

        while (!www.isDone)
        {

        }
        if (www.isDone)
        {
            string ResultString = www.text;
            if (ResultString == "Everything ok.")
            {
                Debug.Log("Register Success");
                // to do (GO DASHBOARD includes start game,clans, etc.)
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
