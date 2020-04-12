using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeManager : MonoBehaviour
{
 
    public Player player;

    public UpgradeFunctionalizer functionalizer;

    public List<Upgrade> upgrades;

    private List<Upgrade> purchasedUpgrades;

    void start() {

        purchasedUpgrades = new List<Upgrade>();
        functionalizer.functionalizeUpgrades(upgrades);

    }

    public List<Upgrade> getUpgradeCategory(Upgrade.UpgradeCategory category) {

        List<Upgrade> returnTheseUpgrades = new List<Upgrade>();

        foreach (Upgrade upgrade in upgrades) {
            if (upgrade.upgradeCategory == category) {
                returnTheseUpgrades.Add(upgrade);
            }
        }

        return returnTheseUpgrades;

    }

    public bool purchaseUpgrade(Upgrade upgrade) {

        foreach(Player.PlayerResource resourceCost in upgrade.resourceCosts.Keys) {

            if(player.getResource(resourceCost) < upgrade.resourceCosts[resourceCost])
                return false;

        }

        foreach(Player.PlayerResource resourceCost in upgrade.resourceCosts.Keys) {

            player.removeResource(resourceCost, upgrade.resourceCosts[resourceCost]);

        }

        upgrade.buy();
        purchasedUpgrades.Add(upgrade);
        return true;

    }

    public bool isPurchased(Upgrade upgrade) => purchasedUpgrades.Contains(upgrade);

    

}
