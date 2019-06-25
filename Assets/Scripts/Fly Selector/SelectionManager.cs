using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{

    public FlyReadoutManager flyReadoutManager;

    private List<Fly> selectedFlies;
    public Text numSelectedText;
    public Text selectionConditionText;
    public Button finalizeSelection;
    private int minFlies;
    private int maxFlies;

    public void updateMinMax(int min, int max) {
        minFlies = min;
        maxFlies = max;        
    }

    public void clearSelectedFlies() {
        selectedFlies.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {

        selectedFlies = new List<Fly>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateSelectedFlies(bool selected){

        FlyReadout flyReadout = EventSystem.current.currentSelectedGameObject.GetComponent<FlyReadout>();
        if (selected) {
            selectedFlies.Add(flyReadout.fly);
        } else {
            selectedFlies.Remove(flyReadout.fly);
        }

        updateSelectionText();

    }

    public void selectAll() {
        foreach(FlyReadout flyReadout in flyReadoutManager.getActiveReadouts()) {
            if (!flyReadout.isSelected()) {
                flyReadout.activateToggle();
                //selectedFlies.Add(flyReadout.fly);
            }
        }
        updateSelectionText();
    }

    public void deselectAll() {
        foreach(FlyReadout flyReadout in flyReadoutManager.getActiveReadouts()) {
            if (flyReadout.isSelected()) {
                flyReadout.deactivateToggle();
                //selectedFlies.Remove(flyReadout.fly);
            }
        }
        updateSelectionText();
    }

    private void updateSelectionText(){

        numSelectedText.text = ("Selected: " + selectedFlies.Count);
        if (selectedFlies.Count > maxFlies) {

            selectionConditionText.text = "Too many flies";
            finalizeSelection.interactable = false;

        } else if (selectedFlies.Count < minFlies) {

            selectionConditionText.text = "Too few flies";
            finalizeSelection.interactable = false;

        } else {

            selectionConditionText.text = "Ok";
            finalizeSelection.interactable = true;

        }

    }

    public void viewSelected() {flyReadoutManager.updateReadout(selectedFlies);}

    public List<Fly> getSelectedFlies()=>selectedFlies;

}
