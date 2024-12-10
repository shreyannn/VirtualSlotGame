using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
  public List<GameObject> items;

  [SerializeField] private float speed;

  [SerializeField] private float startTime = 0.3f;
  [SerializeField] private int reelId;

  [SerializeField] private GameObject reeStopSound;
  [SerializeField] private UIManager _uiManager;
  [SerializeField] private Animator spinAnimator;
  private void Start()
  {
	  UIManager.OnSpin += Spin;
	  UIManager.OnStop += Stop;
  }

  private void OnDestroy()
  {
	  UIManager.OnSpin -= Spin;
	  UIManager.OnStop -= Stop;
  }

  private void Spin()
  {
	  speed = 15;
	  spinAnimator.SetBool("spin",true);
	  spinAnimator.SetBool("stop",false);
  }

  private void Stop()
  {
	  if (_uiManager.spinCount == _uiManager._randomForSparkle && reelId >= _uiManager.startingReelForSparkle)
	  {
		  StartCoroutine(StopOneByOneSparkleEffect());
		  return;
	  }
	  Invoke(nameof(StopOneByOne),startTime*reelId);
  }

  private float temp;
  IEnumerator StopOneByOneSparkleEffect()
  {
	  if (reelId > _uiManager.startingReelForSparkle)
	  {
		  temp = (startTime * reelId) + (1.2f);
	  }
	  if (reelId == _uiManager.startingReelForSparkle)
	  {
		  temp = (startTime * reelId); 
	  }
	  
	  // Debug.Log($"REEL id: {reelId} tm : {temp}");
	  yield return new WaitForSeconds(temp);
	
	  if (reelId == _uiManager.startingReelForSparkle)
	  {
		  _uiManager.sparkleInColumnGameObject[_uiManager.ReturnStartingReelForSparkle()].SetActive(true);
		  speed = 28;
		  yield return new WaitForSeconds(1f);
		  StopOneByOne();
		  yield return new WaitForSeconds(0.2f);
		  _uiManager.sparkleInColumnGameObject[_uiManager.ReturnStartingReelForSparkle()].SetActive(false);
		  yield break;
	  }
	  StopOneByOne();
  }

  private void StopOneByOne()
  {
	  reeStopSound.SetActive(true);
	  spinAnimator.SetBool("spin",false);
	  spinAnimator.SetBool("stop",true);
	 
	  Invoke(nameof(StopReelSound),0.3f);   //sound

	  if (reelId == 4)
	  {
		  GameManager.Instance.checkingGranted = true;
	  }
  }

  private void StopReelSound()
  {
	  reeStopSound.SetActive(false);
  }
  
}