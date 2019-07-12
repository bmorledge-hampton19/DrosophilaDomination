using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{

    public FlyReadoutManager flyReadoutManager;
    public FlyInteractor flyInteractor;

    private List<Fly> selectedFlies;
    public Text numSelectedText;
    public Text selectionConditionText;
    public Button finalizeSelection;
    private int minFlies;
    private int maxFlies;
    public bool viewingSelected = false;

    public void updateMinMax(int min, int max) {
        minFlies = min;
        maxFlies = max;        
    }

    public void clearSelectedFlies() {
        selectedFlies.Clear();
        viewingSelected = false;
        updateSelectionText();
    }

    // Start is called before the first frame update
    void Awake()
    {

        selectedFlies = new List<Fly>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateSelectedFlies(bool selected, FlyReadout flyReadout){

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
            }
        }
        updateSelectionText();
    }

    public void deselectAll() {
        foreach(FlyReadout flyReadout in flyReadoutManager.getActiveReadouts()) {
            if (flyReadout.isSelected()) {
                flyReadout.deactivateToggle();
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

    public void viewSelected() {

        if (!viewingSelected) {
            flyReadoutManager.updateReadout(selectedFlies);
            viewingSelected = true;
        } else {
            flyInteractor.updateFliesInView();
            viewingSelected = false;
        }

    }

    public List<Fly> getSelectedFlies()=>selectedFlies;

}
