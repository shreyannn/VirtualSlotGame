using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Serialization; // for action
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{ 
      public static Action OnSpin;
      public static Action OnStop;
      
      [SerializeField] private List<float> betAmounList; 
      [FormerlySerializedAs("betListCount")] [SerializeField] private int betIndex =0;
      [SerializeField] private TextMeshProUGUI betAmountText;
      [SerializeField] private float betAmount;

      [SerializeField] private List<GameObject> infos;
      [SerializeField] private GameObject infoPanel;
      [SerializeField] private int infoCount =0;

      [SerializeField] private GameObject stopButton;
      [SerializeField] private float coinAmount;
      public TextMeshProUGUI coinText;

      [SerializeField] private float currentTime;
      [SerializeField] private float maxTime = 2f;
      [SerializeField] private bool startCountingTime;
      [SerializeField] private bool stopTimer;
      [SerializeField] private bool spinTimer = true;

      [SerializeField] private TextMeshProUGUI winDisplayAmount;
      
      public int spinCount;
      public bool isScatterPossible;
      public bool isWildPossible;
      public bool isSparkleActive;
      public int _randomForScatter;
      public int _randomForWild;
      public int _randomForSparkle;
      [FormerlySerializedAs("slotNumSparkle")] public int startingReelForSparkle;
      public List<GameObject> sparkleInColumnGameObject;

      public bool _autoSpinStatus;
      [SerializeField] private GameObject stopAutoSpinButton;
      
      [SerializeField] private TextMeshProUGUI winAmountText;

      [SerializeField] private GameObject countDown;
      [SerializeField] private TextMeshProUGUI countDownText;

      [SerializeField] private GameObject leftSideUi;
      [SerializeField] private GameObject rightSideUi;
      [SerializeField] private GameObject SpinStopUi;

      [SerializeField] private GameObject bonusWinDisplay;
      [SerializeField] private TextMeshProUGUI bonusWinAmountText;
      [SerializeField] private float bonusWinAmount;

      [SerializeField] private Button minusButton;
      [SerializeField] private Button plusButton;
      [SerializeField] private Button maxBetButton;
      [SerializeField] private Button autoButton;
      
      
      [SerializeField] private GameObject backgroundMusic;
      [SerializeField] private GameObject backgroundMusicUnactive;
      [SerializeField] private GameObject spinAudio;

      [SerializeField] private GameObject autoSpinOptions;
      [SerializeField] private GameObject autoXIndexGameObject;
      [SerializeField] private TextMeshProUGUI autoXIndexText;
      
      
      [SerializeField] private BalanceUpdater balanceUpdater;
      
      
      private void Start()
      {
            _randomForScatter = Random.Range(8,15);
            _randomForWild = Random.Range(1,3);
            _randomForSparkle = Random.Range(1,2);

            if (DBManager.userName == null)
                  coinAmount = 100; // guest mode
            else
                  coinAmount = DBManager.userBalance;

            spinCount = DBManager.userSpinCount;
            
            coinText.text = coinAmount.ToString("0.00");
           
      }

      private void Update()
      {
            betAmount  =  betAmounList[betIndex];
            if (Time.time - currentTime > maxTime && startCountingTime)
            {
                  Stop();
                  startCountingTime = false;
            }
      }


      public float ReturnBalance()
      {
            return coinAmount;
      }
      #region Spin-Stop


      public void Spin()
      {
            if (!spinTimer) return;
            if (OnSpin != null && coinAmount >= betAmount)
            {
                  OnSpin();
                  Invoke(nameof(AllowStop),1f);
                  spinTimer = false;
                  currentTime = Time.time;
                  startCountingTime = true;
                  stopButton.SetActive(true);

                  GameManager.Instance.SetOldBalance(coinAmount);
                  if (!GameManager.Instance.bonusGameEnabled)
                  {
                        coinAmount -= betAmount;
                        winAmountText.text = 0.ToString("0.00");
                  }
                  
                        coinText.text = coinAmount.ToString("0.00");
                        balanceUpdater.UpdateBalance(DBManager.userID, coinAmount); 
                        
                  isWildPossible= false;
                  isScatterPossible= false;
                  isSparkleActive = false;
                  
                  
                  spinCount++;    // Spin Count
                  
                  if (spinCount == _randomForScatter)         //????
                  {
                        isScatterPossible = true;
                        _randomForScatter = spinCount + Random.Range(8,15);
                  }
                  
                  if (spinCount == _randomForWild)
                  {
                        isWildPossible = true;
                        _randomForWild = spinCount + Random.Range(1,3);
                  }
                  
                  if (spinCount == _randomForSparkle)
                  {
                        startingReelForSparkle = Random.Range(2, 5);
                        isSparkleActive = true;
                  }
                  
                  if (_autoSpinStatus) {      stopAutoSpinButton.SetActive(true);      }      // Stop Button UI
                  else {      stopButton.SetActive(true);      }
                  
                  InteractableButton(false);
                  
                  spinAudio.SetActive(true);      //Audio
            }
      }
      
      public void Stop()
      {
            if (!stopTimer) return;
            if (OnStop != null)
            {
                  OnStop();
                  stopTimer = false;
                  stopButton.SetActive(false);
                  startCountingTime = false;
                  spinAudio.SetActive(false);   // Audio
            }
      }

      public void AllowSpin()
      {
            spinTimer = true;
      }
      private void AllowStop()
      {
            stopTimer = true;
      }
      

      #endregion

      
      #region Bet Amount
      
      public void IncreaseBetAmount()
      {
            betIndex++;
            if (betIndex == 18)
            {
                  betIndex = 0;
            }
            betAmountText.text = betAmounList[betIndex].ToString("0.00");
      }
      
      public void DecreaseBetAmount()
      {
            betIndex--;
            if (betIndex <0)
            {
                  betIndex = 17;
            }
            betAmountText.text = betAmounList[betIndex].ToString("0.00");
      }

      public float ReturnBetAmount()
      {
            return betAmounList[betIndex];
      }

      #endregion


      #region Win Amount


    
      
      public void SetWinAmount(float winAmount)
      {
            if (!GameManager.Instance.bonusGameEnabled)
            {
                  coinAmount += winAmount;
                  coinText.text = coinAmount.ToString("0.00");
                  
                        balanceUpdater.UpdateBalance(DBManager.userID, coinAmount);    //*****************updating balance
                        
                  StartCoroutine(IncreasingWinAmount(winAmount));
                  return;
            }
            StartCoroutine(IncreasingWinAmount(winAmount));
      }
      
      
      IEnumerator IncreasingWinAmount(float winAmount)
      {
            var duration = 1f;
            var elapsedTime = 0f;
            var startAmount = 0f; 

            while (elapsedTime < duration)
            {
                  elapsedTime += Time.deltaTime; 
                  float currentAmount = Mathf.Lerp(startAmount, winAmount, elapsedTime / duration); 
                  winDisplayAmount.text = currentAmount.ToString("0.00"); 
                  yield return null;
            }
            winDisplayAmount.text = winAmount.ToString("0.00"); 
      }

      public void WinAmountUpdateMethod(float winAmount)    // to clear
      {
            if (!GameManager.Instance.bonusGameEnabled)
            {
                  winAmountText.text = winAmount.ToString("0.00");
                  return;
            }
            bonusWinAmount += winAmount;
            winAmountText.text = bonusWinAmount.ToString("0.00");
      } 

      #endregion

      
      #region Max Bet

      public void MaxBet()
      {
            betIndex = 17;
            betAmountText.text = betAmounList[betIndex].ToString("0.00");
      }
      
      #endregion


      #region Info
      
      public void Info()
      {
            infoPanel.SetActive(true);
            infos[infoCount].SetActive(true);
      }

      public void NextInfo()
      {
            infoCount++;
            if (infoCount == 3)
            {
                  infoCount = 0;
            }
            infos[infoCount].SetActive(true);
            
            if(infoCount == 0) 
            {
                  infos[2].SetActive(false);
            }
            else
            {
                  infos[infoCount-1].SetActive(false);
            }
      }

      public void BackInfo()
      {
            infoPanel.SetActive(false);
            infos[infoCount].SetActive(false);
      }
      
      
      #endregion
      
      
      #region Sparkle 
      
      //Sparkle Effect
      public int ReturnStartingReelForSparkle()
      {
            return startingReelForSparkle;
      }
      
      #endregion
      
      
      #region Auto Spin

      public void AutoSpinButton()
      {
            autoSpinOptions.SetActive(true);
      }
      public void AutoSpinInfinite()
      {
            autoSpinOptions.SetActive(false);
            _autoSpinStatus = true;
            StartCoroutine(InfiniteSpin());
      }
      
      IEnumerator InfiniteSpin()
      {
            while (true)
            {
                  yield return new WaitUntil( () => spinTimer );
                  
                  if(!_autoSpinStatus  ||  GameManager.Instance.bonusGameEnabled)
                  { 
                        stopAutoSpinButton.SetActive(false);
                        break;
                  }
                  if (coinAmount >= betAmount)
                  {
                        Spin();
                  }
                  else
                  {
                        stopAutoSpinButton.SetActive(false);
                        break;
                  }
            }
      }

      
      public void AutoSpinX(int count)
      {
            autoSpinOptions.SetActive(false);
            
            autoXIndexText.text = count.ToString();
            autoXIndexGameObject.SetActive(true);
            
            _autoSpinStatus = true;
            StartCoroutine(AutoSpinXRoutine(count));
      }
      IEnumerator AutoSpinXRoutine(int count)
      {
            for (var i = count ; i > 0; i--)
            {
                  yield return new WaitUntil(() => spinTimer);
                  if (!_autoSpinStatus || GameManager.Instance.bonusGameEnabled)
                  {
                        stopAutoSpinButton.SetActive(false);
                        break;
                  }
                  if (coinAmount >= betAmount)
                  {
                        Spin();
                        autoXIndexText.text = i.ToString();
                  }
                  else
                  {
                        stopAutoSpinButton.SetActive(false);
                        break;
                  }
            }

            yield return new WaitForSeconds(3f);
            autoXIndexGameObject.SetActive(false);
            
      }
      
      public void StopAutoSpinButton()
      {
            autoXIndexGameObject.SetActive(false);
            stopAutoSpinButton.SetActive(false);
            _autoSpinStatus = false;
            StartCoroutine(StopAutoSpinCoroutine());
      }

      IEnumerator StopAutoSpinCoroutine()
      {
            yield return new WaitUntil(() => stopTimer);
            Stop();
      }

      #endregion


      #region Auto Spin For Bonus Game
      
      public void AutoSpinForBonusGame(int x)   // x -> number of spin
      {
            _autoSpinStatus = true;
            leftSideUi.SetActive(false);
            rightSideUi.SetActive(false);
            SpinStopUi.SetActive(false);
            countDown.SetActive(true);
            
            GameManager.Instance.BonusGameModeDisplay(false);
            StartCoroutine(SpinForBonusGame(x));
      }
    
      IEnumerator SpinForBonusGame(int x)
      {
            winAmountText.text = 0.ToString("0.00");
            countDownText.text = x.ToString();
            for (int i=x-1; i>= 0; i--)
            {
                  yield return new WaitUntil(() => spinTimer);
                  countDownText.text = i.ToString();
                  if(!_autoSpinStatus )
                  { 
                         break;
                  }
                  Spin();
            }

            yield return new WaitUntil(() => spinTimer);
            bonusWinDisplay.SetActive(true);
            bonusWinAmountText.text = bonusWinAmount.ToString("0.00");
            stopAutoSpinButton.SetActive(false);
      }

      #endregion

      
      public void TakeBonusWinAmount()
      {
            coinAmount += bonusWinAmount;
            coinText.text = coinAmount.ToString("0.00");
                  balanceUpdater.UpdateBalance(DBManager.userID, coinAmount); 
                  
            bonusWinDisplay.SetActive(false);
            leftSideUi.SetActive(true);
            rightSideUi.SetActive(true);
            SpinStopUi.SetActive(true);
            _autoSpinStatus = false;
          
            GameManager.Instance.darkImage.SetActive(false);
            GameManager.Instance.bonusGameEnabled = false; 
            countDown.SetActive(false);
            winAmountText.text = 0.ToString("0.00");
      }
      
      
      public void SetWildForScatterOnClick(int id) 
      {
            GameManager.Instance.bonusGameEnabled = true;
            GameManager.Instance.WildForScatterMethod(id);
      }
      
      public void InteractableButton(bool status)
      {
            minusButton.interactable = status;
            plusButton.interactable = status;
            maxBetButton.interactable = status;
            autoButton.interactable = status;
      }

      #region MyRegion
      
      
      [SerializeField] private bool bgMusicStatus=true;
      public void MusicStatus()
      {
            if (backgroundMusic.activeSelf)            
            {
                  backgroundMusic.SetActive(false);
                  backgroundMusicUnactive.SetActive(true);
                  bgMusicStatus = false;
                  return;
            }
            backgroundMusic.SetActive(true);
            backgroundMusicUnactive.SetActive(false);
            bgMusicStatus = true;
      }

      #endregion
      

      public void BackToMainMenu()
      {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
      }
}
