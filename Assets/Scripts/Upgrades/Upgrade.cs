using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Upgrades/Upgrade", order = 1)]
public class Upgrade : DataObject
{
    
    public enum UpgradeCategory {

        jarsGeneral,
        jarMaterial,
        feedstock,
        jarFurnishing,
        nutrient,
        tasksGeneral,
        grantWriting,
        blackMarket,
        requests,
        colosseum

    }

    [TextArea(1,3)]
    public string description;
    
    public UpgradeCategory upgradeCategory;
    public List<Player.PlayerResource> resourceCostTypes;
    public List<float> resourceCostAmounts;
    public Dictionary<Player.PlayerResource,float> resourceCosts;

    public void buy(){
        executeOnBuy.Invoke();
        }

    public UnlockCondition unlockCondition;

    private UnityEvent executeOnBuy;

    [HideInInspector] public List<UpgradeFunction> upgradeFunctions;

    public void addAction(UnityAction action) {
        executeOnBuy.AddListener(action);
    }

    void OnEnable() {

        executeOnBuy = new UnityEvent();

        resourceCosts = new Dictionary<Player.PlayerResource, float>();
        if (resourceCostTypes != null) {
            for (int i = 0; i < resourceCostTypes.Count; i++) {
                resourceCosts.Add(resourceCostTypes[i],resourceCostAmounts[i]);
            }
        }

    }

}