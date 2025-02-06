using System.Collections.Generic;
using UnityEngine;

public class ListPractice : MonoBehaviour
{
    List<string> fruits = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fruits.Add("Banana");
        fruits.Add("Apple");
        fruits.Add("Cherry");

        foreach (string fruit in fruits)
        {
            Debug.Log(fruit);
        }

        fruits.Remove("Banana");

        Debug.Log($"fruits left: {fruits.Count}, the banan is gone!");
    }


}
