using UnityEditor;

[CustomEditor(typeof(Upgrade))]
public class UpgradeEditor : Editor {

    bool expanded = false;
    Upgrade upgrade;
    SerializedProperty upgradeFunctions;
    CustomUIHierarchy hierarchy;

    void OnEnable() {

        upgrade = target as Upgrade;
        upgradeFunctions = serializedObject.FindProperty("upgradeFunctions");

        initHierarchy();

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
                    upgrade.upgradeFunctions.Add(new UpgradeFunction());
                }
                while(listSize < upgrade.upgradeFunctions.Count){
                    upgrade.upgradeFunctions.RemoveAt(upgrade.upgradeFunctions.Count - 1);
                }
            }

            for(int i = 0; i < upgradeFunctions.arraySize; i++){

                EditorGUILayout.LabelField("Upgrade Function " + (i+1) + ":");
                EditorGUI.indentLevel += 1;

                hierarchy.runHierarchy(upgradeFunctions.GetArrayElementAtIndex(i));

            }

            serializedObject.ApplyModifiedProperties();

        }
        
    }
    

    private void initHierarchy() {

        hierarchy = new CustomUIHierarchy();
        hierarchy.addToDisplay("functionType");

            CustomUIHierarchy unlockTypeHierarchy = new CustomUIHierarchy("functionType");
            unlockTypeHierarchy.addAcceptableCondition((int)FunctionType.unlock);
            unlockTypeHierarchy.addToDisplay("unlockType");
            hierarchy.addNestedHierarchy(unlockTypeHierarchy);

                CustomUIHierarchy jarPropertyHierarchy = new CustomUIHierarchy("unlockType");
                jarPropertyHierarchy.addAcceptableCondition((int)UnlockType.jarProperty);
                jarPropertyHierarchy.addToDisplay("jarProperty");
                unlockTypeHierarchy.addNestedHierarchy(jarPropertyHierarchy);

                CustomUIHierarchy taskTypeHierarchy = new CustomUIHierarchy("unlockType");
                taskTypeHierarchy.addAcceptableCondition((int)UnlockType.task);
                taskTypeHierarchy.addToDisplay("taskType");
                unlockTypeHierarchy.addNestedHierarchy(taskTypeHierarchy);

            CustomUIHierarchy increaseTypeHierarchy = new CustomUIHierarchy("functionType");
            increaseTypeHierarchy.addAcceptableCondition((int)FunctionType.increase);
            increaseTypeHierarchy.addToDisplay("increaseFunction");
            increaseTypeHierarchy.addToDisplay("increaseType");
            increaseTypeHierarchy.addToDisplay("increaseValue");
            hierarchy.addNestedHierarchy(increaseTypeHierarchy);

    }

}