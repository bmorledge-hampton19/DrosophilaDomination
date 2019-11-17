using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Upgrades/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
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

    public string upgradeName;
    [TextArea(1,3)]
    public string description;
    
    public UpgradeCategory upgradeCategory;
    public List<Player.PlayerResource> resourceCostTypes;
    public List<float> resourceCostAmounts;
    private Dictionary<Player.PlayerResource,float> resourceCosts;
    public Dictionary<Player.PlayerResource, float> getResourceCosts() => resourceCosts;
    public TraitDB.GamePhase gamePhase;

    public void buy(){
        executeOnBuy.Invoke();
        }

    public UnlockCondition unlockCondition = new UnlockCondition();

    private UnityEvent executeOnBuy;

    [HideInInspector] public List<UpgradeFunction> upgradeFunctions;

    public void addAction(UnityAction action) {
        executeOnBuy.AddListener(action);
    }

    void OnEnable() {

        resourceCosts = new Dictionary<Player.PlayerResource, float>();
        if (resourceCostTypes != null) {
            for (int i = 0; i < resourceCostTypes.Count; i++) {
                resourceCosts.Add(resourceCostTypes[i],resourceCostAmounts[i]);
            }
        }

    }

}