using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public GameObject[] Upgrades;

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
            GameManager man = FindObjectOfType<GameManager>();
            if(script.name == name) {
                // item found, try to buy. if not enough currency, return
                print(man.currency + "  " + script.cost);
                if(man.currency >= script.cost && script.numUpgrades < script.maxUpgrades) {
                    print("bought");
                    man.currency -= script.cost;
                    script.cost += script.cost;
                    script.numUpgrades++;
                    //PerformUpgrade(script.name, script.numUpgrades);
                    Refresh();
                }
                return;
            }
        }
    }

    public void PerformUpgrade(string name, int tier, GameObject player) {
        switch(name) {
            case "Speed":
                // goal of speed upgrade is to : firstly, upgrade the torque.
                // final upgrade is rear wheel drive as this gives the biggest difference in speed
                SimpleCarController script = player.GetComponent<SimpleCarController>();
                if(tier >= 1) {
                    print("tier 1 speed");
                    script.maxMotorTorque = 9000;
                }
                
                if(tier >= 2) {
                    print("tier 2 speed");
                    script.axleInfos[1].motor = true;
                }
                break;
            default:
                break;
        }
    }

    public void PerformUpgrades(GameObject player) {
        for(int i = 0; i < Upgrades.Length; ++i) {
            ShopItem script = Upgrades[i].GetComponent<ShopItem>();
            if(script.numUpgrades > 0)
                PerformUpgrade(script.name, script.numUpgrades, player);
        }
    }
}
