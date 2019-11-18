using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeFunctionalizer : MonoBehaviour
{
    
    public GameManager gameManager;
    public TaskManager taskManager;
    public PropertyDB propertyDB;
    public GrantWriterManager grantWriterManager;

    public void functionalizeUpgrades(List<Upgrade> upgrades) {

        foreach(Upgrade upgrade in upgrades) {

            foreach(UpgradeFunction function in upgrade.upgradeFunctions) {

                UnityAction actionToAdd;

                if (function.functionType == FunctionType.unlock)
                    actionToAdd = getUnlockAction(function);
                else if (function.functionType == FunctionType.increase) 
                    actionToAdd = getIncreaseAction(function);
                else actionToAdd = null;
                    
                if (actionToAdd == null) print("ERROR:  Function not assigned");
                else upgrade.addAction(actionToAdd);

            }

        }

    }

    private UnityAction getUnlockAction(UpgradeFunction function) {

        switch (function.unlockType) {

            case UnlockType.jar:
                return gameManager.createNewJar;

            case UnlockType.jarProperty:
                return delegate() {propertyDB.discoverObject(function.jarProperty);};

            case UnlockType.task:
                return getTaskUnlock(function);

            default:
                return null;

        }

    }

    private UnityAction getIncreaseAction(UpgradeFunction function) {

        switch (function.increaseType) {

            case IncreaseType.grantPayout:
                return delegate() {grantWriterManager.increasePayout(function.increaseValue,function.increaseFunction);};
            default:
                return null;
        }
        

    }

    private UnityAction getTaskUnlock(UpgradeFunction function) {

        switch (function.taskType) {

            case TaskType.blackMarket:
                return taskManager.unlockBlackMarket;

            case TaskType.requests:
                return taskManager.unlockRequests;

            case TaskType.collosseum:
                return taskManager.unlockCollosseum;

            default:
                return null;

        }

    }

}
