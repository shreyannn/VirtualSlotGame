using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(RegisterUser());
    }

    IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form);
        yield return www.SendWebRequest();

        // if (www.result == UnityWebRequest.Result.Success && www.downloadHandler.text.Trim() == "0")
        // {
        //     Debug.Log("User created successfully.");
        //     UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        // }
        // else
        // {
        //     Debug.Log("User creation failed. Error #" + www.downloadHandler.text.Trim());
        // }
        //
        if ( www.downloadHandler.text[0] == 'R' ){
            Debug.Log("User created successfully.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.downloadHandler.text);
        }
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


