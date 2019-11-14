using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Upgrades/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    
    public enum UpgradeCategory {

        jars,
        jarMaterial,
        feedstock,
        jarFurnishing,
        nutrient,
        task,
        blackMarket,
        colosseum

    }

    public string upgradeName;
    [TextArea(1,3)]
    public string description;
    
    public UpgradeCategory upgradeCategory;
    public List<Player.PlayerResource> resourceCostTypes;
    public List<float> resourceCostAmounts;
    private Dictionary<Player.PlayerResource,float> resourceCost;
    public TraitDB.GamePhase gamePhase;

    private bool bought = false;
    public bool isBought(){return bought;}
    public void buy(){
        bought = true;
        executeOnBuy.Invoke();
        }

    public UnlockCondition unlockCondition = new UnlockCondition();

    private UnityEvent executeOnBuy;

    [HideInInspector] public List<UpgradeFunction> upgradeFunctions;

    public void addAction(UnityAction action) {
        executeOnBuy.AddListener(action);
    }

    void OnEnable() {

        resourceCost = new Dictionary<Player.PlayerResource, float>();
        if (resourceCostTypes != null) {
            for (int i = 0; i < resourceCostTypes.Count; i++) {
                resourceCost.Add(resourceCostTypes[i],resourceCostAmounts[i]);
            }
        }

    }

}

[CustomEditor(typeof(Upgrade))]
public class UpgradeEditor : Editor {

    bool expanded = false;
    Upgrade upgrade;
    SerializedProperty upgradeFunctions;

    void OnEnable() {

        upgrade = target as Upgrade;
        upgradeFunctions = serializedObject.FindProperty("upgradeFunctions");

    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        expanded = EditorGUILayout.Foldout(expanded,"Upgrade Functions",true);
        EditorGUI.indentLevel += 1;

        if (expanded) {

            int listSize = upgrade.upgradeFunctions.Count;
            listSize = EditorGUILayout.IntField ("Size", listSize);
            if (listSize < 0) listSize = 0;
   
            if(listSize != upgradeFunctions.arraySize){
                while(listSize > upgrade.upgradeFunctions.Count){
                    //upgradeFunctions.InsertArrayElementAtIndex(upgradeFunctions.arraySize);
                    upgrade.upgradeFunctions.Add(new UpgradeFunction());
                }
                while(listSize < upgrade.upgradeFunctions.Count){
                    //upgradeFunctions.DeleteArrayElementAtIndex(upgradeFunctions.arraySize - 1);
                    upgrade.upgradeFunctions.RemoveAt(upgrade.upgradeFunctions.Count - 1);
                }
            }

            for(int i = 0; i < upgradeFunctions.arraySize; i++){

                EditorGUILayout.LabelField("Upgrade Function " + (i+1) + ":");
                EditorGUI.indentLevel += 1;

                SerializedProperty upgradeFunction = upgradeFunctions.GetArrayElementAtIndex(i);

                SerializedProperty functionType = upgradeFunction.FindPropertyRelative("functionType");
                SerializedProperty unlockType = upgradeFunction.FindPropertyRelative("unlockType");
                SerializedProperty taskType = upgradeFunction.FindPropertyRelative("taskType");
                
                EditorGUILayout.PropertyField(functionType);

                if (functionType.enumValueIndex == (int)FunctionType.unlock)
                {
                    EditorGUILayout.PropertyField(unlockType);
                }
                
                if (unlockType.enumValueIndex == (int)UnlockType.task)
                {
                    EditorGUILayout.PropertyField(taskType);
                }

                EditorGUI.indentLevel -= 1;

            }

            serializedObject.ApplyModifiedProperties();

        }
        
    }
    

}