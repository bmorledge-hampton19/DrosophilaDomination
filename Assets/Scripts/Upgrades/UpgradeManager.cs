using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public class ResourceReadout {
    public PlayerResource resource;
    public Text readout;
}

public class UpgradeManager : MonoBehaviour
{
 
    public Player player;

    public UpgradeFunctionalizer functionalizer;

    public UpgradeDB upgradeDB;

    public UpgradeCategoriesPanelManager upgradeCategoriesPanelManager;

    public GameObject upgradeReadoutPrefab;
    public Transform upgradeReadoutViewer;
    private Dictionary<Upgrade, GameObject> upgradeReadouts;
    private List<GameObject> activeReadouts = new List<GameObject>();

    
 
    public List<ResourceReadout> resourceReadouts;
    private Dictionary<PlayerResource, Text> resourceReadoutsDict = new Dictionary<PlayerResource, Text>();

    public Toggle hidePurchasedUpgrades;

    void Awake() {
        foreach(ResourceReadout resourceReadout in resourceReadouts) {
            resourceReadoutsDict[resourceReadout.resource] = resourceReadout.readout;
        }
    }

    void Start() {

        functionalizer.functionalizeUpgrades(upgradeDB.getCurrentObjectTier());
        initializeUpgradeReadouts();
        hidePurchasedUpgrades.onValueChanged.AddListener((_) => upgradeCategoriesPanelManager.reclickCurrentButton());

        player.playerResourceChanged += updateResourceReadout;
        foreach(PlayerResource playerResource in resourceReadoutsDict.Keys) updateResourceReadout(playerResource);

    }

    private void initializeUpgradeReadouts() {

        upgradeReadouts = new Dictionary<Upgrade, GameObject>();

        foreach(Upgrade upgrade in upgradeDB.getCurrentObjectTier()) {

            GameObject newReadout = Instantiate(upgradeReadoutPrefab,upgradeReadoutViewer);
            newReadout.GetComponent<UpgradeReadout>().initialize(upgrade, this);
            upgradeReadouts[upgrade] = newReadout;
            newReadout.SetActive(false);

        }

    }

    public bool purchaseUpgrade(Upgrade upgrade) {

        foreach(PlayerResource resourceCost in upgrade.resourceCosts.Keys) {

            if(player.getResource(resourceCost) < upgrade.resourceCosts[resourceCost])
                return false;

        }

        foreach(PlayerResource resourceCost in upgrade.resourceCosts.Keys) {

            player.removeResource(resourceCost, upgrade.resourceCosts[resourceCost]);

        }

        upgrade.buy();
        return true;

    }

    private void updateResourceReadout(PlayerResource playerResource) {
        if(playerResource == PlayerResource.money) {
            resourceReadoutsDict[playerResource].text = player.getResource(playerResource).ToString("C2");
        } else {
            resourceReadoutsDict[playerResource].text = EnumHelper.GetDescription(playerResource) + ": " + player.getResource(playerResource);
        }
    }

    private void disableActiveUpgradeReadouts() {
        foreach(GameObject upgradeReadout in activeReadouts) {
            upgradeReadout.SetActive(false);
        }
        activeReadouts.Clear();
    }

    private void showUpgrades(List<UpgradeCategory> upgradeCategories) {

        disableActiveUpgradeReadouts();

        foreach(UpgradeCategory upgradeCategory in upgradeCategories) {
            foreach(Upgrade upgrade in upgradeDB.getUpgradesByCategory(upgradeCategory, true)) {
                if(!upgrade.isPurchased() || !hidePurchasedUpgrades.isOn) {
                    GameObject upgradeReadout = upgradeReadouts[upgrade];
                    upgradeReadout.SetActive(true);
                    activeReadouts.Add(upgradeReadout);
                }
            }
        }

    }

    public void showJarsGeneralUpgrades(){
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.jarsGeneral});
    }

    public void showJarsMaterialUpgrades(){
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.jarMaterial});
    }

    public void showFeedstockUpgrades(){
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.feedstock});
    }

    public void showJarFurnishingUpgrades(){
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.jarFurnishing});
    }

    public void showNutrientUpgrades(){
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.nutrient});
    }

    public void showAllJarUpgrades() {
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.jarsGeneral,
                                                UpgradeCategory.jarMaterial,
                                                UpgradeCategory.feedstock,
                                                UpgradeCategory.jarFurnishing,
                                                UpgradeCategory.nutrient});
    }

    public void showTasksGeneralUpgrades() {
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.tasksGeneral});
    }

    public void showGrantWritingUpgrades() {
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.grantWriting});
    }

    public void showBlackMarketUpgrades() {
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.blackMarket});
    }

    public void showRequestsUpgrades() {
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.requests});
    }

    public void showColosseumUpgrades() {
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.colosseum});
    }

    public void showAllTaksUpgrades() {
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.tasksGeneral,
                                                UpgradeCategory.grantWriting,
                                                UpgradeCategory.blackMarket,
                                                UpgradeCategory.requests,
                                                UpgradeCategory.colosseum});
    }

    public void showQualityOfLifeUpgrades() {
        showUpgrades(new List<UpgradeCategory> {UpgradeCategory.qualityOfLife});
    }

}
