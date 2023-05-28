using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public GameObject[] Upgrades;
    public GameObject GameMan;

    // Update is called once per frame
    public void Refresh() {
        for(int i = 0; i < Upgrades.Length; ++i) {
            TMPro.TextMeshProUGUI textMeshPro = Upgrades[i].GetComponent<TMPro.TextMeshProUGUI>();
            ShopItem script = Upgrades[i].GetComponent<ShopItem>();
            textMeshPro.text = script.name + "\n" + script.cost.ToString("F2") + " Bananas";
        }
    }

    public void Buy(string name) {
        for(int i = 0; i < Upgrades.Length; ++i) {
            ShopItem script = Upgrades[i].GetComponent<ShopItem>();
            if(script.name == name) {
                // item found, try to buy. if not enough currency, return
                print(GameManager.currency + "  " + script.cost);
                if(GameManager.currency >= script.cost && script.numUpgrades >= 1) {
                    print("bought");
                    GameManager.currency -= script.cost;
                    script.cost += script.cost;
                }
                return;
            }
        }
    }
}
