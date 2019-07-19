using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{

    public FlyReadoutManager flyReadoutManager;
    public FlyInteractor flyInteractor;

    public GameObject statTextPrefab;
    public GameObject statTextParent;

    private List<Fly> selectedFlies;
    public Text numSelectedText;
    public List<Text> totalStatTexts;
    public Text selectionConditionText;
    public Button finalizeSelection;
    private int minFlies;
    private int maxFlies;
    private List<FlyStats.StatID> statsToDisplay;
    public bool viewingSelected = false;

    public void updateMinMax(int min, int max) {
        minFlies = min;
        maxFlies = max;        
    }

    public void setStatsToDisplay(List<FlyStats.StatID> statsToDisplay) {

        this.statsToDisplay = statsToDisplay;

        if (statsToDisplay.Count == 0) statTextParent.SetActive(false);
        else statTextParent.SetActive(true);

        foreach (Text statText in totalStatTexts) statText.gameObject.SetActive(false);

        for (int i = 0; i < statsToDisplay.Count; i++) {
            
            if (totalStatTexts.Count == i) {
                GameObject newTotalStatText = Instantiate(statTextPrefab,statTextParent.GetComponent<Transform>());
                totalStatTexts.Add(newTotalStatText.GetComponent<Text>());
            }

            totalStatTexts[i].gameObject.SetActive(true); 
        
        }

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

        
            for (int i = 0; (statsToDisplay != null && i < statsToDisplay.Count); i++) {

                int statTotal = 0;
                foreach (Fly fly in selectedFlies) {
                    statTotal += fly.stats.getStat(statsToDisplay[i]);
                }

                if (statsToDisplay[i] != FlyStats.StatID.price) totalStatTexts[i].text = "Total " + statsToDisplay[i] + ": " + statTotal;
                else totalStatTexts[i].text = string.Format("Total Price: {0:C}",((float)statTotal/100));

            }

        if (selectedFlies.Count > maxFlies) {

            if (selectionConditionText != null) selectionConditionText.text = "Too many flies";
            finalizeSelection.interactable = false;

        } else if (selectedFlies.Count < minFlies) {

            if (selectionConditionText != null) selectionConditionText.text = "Too few flies";
            finalizeSelection.interactable = false;

        } else {

            if (selectionConditionText != null) selectionConditionText.text = "Ok";
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
