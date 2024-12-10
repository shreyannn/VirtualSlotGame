using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text playerDisplay;
    public Text playerBalance;

    [SerializeField] private Text logInButtonStatus;
    private void Start()          //databse  -> login part
    {
        if (DBManager.LoggedIn && DBManager.userName != null)
        {
            playerDisplay.text = "Player:  " + DBManager.userName;
            playerBalance.text = "Balance: " + DBManager.userBalance;
            logInButtonStatus.text = "LOG OUT";
        }
       
    }


    // public void LoadSinglePlayer()
    // {
    //     Debug.Log("Single Player Game Loading...");
    //     SceneManager.LoadScene(2);
    //     Time.timeScale = 1;             // pause -> Time.timeScale =0  -> main menu ->single player/ coop -> resume play -> Time.timeScale is still 0.
    // }
    //
    // public void LoadCoOp()
    // {
    //     Debug.Log("Co-Op Game Loading...");
    //     SceneManager.LoadScene(3);
    //     Time.timeScale = 1;
    // }

    public void GoToRegister()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToLogin()
    {
        if (DBManager.LoggedIn)
        {
            DBManager.LogOut();
            logInButtonStatus.text = "LOG IN";    // Reset (LOG OUT)
            playerDisplay.text = "No user logged in.";
            playerBalance.text = "";
            return;
        }
        SceneManager.LoadScene(4);
    }
    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenuInitial()
    {
        SceneManager.LoadScene(2);
    }


}
