using System.Collections.Generic;
using UnityEditor;

public class CustomUIHierarchy
{

    private List<string> toDisplay;
    private string toCheck;
    private List<int> acceptableConditions;
    private List<CustomUIHierarchy> nestedHierarchies;

    public CustomUIHierarchy() {
        toCheck = "TRUE";
        init();
    }
    public CustomUIHierarchy(string toCheck) {
        this.toCheck = toCheck;
        init();
    }

    private void init() {
        toDisplay = new List<string>();
        acceptableConditions = new List<int>();
        nestedHierarchies = new List<CustomUIHierarchy>();
    }

    public void addToDisplay(string serializableName) {
        toDisplay.Add(serializableName);
    }

    public void addAcceptableCondition(int acceptableCondition) {
        acceptableConditions.Add(acceptableCondition);
    }

    public void addNestedHierarchy(CustomUIHierarchy hierarchy) {
        nestedHierarchies.Add(hierarchy);
    }

    public void runHierarchy(SerializedProperty upgradeFunction) {

        SerializedProperty serializedToCheck = null;

        if (toCheck != "TRUE") 
            serializedToCheck = upgradeFunction.FindPropertyRelative(toCheck);

        if (serializedToCheck == null || acceptableConditions.Contains(serializedToCheck.enumValueIndex)) {

            EditorGUI.indentLevel += 1;

            foreach (string serializableName in toDisplay) 
                EditorGUILayout.PropertyField(upgradeFunction.FindPropertyRelative(serializableName));

            foreach (CustomUIHierarchy hierarchy in nestedHierarchies)
                hierarchy.runHierarchy(upgradeFunction);

            EditorGUI.indentLevel -= 1;

        }

    }

}
