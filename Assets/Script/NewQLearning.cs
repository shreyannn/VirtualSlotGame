using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewQLearning : MonoBehaviour
{
    private int nSymbols = 10; // Number of symbols (A-F)
    private float[,] qTable; // Q-table
    private float[] frequencies; // Symbol frequencies
    private float learningRate = 0.1f;
    private float discountFactor = 0.9f;
    private float epsilon = 1.0f; // Exploration rate
    private float epsilonDecay = 0.99f;
    private float minEpsilon = 0.1f;
    private float targetRTP = 1.5f;

   public void StartNewQLearning()
    {
        InitializeQTable();
        InitializeFrequencies();
        StartCoroutine(TrainQAgent(1000)); // Train for 1000 episodes
    }


    public void KeepTraining()
    {
        StartCoroutine(TrainQAgent(1000)); // Train for 1000 episodes
    }

    private void InitializeQTable()
    {
        qTable = new float[nSymbols, nSymbols]; // Q-table with zeros
    }

    private void InitializeFrequencies()
    {
        frequencies = new float[nSymbols];
        for (int i = 0; i < nSymbols; i++)
        {
            frequencies[i] = 1.0f / nSymbols; // Equal initial probabilities
        }
    }

    private float CalculateRTP(float[] currentFrequencies)
    {
        // Placeholder for actual RTP calculation
        // Replace with your slot game's RTP logic
        GameManager.Instance.SetSymbolFrequencies(currentFrequencies);
        float rtp = 0f;
        for (int i = 0; i < 1000; i++)
        {
            rtp += GameManager.Instance.SimulateSpinNew();
        }
        // Debug.Log("rtp/numofSpin: "+rtp/1000);
        return rtp/1000;
    }

    private float CalculateReward(float currentRTP)
    {
        return -Mathf.Abs(targetRTP - currentRTP); // Closer to target RTP = higher reward
    }

    private float[] UpdateFrequencies(int action, float step = 0.05f)
    {
        float[] newFrequencies = (float[])frequencies.Clone();
        newFrequencies[action] += step;
        for (int i = 0; i < newFrequencies.Length; i++)
        {
            newFrequencies[i] = Mathf.Clamp(newFrequencies[i], 0.01f, 1.0f);
        }
        NormalizeFrequencies(newFrequencies);
        return newFrequencies;
    }

    private void NormalizeFrequencies(float[] frequencies)
    {
        float sum = 0f;
        foreach (float freq in frequencies)
        {
            sum += freq;
        }

        for (int i = 0; i < frequencies.Length; i++)
        {
            frequencies[i] /= sum;
        }
    }

    private IEnumerator TrainQAgent(int episodes)
    {
        for (int episode = 0; episode < episodes; episode++)
        {
            // Choose an action (epsilon-greedy)
            int action;
            if (UnityEngine.Random.value < epsilon)
            {
                action = UnityEngine.Random.Range(0, nSymbols); // Explore
            }
            else
            {
                action = GetBestAction(); // Exploit
            }

            // Update frequencies
            float[] newFrequencies = UpdateFrequencies(action);

            // Calculate RTP and reward
            float currentRTP = CalculateRTP(newFrequencies);
            float reward = CalculateReward(currentRTP);

            // Update Q-table
            float oldValue = qTable[0, action];
            float bestFutureValue = GetMaxQValue(0);
            qTable[0, action] = oldValue + learningRate * (reward + discountFactor * bestFutureValue - oldValue);

            // Update state
            frequencies = newFrequencies;

            // Decay epsilon
            epsilon = Mathf.Max(minEpsilon, epsilon * epsilonDecay);

            // Log progress
            if (episode % 100 == 0)
            {
                Debug.Log($"Episode {episode}: RTP={currentRTP:F3}, Frequencies={string.Join(", ", frequencies)}");
            }

            yield return null;
        }

        Debug.Log("Training complete!");
    }

    private int GetBestAction()
    {
        int bestAction = 0;
        float bestValue = float.MinValue;
        for (int i = 0; i < nSymbols; i++)
        {
            if (qTable[0, i] > bestValue)
            {
                bestValue = qTable[0, i];
                bestAction = i;
            }
        }
        return bestAction;
    }

    private float GetMaxQValue(int state)
    {
        float maxValue = float.MinValue;
        for (int i = 0; i < nSymbols; i++)
        {
            if (qTable[state, i] > maxValue)
            {
                maxValue = qTable[state, i];
            }
        }
        return maxValue;
    }
}
