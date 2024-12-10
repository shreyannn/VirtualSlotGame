using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameItemNonDisplay : MonoBehaviour
{ 
	public Image itemImage;
	
	private void Start()
	{
		itemImage.sprite = GameManager.Instance.GetSpriteNonDisplayItems();
	}
}