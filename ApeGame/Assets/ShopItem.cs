using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] public string name;
    [SerializeField] public float maxUpgrades;
    [SerializeField] public float cost;
    public int numUpgrades = 0;

    public void addToCost(float x) {
        this.cost += x;
    }
}
