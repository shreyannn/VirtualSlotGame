using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Button submitButton;

    [SerializeField] private GameObject wrongCredentialsPopUp;
    [SerializeField] private GameObject errorPopUp;
    [SerializeField] private GameObject accountStatusPopUp;
    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    // hello
    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);
    
        using(UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)    
            {
                string response = www.downloadHandler.text;
                Debug.Log("Server Response: " + response);
               
                
                if (response.StartsWith("Login successful"))
                {
                    DBManager.userName = nameField.text;

                    // Extract account status
                    int statusIndex = response.IndexOf("Account Status: ");
                    if (statusIndex != -1)
                    {
                        statusIndex += "Account status: ".Length;
                        int statusEndIndex = response.IndexOf(" |", statusIndex); // Find the end of the status section
                        string accountStatus = response.Substring(statusIndex, statusEndIndex - statusIndex).Trim();
                        Debug.Log("Account Status: " + accountStatus);
                    
                        if (accountStatus == "1")
                        {
                            Debug.Log("your account is on hold");
                            accountStatusPopUp.SetActive(true);
                            Invoke(nameof(AccountStatusPopUpDisable),2);
                            yield break;
                        }
                    }
                    
                    // Extract user ID
                    int userIDIndex = response.IndexOf("ID: ");
                    if (userIDIndex != -1)
                    {
                        userIDIndex += "ID: ".Length;
                        int userIDEndIndex = response.IndexOf(" |", userIDIndex); // Find the end of the status section
                        string userID = response.Substring(userIDIndex, userIDEndIndex - userIDIndex).Trim();
                        Debug.Log("USER ID: " + userID);
                        DBManager.userID = int.Parse(userID);
                    }
                    
                    
                    
                    // Extract Spin Count
                    int spinIndex = response.IndexOf("Your SpinCount: ");
                    if (spinIndex != -1)
                    {
                        spinIndex += "Your SpinCount: ".Length;
                        int spinEndIndex = response.IndexOf(" |", spinIndex); // Find the end of the status section
                        string spinCount = response.Substring(spinIndex, spinEndIndex - spinIndex).Trim();
                        Debug.Log("Spin Count: " + spinCount);
                        DBManager.userSpinCount = int.Parse(spinCount);
                    }
                    
                    // Extract the balance directly
                    int balanceIndex = response.IndexOf("Your balance: ");
                    if (balanceIndex != -1)
                    {
                        balanceIndex += "Your balance: ".Length;
                        string balance = response.Substring(balanceIndex).Trim();
                        Debug.Log("BALANCE: " + balance);
                        DBManager.userBalance = float.Parse(balance);
                    }
                    
                    DBManager.userName = nameField.text;
                    Debug.Log("Logged In.");
                    SceneManager.LoadScene(2);
                }
                else
                {
                    // Debug.Log("Login failed: " + response);
                    wrongCredentialsPopUp.SetActive(true);
                    Invoke(nameof(WrongCredentialsPopUpDisable),2);
                }
            }
            else
            {
                Debug.LogError("Error: " + www.error);
                errorPopUp.SetActive(true);
                Invoke(nameof(ErrorPopUpDisable),2);
            }
            
        }
    
    }

    private void WrongCredentialsPopUpDisable()
    {
        CancelInvoke(nameof(WrongCredentialsPopUpDisable));
        wrongCredentialsPopUp.SetActive(false);
    }
    
    private void ErrorPopUpDisable()
    {
        CancelInvoke(nameof(ErrorPopUpDisable));
        errorPopUp.SetActive(false);
    }
    
    private void AccountStatusPopUpDisable()
    {
        CancelInvoke(nameof(AccountStatusPopUpDisable));
        accountStatusPopUp.SetActive(false);
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 6) && (passwordField.text.Length >= 6);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(2);
    }
}
