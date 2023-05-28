using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] public string name;
    [SerializeField] public int numUpgrades;
    [SerializeField] public float cost;

    public void addToCost(float x) {
        this.cost += x;
    }
}
