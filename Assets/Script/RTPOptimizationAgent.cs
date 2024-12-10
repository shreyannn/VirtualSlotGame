using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class RTPOptimizationAgent : Agent
{
    
    private float[] symbolProbabilities = new float[13]; // Initial probabilities
    private float currentRTP;
    
    private int totalSpins = 0; // Track total spins across episodes
    private const int maxSpins = 10000; // Maximum spins before ending the episode

    
    public override void Initialize()
    {
        // Initialize symbol probabilities
        for (int i = 0; i < symbolProbabilities.Length; i++)
        {
            symbolProbabilities[i] = 1f / symbolProbabilities.Length; // Equal distribution
        }
        // GameManager.Instance.SetSymbolProbabilities(symbolProbabilities);
    }
    
    
   public override void OnActionReceived(ActionBuffers actions)
   {
       // Update symbol probabilities based on actions
       for (int i = 0; i < symbolProbabilities.Length; i++)
       {
           symbolProbabilities[i] += actions.ContinuousActions[i];
           symbolProbabilities[i] = Mathf.Clamp01(symbolProbabilities[i]); // Keep probabilities in [0,1]
       }
       
       // GameManager.Instance.SetSymbolProbabilities(symbolProbabilities);
       // currentRTP = GameManager.Instance.SimulateSpin();  //simulate
       
       
       // Calculate reward
       float reward = CalculateReward(currentRTP);
       SetReward(reward);

       
       // End episode if target RTP is achieved
       if (Mathf.Abs(currentRTP - 0.95f) < 0.01f || totalSpins >= maxSpins) // Target RTP = 95%
       {
           EndEpisode();
       }
       
       
       // Debug.Log(actions.DiscreteActions[0]);
     // Debug.Log(actions.ContinuousActions[0]);
   }

   public override void CollectObservations(VectorSensor sensor)
   {
       sensor.AddObservation(currentRTP);
       foreach (float prob in symbolProbabilities)
       {
           sensor.AddObservation(prob);
       }
   }
   
   
   private float CalculateReward(float rtp)
   {
       float targetRTP = 0.95f; // Target RTP = 95%
       return 1f - Mathf.Abs(rtp - targetRTP); // Higher reward for closer RTP
   }
   
   
   public override void Heuristic(in ActionBuffers actionsOut)
   {
       // For manual testing: set random adjustments
       for (int i = 0; i < actionsOut.ContinuousActions.Length; i++)
       {
           // actionsOut.ContinuousActions[i] = Random.Range(-0.01f, 0.01f); // Small random adjustments
       }
   }
}
