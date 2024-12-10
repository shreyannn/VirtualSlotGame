using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
        public static string userName;
        // public static string userID;
        public static int userID;
        public static float userBalance;
        public static int userSpinCount;
        public static bool LoggedIn { get { return userName != null; } }
        
        public static void LogOut()
        {
            userName = null;
            userID = 0;
            userBalance = 0;
            userSpinCount = 0;
        }
        
        
}
