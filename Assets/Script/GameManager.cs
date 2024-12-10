using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.Log("instance is null");
			return _instance;
		}
	}
	private void Awake()
	{
		_instance = this;
	}

	[SerializeField] private List<Grid> placementId ;
	[SerializeField] private List<Item> items;
	[SerializeField] private List<MultiplierInScatter> multiplierInScatter;
	[SerializeField] private int columnCount = 5;
	[SerializeField] private int rowCount = 3;
	[SerializeField] private int[,] reelOnStop;
	
	[SerializeField] private int itemcount;
	[SerializeField] private bool labelPass=false;
	[SerializeField] private bool match=false;
	[SerializeField] private List<int> winCheckList;
	[SerializeField] private List<GameItem> winLineCheckInGameItemList;
	[SerializeField] private List<winLineStore> winLineStoreInGameItemsList;
	
	public int max;    // if we win with 4 consecutive items, we wont detect 3 items win from it.
	public bool hasWon;
	public bool overAllHasWon;
	[SerializeField] private Animator[] tmpItemScaling = new Animator[5];
	public bool reelComplete;
	public bool checkingGranted;
	
	[SerializeField] private int checkId;

	[SerializeField] private UIManager _uiManager;
	public GameObject darkImage;
	[SerializeField] private GameObject winDisplay;
	[SerializeField] private GameObject particleDisplay;

	private int bonusId = 10;
	private int wildId = 12;
	[SerializeField] private List<Sprite> wildImaged;
	[SerializeField] private bool containsWild;
	[SerializeField] private bool wildOnSetReel;
	
	[SerializeField] private int tier=0;
	public int tier2probability = 2;
	public int tier1probability = 20;
	[SerializeField] private List<int> diffItemColomnWise;

	public bool bonusGameEnabled;
	public List<GameObject> bonusGameModes;
	[SerializeField] private bool bonusGameInitiated;
	public bool bonusGameDetected;
	[SerializeField] private int WildForScatter;

	[SerializeField] private GameObject loseSound;

	[SerializeField] private float oldBalance;
	[SerializeField] private float newBalance;
	[SerializeField] private int spinCount;
	[SerializeField] private float betAmount;
	public float winAmount;
	[SerializeField] private float RTP;
	
	private void Start()
	{
		Initialize();
		SetReel();    // setting reel initially.
		UIManager.OnSpin += Spin;
		UIManager.OnStop += Stop;
	}

	private void OnDestroy()
	{
		UIManager.OnSpin -= Spin; 
		UIManager.OnStop -= Stop;
	}

	private void Initialize()
	{
		reelOnStop = new int[rowCount, columnCount];
	}

	private bool isSimulation;
	public float SimulateSpin()
	{
		isSimulation = true;
		ResetVariables(); 
		SetReel();
		Checking();


		spinCount = _uiManager.spinCount;
		betAmount = _uiManager.ReturnBetAmount();    
		RTP = (winAmount / betAmount) * 100;
		
		Debug.Log("rtp: "+RTP +"   winAmount: "+winAmount+ "   bet: "+betAmount);
		return RTP;
	}
	
	
	private void Spin()
	{
		isSimulation = false;
		
		StopAllCoroutines();
		SetReel();
		
		ResetScaleAnimation();
		ResetVariables();       
		
		loseSound.SetActive(false);
	}

	private void Stop()
	{
		StartCoroutine(WaitForCheckGranted());
		// GenerateTier();
	}

	private IEnumerator WaitForCheckGranted()
	{
		yield return new WaitUntil(() => checkingGranted);
		yield return new WaitForSeconds(0.5f);
		Checking();
	}
	
	public void SetOldBalance(float balance)
	{
		oldBalance = balance;      //before reducing bet amount 
		Debug.Log("old balance: "+oldBalance);
	}
	
	private void Checking()
	{
		spinCount = _uiManager.spinCount;
		betAmount = _uiManager.ReturnBetAmount(); 
		
		for (var i = 0; i < 3; i++)
		{
			checkId = reelOnStop[i, 0]; 
			CheckWin(checkId ,i); 
		}
		
		RTP = (winAmount / betAmount) * 100;
		RTP = Mathf.Round(RTP * 100f) / 100f;  //upto 2decimal values
		newBalance = _uiManager.ReturnBalance() + winAmount;   // after adding win amount
		Debug.Log("new balance: "+newBalance);

		StartCoroutine(PlayerActivityMethod());
		
		if(isSimulation) return;
		switch (overAllHasWon)
		{
			case false:
				loseSound.SetActive(true);
				_uiManager.AllowSpin();
				break;
			case true:
				_uiManager.SetWinAmount(winAmount);
				_uiManager.WinAmountUpdateMethod(winAmount);
				break;
		}

		if (bonusGameDetected)          // for first spin of bonus game
		{
			bonusGameEnabled = true;
			bonusGameDetected = false;
		}
		_uiManager._randomForSparkle = _uiManager.spinCount + Random.Range(1,3);        // for sparkle effect
		_uiManager.InteractableButton(true);      // at the very end
	}



	#region SetReel


	// private float[] symbolProbabilities;
	// Probabilities for each symbol (Symbol IDs: 1 to 13)
	private float[] symbolProbabilities = new float[]
	{
		0.2f, 0.1f, 0.1f, 0.2f, 0.1f, 0.1f,0.2f
		 //0.05f, 0.05f, 0.05f, 0.05f, 0.05f, 0.05f, 0.05f
	};

	// public void SetSymbolProbabilities(float[] tunedProbabilities)
	// {
	// 	symbolProbabilities = tunedProbabilities;
	// }
	
	
	
	// Get a random symbol ID based on weighted probabilities.
	
	private int GetWeightedRandomSymbol(float[] probabilities)
	{
		float total = 0;
		foreach (float prob in probabilities)
			total += prob;

		float randomPoint = Random.value * total;

		for (int i = 0; i < probabilities.Length; i++)
		{
			if (randomPoint < probabilities[i])
				return i + 1; // Symbol IDs start from 1
			randomPoint -= probabilities[i];
		}

		return probabilities.Length; // Default to the last symbol
	}

	private void SetReel()
	{
		for (int j = 0; j < columnCount; j++)
		{
			for (int i = 0; i < rowCount; i++)
			{
				int id = GetWeightedRandomSymbol(symbolProbabilities);
				reelOnStop[i, j] = id; // Assign the ID to the reel position
				// Debug.Log("i :"+ i+ "j: "+j + "id : "+ id);
			}
		}
		reelComplete = true; // Mark the reel generation as complete
	}

	#endregion
	
	
	//
	// private IEnumerator SetReel()
	// {
	// 	for (int j = 0; j < columnCount; j++)  // Iterate through each column
	// 	{
	// 		int i = 0;
	// 		while (i < rowCount)
	// 		{
	// 			// Select a symbol based on probabilities
	// 			int symbolId = GetWeightedRandomSymbol(symbolProbabilities);
	//
	// 			// Get cluster size for the current symbol
	// 			int clusterLength = GetClusterSizeForSymbol(symbolId);
	//
	// 			// Limit cluster size to fit within the reel
	// 			clusterLength = Mathf.Min(clusterLength, rowCount - i);
	//
	// 			// Place the cluster on the reel
	// 			for (int k = 0; k < clusterLength; k++)
	// 			{
	// 				reelOnStop[i, j] = symbolId;
	// 				i++;
	// 				yield return null;
	// 			}
	// 		}
	// 	}
	// }
	//
	//
	//
	// int GetClusterSizeForSymbol(int symbolId)
	// {
	// 	// Define cluster probabilities for the symbol
	// 	float[] clusterSizeProbabilities = GetClusterSizeProbabilitiesForSymbol(symbolId);
	//
	// 	// Use a weighted random selection to determine cluster size
	// 	return GetWeightedRandomSymbol(clusterSizeProbabilities) + 1; // Base size starts at 1
	// }
	//
	//
	// float[] GetClusterSizeProbabilitiesForSymbol(int symbolId)
	// {
	// 	switch (symbolId)
	// 	{
	// 		case 1: // Symbol 1 probabilities
	// 			return new float[] { 0.3f, 0.25f, 0.2f, 0.15f, 0.1f }; // 30% single, 25% double, 20% triple, 15% quad, 10% quintuple
	//
	// 		case 2: // Symbol 2 probabilities
	// 			return new float[] { 0.4f, 0.3f, 0.15f, 0.1f, 0.05f }; // Higher likelihood of smaller clusters
	//
	// 		case 3: 
	// 			return new float[] { 0.4f, 0.3f, 0.15f, 0.1f, 0.05f };
	// 		
	// 		case 4:
	// 			return new float[] { 0.4f, 0.3f, 0.15f, 0.1f, 0.05f };
	// 		
	// 		case 5: 
	// 			return new float[] { 0.4f, 0.3f, 0.15f, 0.1f, 0.05f };
	// 		
	// 		default: // Default probabilities for other symbols
	// 			return new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f }; // Even distribution for all cluster sizes
	// 	}
	// }
	//
	//
	
	
	
	public Sprite GetSpriteOnStop(int i, int j)
	{
		if (reelOnStop[i, j] == 12)
		{
			return bonusGameEnabled ? wildImaged[WildForScatter] : wildImaged[Random.Range(0, 5)];
		}
		return items[reelOnStop[i, j]].image;
	}

	public Sprite GetSpriteNonDisplayItems()
	{
		return items[Random.Range(0, 13)].image;
	}
	
	
	public int GetItemId(int i, int j)
	{
		return reelOnStop[i, j];
	}

	private void CheckWin(int checkid, int x)
	{
		ResetVariablesDuringCheckWin();
		winLineCheckInGameItemList.Add(placementId[x].column[0]);
		var isBonusCheckId = (checkid == bonusId);        // is bonus in starting column?
		
		for (var j = 1; j < columnCount; j++)
		{
			for (var i = 0; i < rowCount; i++)
			{
				Label: 
				if (labelPass)
				{
					if (match)
					{
						if (winCheckList.Count == 0)
						{
							CopyList(j);    // to copy last winLine
							if(isSimulation) return;
							if (hasWon) {    StartCoroutine(DisplayWinlines());    }
							return;
						}
						j = winCheckList[winCheckList.Count - 1];
						i = winCheckList[winCheckList.Count - 2];
						
						CopyList(j);
						winCheckList.RemoveRange(winCheckList.Count - 2, 2);
						labelPass = false;
					}
					else   // not even a single match
					{
						return;
					} 
				}
				
				if ((isBonusCheckId && checkid == reelOnStop[i, j]) || (!isBonusCheckId && (checkid == reelOnStop[i, j] || reelOnStop[i, j] == wildId)))      // MATCH
				{
							match = true;
                            itemcount++;
                            winLineCheckInGameItemList.Add(placementId[i].column[j]);
                
                            if (j == 4)   // for the last column
                            {
                                    if (i == 0 || i == 1) { CopyList(j); continue; }
                                    if (i == 2) { CopyList(j); labelPass = true; goto Label; }
                            }
                
                            if (i == 1 || i == 0) { winCheckList.Add(i + 1); winCheckList.Add(j); }   // adding lower items for future checking in a list
                            break;
				}

				if (checkid != reelOnStop[i, j])          // NOT MATCH
				{
					if (i < 2) { continue; }
					if (i == 2) {   labelPass = true;   goto Label;   }
				} 
			}
		}
	}
	
	private void CopyList(int j)
	{
		if (itemcount >= 3)
		{
			hasWon = true;
			overAllHasWon = true;
			
			if (winLineCheckInGameItemList[0].id == bonusId)     // to check scatter
			{  
				bonusGameDetected = true;      
				bonusGameInitiated = true;     // for bonus mode
			}
			
			if (winLineCheckInGameItemList.Count >= max)
			{
			        winLineStoreInGameItemsList.Add(new winLineStore());
			     	max = winLineCheckInGameItemList.Count;
				
				for (int m = 0; m < winLineCheckInGameItemList.Count; m++)
				{
					winLineStoreInGameItemsList[^1].winline.Add(winLineCheckInGameItemList[m]);   // add from the last
				}
				
				
				for (int q = 1; q <= winLineCheckInGameItemList.Count-1; q++)    // check the list before adding win for wild
				{
					if (winLineCheckInGameItemList[q].id == wildId)
					{
						containsWild = true;
						break;     
					}
					containsWild = false;
				}
				
				if (!bonusGameEnabled)
				{
					switch (containsWild)
					{
						case false:
							AddWin(checkId, 1);
							break;
						case true:
							AddWin(checkId, 2);
							break;
					}
				}
				if (bonusGameEnabled)
				{
					MultiplierSelectionForBonus();
				}
			}
			RemoveItemFromWinLineCheckList(j);
		}
		else {     RemoveItemFromWinLineCheckList(j);    }
	}

	private void RemoveItemFromWinLineCheckList(int j)
	{
		for (int t = j; t < itemcount; t++)     // exception 1
		{
			winLineCheckInGameItemList.RemoveAt(winLineCheckInGameItemList.Count - 1);
		}       
		itemcount = j ;
	}

	private void AddWin(int checkId, int multiplier)
	{
		winAmount += ((items[checkId].itemCountMultiplier[5 - itemcount].countMultiplier * _uiManager.ReturnBetAmount() ) / 30) * multiplier;
		// Debug.Log($"checkId:  {checkId}  multiplier : {items[checkId].itemCountMultiplier[5 - itemcount].countMultiplier}  betAmount :  {_uiManager.BetAmountMethod()} multiplierBonus : {multiplier} ");
		// Debug.Log($"winAmount in addwin: {winAmount}");
	}

	private void MultiplierSelectionForBonus()
	{
		int rand = Random.Range(1, 100);
		if (rand < 2)
		{
			AddWin(checkId, multiplierInScatter[WildForScatter].multiplier[2]);
		}
		else if (rand < 6)
		{
			AddWin(checkId, multiplierInScatter[WildForScatter].multiplier[1]);
		}
		else if (rand < 10)
		{
			AddWin(checkId, multiplierInScatter[WildForScatter].multiplier[0]);
		}
		else if (rand < 100)
		{
			AddWin(checkId, 1);
		}
	}
	
	IEnumerator DisplayWinlines()
	{
		winDisplay.SetActive(true);
		particleDisplay.SetActive(true);
		
		Invoke(nameof(ResetWinDisplay),1.9f);
		Invoke(nameof(ResetParticleSystem),3f);
		while (true) 
		{
			darkImage.SetActive(true);
			yield return null;
			for (var m = 0; m < winLineStoreInGameItemsList.Count; m++)
			{
				for (int n = 0; n < winLineStoreInGameItemsList[m].ElementCount(); n++)
				{
					winLineStoreInGameItemsList[m].winline[n].GameItemSortingOrder(true);
					tmpItemScaling[n] = winLineStoreInGameItemsList[m].winline[n].ScaleAnimation();
					tmpItemScaling[n].SetBool("scaling",true);
				}
				yield return new WaitForSeconds(2f);
				for (int n = 0; n < winLineStoreInGameItemsList[m].ElementCount(); n++)
				{
					winLineStoreInGameItemsList[m].winline[n].GameItemSortingOrder(false);
					tmpItemScaling[n].SetBool("scaling",false);
				}

				if (!_uiManager._autoSpinStatus && !bonusGameEnabled)   // win case 1
				{
					_uiManager.AllowSpin();
				}
			}
			
			if(_uiManager._autoSpinStatus && !bonusGameEnabled)    // win case 2      
			{
				_uiManager.AllowSpin();
			}
			
			if (bonusGameInitiated)
			{
				yield return new WaitForSeconds(0.5f);
				darkImage.SetActive(false);
				BonusGameModeDisplay(true);
				bonusGameInitiated = false;
			}
			
			if (bonusGameEnabled) 
			{
				_uiManager.AllowSpin();
				yield break;
			}
		}
	}

	private void ResetScaleAnimation()            // do it during spin
	{
		for (var m = 0; m < winLineStoreInGameItemsList.Count; m++)
		{
			for (var n = 0; n < winLineStoreInGameItemsList[m].ElementCount(); n++)
			{
				winLineStoreInGameItemsList[m].winline[n].GameItemSortingOrder(false);
				tmpItemScaling[n].SetBool("scaling",false);
			}
		}
	}

	private void ResetVariables()           // done during spin and each checkid checkwin.
	{
		labelPass = false;
		match = false;
		winCheckList.Clear();
		winLineCheckInGameItemList.Clear();
		hasWon = false;
		itemcount = 1;
		max = 0;
		
		winLineStoreInGameItemsList.Clear();
        winAmount = 0;
        
        if(!isSimulation)
			darkImage.SetActive(false);
        wildOnSetReel = false;
        containsWild = false;
        overAllHasWon = false;
        checkingGranted = false;
	}

	private void ResetVariablesDuringCheckWin()
	{
		
		labelPass = false;
		match = false;
		winCheckList.Clear();
		winLineCheckInGameItemList.Clear();
		hasWon = false;
		itemcount = 1;
		max = 0;

	}
	
	public void ResetWinDisplay()
	{
		winDisplay.SetActive(false);
	}

	public void ResetParticleSystem()
	{
		particleDisplay.SetActive(false);
	}

	public void BonusGameModeDisplay(bool status)
	{
		for (var i = 0; i < 5; i++)
		{
			bonusGameModes[i].SetActive(status);
		}
	}

	public void WildForScatterMethod(int id)
	{
		WildForScatter = id;
	}
	
	
	
	IEnumerator PlayerActivityMethod()
	{
		WWWForm form = new WWWForm();
		
		
		// // Debug the variables
		// Debug.Log("User ID: " + DBManager.userID);
		// Debug.Log("Old Balance: " + oldBalance);
		// Debug.Log("New Balance: " + newBalance);
		// Debug.Log("Spin Count: " + spinCount);
		// Debug.Log("Bet Amount: " + betAmount);
		// Debug.Log("Win Amount: " + winAmount);
		// Debug.Log("RTP: " + RTP);
		
		
		
		form.AddField("user_id", DBManager.userID.ToString());
		form.AddField("old_balance", oldBalance.ToString());
		form.AddField("new_balance", newBalance.ToString());
		form.AddField("spin_count", spinCount.ToString());
		form.AddField("bet", betAmount.ToString());
		form.AddField("win", winAmount.ToString());
		form.AddField("RTP", RTP.ToString());

		using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/playerActivity.php", form))
		{
			// Wait for the request to complete
			yield return www.SendWebRequest();

			// Check for errors
			if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.LogError("Error: " + www.error);
			}
			else
			{
				// Log server response
				Debug.Log("Server Response: " + www.downloadHandler.text);
			}
		}
	}

	
}
	


[System.Serializable]
public class Grid      // another list
{
      public List<GameItem> column;
}

[System.Serializable]
public class winLineStore
{
	public List<GameItem> winline=new List<GameItem>();
	public int ElementCount()
	{
		return winline.Count;
	}
}

[System.Serializable]
public class MultiplierInScatter
{
	public List<int> multiplier;
}