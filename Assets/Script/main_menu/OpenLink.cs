using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
   public void RegisterOnline()
   {
      Application.OpenURL("http://localhost:8000/register");
   }
   
   public void OpenDashboard()
   {
      Application.OpenURL("http://localhost:8000");
   }
}
