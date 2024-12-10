using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BalanceUpdater : MonoBehaviour
{
    private string updateUrl = "http://localhost/sqlconnect/balanceUpdateUnitySide.php";

    public void UpdateBalance(int userID, float newBalance)
    {
        StartCoroutine(UpdateBalanceRequest(userID, newBalance));
    }

    private IEnumerator UpdateBalanceRequest(int userID, float newBalance)
    {
        // Prepare form data
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("newBalance", newBalance.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(updateUrl, form))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + www.error);
            }
        }
    }
}