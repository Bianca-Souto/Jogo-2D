using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Amounts")]
    public int totalWood;
    public int carrots;
    public float currentwater;
    public int fishes;
    public int totalStone;

    [Header("Limits")]
    public float waterLimit = 20;
    public float carrotsLimit = 5;
    public float woodLimit = 8;
    public float fishLimit = 7;
    public float stoneLimit = 8;

    public void WaterLimit(float water)
    {
        if(currentwater <= waterLimit)
        {
            currentwater += water;
        }
    }
}
