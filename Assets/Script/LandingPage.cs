using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPage : MonoBehaviour
{
   public void PlayNow()
   {
      UnityEngine.SceneManagement.SceneManager.LoadScene(1);
   }
   
   
   
   [SerializeField] private List<GameObject> infos;
   [SerializeField] private GameObject infoPanel;
   [SerializeField] private int infoCount =0;
   
   
   public void Info()
   {
      Debug.Log("info selected");
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
   
   [SerializeField] private GameObject AudioManager;
   
   [SerializeField] private GameObject backgroundMusic;
   [SerializeField] private GameObject backgroundMusicUnactive;
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

   public bool ReturnBackGroundMusic()
   {
      return bgMusicStatus;
   }
   
   private void Start()
   {
      DontDestroyOnLoad(AudioManager);
   }

}
