using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnlockCondition
{
    
    public List<Upgrade> specificUpgrades;

    public List<Upgrade.UpgradeCategory> generalSumType;
    public List<int> generalSumCount;

    public bool isConditionSatisfied(UpgradeManager manager) {
        
        
        
        return false;
    }

}
