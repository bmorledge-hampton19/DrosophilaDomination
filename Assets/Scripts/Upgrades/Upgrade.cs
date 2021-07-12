using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

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
        colosseum,
        qualityOfLife

    }

[CreateAssetMenu(fileName = "NewTrait", menuName = "Upgrades/Upgrade", order = 1)]
public class Upgrade : DataObject
{

    [TextArea(1,3)]
    public string description;
    
    public UpgradeCategory upgradeCategory;
    public List<PlayerResource> resourceCostTypes;
    public List<float> resourceCostAmounts;
    public Dictionary<PlayerResource,float> resourceCosts;

    public UnlockCondition unlockCondition;

    private bool purchased = false;
    public bool isPurchased() {return purchased;}
    private UnityEvent executeOnBuy;
    public void buy(){
        executeOnBuy.Invoke();
        purchased = true;
    }    

    [HideInInspector] public List<UpgradeFunction> upgradeFunctions;

    public void addAction(UnityAction action) {
        executeOnBuy.AddListener(action);
    }

    void OnEnable() {

        executeOnBuy = new UnityEvent();

        resourceCosts = new Dictionary<PlayerResource, float>();
        if (resourceCostTypes != null) {
            for (int i = 0; i < resourceCostTypes.Count; i++) {
                resourceCosts.Add(resourceCostTypes[i],resourceCostAmounts[i]);
            }
        }

    }

}