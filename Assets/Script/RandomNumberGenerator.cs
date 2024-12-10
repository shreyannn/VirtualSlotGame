//
// using UnityEngine;
//
// public class RandomNumberGenerator : MonoBehaviour
// {
//     void Start()
//     {
//         // Initialize with a seed (e.g., based on current time)
//         long seed = DateTime.Now.Ticks;
//         LinearCongruentialGenerator rng = new LinearCongruentialGenerator(seed);
//
//         // Generate random numbers
//         Debug.Log($"Random Float: {rng.NextFloat()}");
//         Debug.Log($"Random Int (1-10): {rng.NextInt(1, 10)}");
//     }
//     
//     
//     
// }
//
// public class LinearCongruentialGenerator
// {
//     private long seed;
//     private const long a = 1664525;  // Multiplier (common choice)
//     private const long c = 1013904223; // Increment (common choice)
//     private const long m = (1L << 32); // Modulus (2^32)
//
//     public LinearCongruentialGenerator(long seed)
//     {
//         this.seed = seed;
//     }
//
//     // Generate the next random number in the range [0, 1)
//     public float NextFloat()
//     {
//         seed = (a * seed + c) % m;
//         return (float)seed / m;
//     }
//
//     // Generate the next random integer in the range [min, max)
//     public int NextInt(int min, int max)
//     {
//         if (min >= max) throw new ArgumentException("min must be less than max");
//         return min + (int)(NextFloat() * (max - min));
//     }
// }
//
//
//