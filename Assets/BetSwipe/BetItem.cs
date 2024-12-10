// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// namespace Kauda
// {
//     public class BetItem : MonoBehaviour
//     {
//       
//         [SerializeField] private string targetStatus;
//         [SerializeField] private int speed; 
//         [SerializeField] private Vector3 targetPosition;
//         
//       
//
//         [SerializeField] private Transform Buttom_First;
//         [SerializeField] private Transform Buttom_Second;
//         [SerializeField] private Transform Button_Third;
//         [SerializeField] private Transform Button_Fourth;
//         
//         [SerializeField] private Transform Top_First; 
//         [SerializeField] private Transform Top_Second;
//         [SerializeField] private Transform Top_Third;
//         [SerializeField] private Transform Top_Fourth;
//
//         [SerializeField] private ColorfulOrchardSelectBet colorfulOrchardSelectBet;
//
//
//        
//
//
//         #region Direction And Speed
//
//         private void DetermineSpeed()
//         {
//             switch (targetStatus)
//             {
//                 case "up" or "down":
//                     speed = 500;
//                     break;
//                 case "up2" or "down2":
//                     speed = 750;
//                     break;
//             }
//
//             if (colorfulOrchardSelectBet.maxSpeed)
//             {
//                 speed = 1500;
//             }
//             
//         }
//         public void SetTargetStatus(string status)
//         {
//             Vector3 direction = Vector3.zero;
//             float distance = 0;
//
//             switch (status)
//             {
//                 case "up":
//                     direction = Vector3.up;
//                     distance = 110.88f;
//                     break;
//                 case "up2":
//                     direction = Vector3.up;
//                     distance = 221.76f;
//                     break;
//                 case "down":
//                     direction = Vector3.down;
//                     distance = 110.88f;
//                     break;
//                 case "down2":
//                     direction = Vector3.down;
//                     distance = 221.76f;
//                     break;
//             }
//
//             CancelInvoke(nameof(StopUpdate));
//             StartUpdate();
//             targetPosition = gameObject.transform.localPosition + direction * distance;
//            
//             targetStatus = status;
//             DetermineSpeed();
//             StartCoroutine(WaitTimeToReachTarget());
//         }
//
//         
//
//         #endregion
//
//
//         #region RePositioning
//
//         private IEnumerator WaitTimeToReachTarget()
//         {
//             yield return new WaitUntil(() => colorfulOrchardSelectBet.TargetReached);
//             yield return new WaitForSeconds(0.05f);
//             AdjustBetItems();
//         }
//         private void AdjustBetItems()
//         {
//             var temp = targetStatus;
//             targetStatus = null;
//             switch (temp)
//             {
//                 case "up" or "down":
//                     PositionAdjustmentInSingleIncrement();
//                     break;
//                 case "up2" or "down2":
//                    PositionAdjustmentInDoubleIncrement();
//                     break;
//             }
//             colorfulOrchardSelectBet.TargetReached =false;
//           
//         }
//       
//         private void PositionAdjustmentInSingleIncrement()     // up and down
//         {
//             var currentPos = gameObject.transform.localPosition;
//             if (currentPos.y < Buttom_Second.transform.localPosition.y -50)
//             {
//                 gameObject.transform.localPosition = Top_Third.transform.localPosition;
//                 return;
//             }
//             if (currentPos.y > Top_Third.transform.localPosition.y +50)
//             {
//                 gameObject.transform.localPosition = Buttom_Second.transform.localPosition;
//                  
//             }
//         }
//         private void PositionAdjustmentInDoubleIncrement()     // up2 and down2
//         {
//             PositionAdjustmentForBottom();
//             PositionAdjustmentForTop();
//         }
//
//         private void PositionAdjustmentForBottom() 
//         {
//             var currentPos = gameObject.transform.localPosition;
//             if (currentPos.y < Button_Fourth.transform.localPosition.y+50)
//             {
//                 gameObject.transform.localPosition = Top_Fourth.transform.localPosition;
//                 return;
//             }
//             if (currentPos.y < Button_Third.transform.localPosition.y+50)
//             {
//                 gameObject.transform.localPosition = Top_Third.transform.localPosition;
//             } 
//         }
//         private void PositionAdjustmentForTop() 
//         {
//             var currentPos = gameObject.transform.localPosition;
//             if (currentPos.y > Top_First.transform.localPosition.y-50)
//             {
//                 gameObject.transform.localPosition = Buttom_First.transform.localPosition;
//                 return;
//             }
//             if (currentPos.y > Top_Second.transform.localPosition.y-50)
//             {
//                 gameObject.transform.localPosition = Buttom_Second.transform.localPosition;
//             }
//         }
//
//
//
//
//         #endregion
//
//
//         #region Update Start (mono)
//
//         private void Update()
//         {
//             if (targetStatus == null) return;
//             
//             var step = speed * Time.deltaTime;
//             var currentPosition = gameObject.transform.localPosition;
//
//             var movingUp = targetStatus is "up" or "up2";
//             var movingDown = targetStatus is "down" or "down2";
//             
//             if ((movingUp && currentPosition.y < targetPosition.y) || (movingDown && currentPosition.y > targetPosition.y))
//             {
//                 gameObject.transform.localPosition = Vector3.MoveTowards(currentPosition, targetPosition, step);
//                 return;
//             }
//             colorfulOrchardSelectBet.TargetReached = true;
//             Invoke(nameof(StopUpdate),2f);
//         }
//         
//         private void Start()
//         {
//             StopUpdate();
//         }
//
//         private void StartUpdate()
//         {
//             enabled = true;
//         }
//
//         private void StopUpdate()
//         {
//             enabled = false;
//         }
//        
//         #endregion
//         
//         
//     }
// }
