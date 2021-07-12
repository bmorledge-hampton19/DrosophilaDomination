using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCategoriesPanelManager : MonoBehaviour
{
    
    public GameObject jarsSubcategoriesPanel;
    public GameObject tasksSubcategoriesPanel;
    public GameObject miscSubcategoriesPanel;

    public Button currentButton;

    public GameObject currentSubcategoriesPanel;

    void update() {

    }

    void Awake() {

        currentSubcategoriesPanel = jarsSubcategoriesPanel;

        foreach(GameObject subcategoriesPanels in new List<GameObject> {jarsSubcategoriesPanel, tasksSubcategoriesPanel, miscSubcategoriesPanel}) {
            foreach(Button button in subcategoriesPanels.GetComponentsInChildren<Button>()) {
                button.onClick.AddListener(() => switchCurrentButton(button));
            }
        }

    }

    void Start() {
        reclickCurrentButton();
    }

    public void switchCurrentButton(Button button) {
        currentButton.interactable = true;
        button.interactable = false;
        currentButton = button;
    }

    public void reclickCurrentButton() {currentButton.onClick.Invoke();}

    private void onSubcategoriesDropdownClick(GameObject clickedSubcategoriesPanel) {
        if(currentSubcategoriesPanel == clickedSubcategoriesPanel) {
            currentSubcategoriesPanel.SetActive(!currentSubcategoriesPanel.activeSelf);
        } else {
            currentSubcategoriesPanel.SetActive(false);
            clickedSubcategoriesPanel.SetActive(true);
            currentSubcategoriesPanel = clickedSubcategoriesPanel;
        }
    }

    public void onJarsSubcatClick() {onSubcategoriesDropdownClick(jarsSubcategoriesPanel);}
    public void onTasksSubcatClick() {onSubcategoriesDropdownClick(tasksSubcategoriesPanel);}
    public void onMiscSubcatClick() {onSubcategoriesDropdownClick(miscSubcategoriesPanel);}

}
