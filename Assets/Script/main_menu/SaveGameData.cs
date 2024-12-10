using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SaveGameData : MonoBehaviour
{

    public Text playerDisplay;
    public Text scoreDisplay;

    private void Awake()
    {
        if (DBManager.userName == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);   //...
        }
        playerDisplay.text = "Player:  " + DBManager.userName;
        scoreDisplay.text = "High Score:  " + DBManager.userBalance;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.userName);
        // form.AddField("balance", DBManager.balance);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "0")
            // if (!string.IsNullOrEmpty(www.downloadHandler.text) && www.downloadHandler.text[0] == '0')
            {
                Debug.Log("Game Saved.");
            }
            else
            {
                Debug.Log("Save failed. Error #" + www.downloadHandler.text);
            }
        }
        // DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void IncreaseScore()
    {
        DBManager.userBalance++;
        scoreDisplay.text = "Score: " + DBManager.userBalance;
    }


}



