using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public GameObject[] Upgrades;
    public GameObject Player;

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
                    PerformUpgrade(script.name, script.numUpgrades);
                    Refresh();
                }
                return;
            }
        }
    }

    public void PerformUpgrade(string name, int tier) {
        switch(name) {
            case "Speed":
                // goal of speed upgrade is to : firstly, upgrade the torque.
                // final upgrade is rear wheel drive as this gives the biggest difference in speed
                print("Speed upgraded");
                SimpleCarController script = Player.GetComponent<SimpleCarController>();
                // if(tier == 1) {
                //     script.axleInfos[1].motor = true;
                // }
                break;
            default:
                break;
        }
    }
}
