using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameItem : MonoBehaviour
{
	[SerializeField] private int reelId;
	[SerializeField] private int positionId;
	[SerializeField] private int placementId;
	[SerializeField] private Animator scaleAnimation;
	public Image itemImage;
	public Canvas _canvas;
	public int id;
	
	public void Start()
	{
		StartCoroutine(RandomItemOnStart());
		UIManager.OnStop += ChangeSprite;
	}

	public void OnDestroy()
	{
		UIManager.OnStop -= ChangeSprite;
	}

	IEnumerator RandomItemOnStart()      // when game is started
	{
		yield return new WaitUntil(() => GameManager.Instance.reelComplete);
		itemImage.sprite = GameManager.Instance.GetSpriteOnStop(positionId, reelId);      // each item has it own script 
	}

	private void ChangeSprite()
	{
		id = GameManager.Instance.GetItemId(positionId, reelId);             // for checking bonus game
		itemImage.sprite = GameManager.Instance.GetSpriteOnStop(positionId, reelId);
	}
	
	
	public Animator ScaleAnimation()
	{
		return scaleAnimation;
	}

	public void GameItemSortingOrder(bool sorting)
	{
		_canvas.overrideSorting = sorting;
	}
	
}
