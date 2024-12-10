// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using Kauda.Core;
// using TMPro;
// using UnityEngine;
//
// namespace Kauda
// {
//     public class ColorfulOrchardSelectBet : MonoBehaviour
//     {
//         [SerializeField] private List<float> betList;
//         [SerializeField] private int index;
//         [SerializeField] private int oldIndex;
//         [SerializeField] private List<TextMeshProUGUI> betButtonText;
//         [SerializeField] private ColorfulOrchardUIManagerTesting colorfulOrchardUIManagerTesting;
//         [SerializeField] private ColorfulOrchardSpinTesting colorfulOrchardSpinTesting;
//
//         public bool maxSpeed;
//
//         [SerializeField] private List<BetItem> betItems;
//         [SerializeField] private GameObject blocker;
//         [SerializeField] private GameObject betButtonBlocker;
//         [SerializeField] private GameObject maxButtonBlocker;
//       
//         [SerializeField] private bool isAutomatedCall;
//         
//         //Define the mapping between IDs and status strings
//         private readonly Dictionary<int, string> _idToStatus = new Dictionary<int, string>
//         {
//             { 1, "down2" },
//             { 2, "down" },
//             { 3, "up" },
//             { 4, "up2" }
//         };
//
//         [SerializeField] private bool targetReached; 
//         public bool TargetReached
//         { 
//             get => targetReached;
//             set => targetReached = value;
//         }
//         
//
//         #region MaxBet and DefaultBet
//         
//         public void SetBetAmount(List<float> bList, int betPosition)
//         {
//             betList.AddRange(bList);
//             CreateBetList(betPosition);
//         }
//         
//         private void CreateBetList(int defaultIndex)
//         {
//             var listCount = betList.Count;
//             for (var i = 0; i < listCount; i++)
//             {
//                 betButtonText[i].text = betList[(defaultIndex - 4 + i + listCount) % listCount].ToString();
//             }
//             betButtonText[4].color=Color.white;
//             index = defaultIndex;
//             colorfulOrchardUIManagerTesting.SetBetAmount(index);
//         }
//
//         public void MaxBet()
//         {
//             if(selectBet)
//                 return;
//             blocker.SetActive(true);
//             betButtonBlocker.SetActive(true);
//             maxButtonBlocker.SetActive(true);
//             StartCoroutine(MaximumList(index));
//         }
//         
//         private IEnumerator MaximumList(int baseIndex)       // Sets the betList to max bet.
//         {
//             isAutomatedCall = true;
//
//             // Determine baseValue based on whether it's SetMaxList
//             var baseValue = baseIndex < 4 ? baseIndex + 1 : 9 - baseIndex;
//             
//             var intTemp = baseValue / 2;
//             var isWholeNumber = (baseValue % 2 == 0);
//             var selectBetValue = (baseIndex < 4) ? 1 : 4;
//             var finalBet = (baseIndex < 4) ? 2 : 3;
//
//             maxSpeed = true;
//             oldIndex = index;
//
//             for (var i = intTemp; i > 0; i--)                  
//             {
//                 SelectBet(selectBetValue);
//                 yield return new WaitForSeconds(0.23f);
//             }
//
//             if (!isWholeNumber)              // if the above calculation gives you whole number, you can reach the max amount with 2 increment movement.
//             {
//                 SelectBet(finalBet);
//             }
//
//             maxSpeed = false;
//             isAutomatedCall = false;
//
//             colorfulOrchardUIManagerTesting.SetBetAmount(index);
//             StartCoroutine(UpdateColorDuringMax());
//         }
//         
//         
//         #endregion
//         
//         
//         
//         #region Indexing  (Button Click)
//
//         private bool selectBet;
//         public void SelectBet(int id)
//         {
//             if (!isAutomatedCall && (blocker.activeSelf|| maxButtonBlocker.activeSelf)) 
//             {
//                 return;
//             }
//             
//             // AudioManager.Instance.Play("betSlider");
//             selectBet = true;
//             CancelInvoke(nameof(DelayDisable));
//             blocker.SetActive(true);
//             betButtonBlocker.SetActive(true);
//             maxButtonBlocker.SetActive(true);
//            
//             if (!maxSpeed)
//             {
//                 oldIndex = index;
//             }
//
//             IndexingCalculation(id);
//             
//             StartCoroutine(DisableBlocker());
//             UpdateItemStatus(id);
//             if (!maxSpeed)
//             {
//                 StartCoroutine(UpdateColor());
//                 colorfulOrchardUIManagerTesting.SetBetAmount(index);
//                 return;
//             }
//             betButtonText[(oldIndex - 6 + betList.Count) % betList.Count].color=Color.green;
//             
//         }
//         
//         private void IndexingCalculation(int id)
//         {
//             var betCount = betList.Count;
//             
//             switch (id)
//             {
//                 case 1:
//                     index = (index - 2 + betCount) % betCount;
//                     break;
//
//                 case 2:
//                     index = (index - 1 + betCount) % betCount;
//                     break;
//
//                 case 3:
//                     index = (index + 1) % betCount;
//                     break;
//
//                 case 4:
//                     index = (index + 2) % betCount;
//                     break;
//             }
//         }
//         
//         #endregion
//
//         
//         
//         #region Direction
//
//         private void UpdateItemStatus(int id)
//         {
//             _idToStatus.TryGetValue(id, out var status);
//             foreach (var item in betItems)
//             {
//                 item.SetTargetStatus(status);
//             }
//         }
//         
//         #endregion
//
//         #region Color and Blocker
//
//         
//         private IEnumerator UpdateColor()
//         {
//             betButtonText[CalculateIndexAccordingToList(oldIndex)].color=Color.green;
//             yield return new WaitForSeconds(0.2f);
//             betButtonText[CalculateIndexAccordingToList(index)].color=Color.white;
//         }
//         private IEnumerator UpdateColorDuringMax()
//         {
//             betButtonText[CalculateIndexAccordingToList(oldIndex)].color=Color.green;
//             betButtonText[CalculateIndexAccordingToList(index)].color=Color.white;
//             yield return new WaitForSeconds(0.3f); 
//             
//             if(!colorfulOrchardSpinTesting.ReturnSpinTimer() || colorfulOrchardSpinTesting.ReturnAutoSpinStatus())
//                 yield break;
//             maxButtonBlocker.SetActive(false);
//             blocker.SetActive(false);
//             betButtonBlocker.SetActive(false);
//         }
//
//         private IEnumerator DisableBlocker()
//         {
//             yield return new WaitUntil(() => targetReached);
//             yield return new WaitForSeconds(0.05f);
//             
//             if (maxSpeed) yield break;
//             if(!colorfulOrchardSpinTesting.ReturnSpinTimer() || colorfulOrchardSpinTesting.ReturnAutoSpinStatus())
//                 yield break;
//             blocker.SetActive(false);
//             
//             Invoke(nameof(DelayDisable),0.3f);
//         }
//
//
//         private void DelayDisable()
//         {
//             maxButtonBlocker.SetActive(false);
//             betButtonBlocker.SetActive(false);
//             selectBet = false;
//         }
//         #endregion
//
//
//         private int CalculateIndexAccordingToList(int indexState)
//         {
//             var listCount = betList.Count;
//             return (indexState - 6 + listCount) % listCount;
//         }
//
//
//         public void DisableBlockerAfterBonus()
//         {
//             blocker.SetActive(false);
//             maxButtonBlocker.SetActive(false);
//             betButtonBlocker.SetActive(false);
//             selectBet = false;
//         }
//     }
// }
