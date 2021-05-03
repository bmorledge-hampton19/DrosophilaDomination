using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCategoriesPanelManager : MonoBehaviour
{
    
    public GameObject jarsSubcategoriesPanel;
    public GameObject tasksSubcategoriesPanel;
    public GameObject miscSubcategoriesPanel;

    GameObject currentSubcategoriesPanel;

    void update() {

    }

    void Awake() {

        currentSubcategoriesPanel = jarsSubcategoriesPanel;
        focusJarsSubcategoriesPanel();

    }

    public void focusJarsSubcategoriesPanel() {
        currentSubcategoriesPanel.SetActive(false);
        jarsSubcategoriesPanel.SetActive(true);
        currentSubcategoriesPanel = jarsSubcategoriesPanel;
    }
    public void focusTasksSubcategoriesPanel() {
        currentSubcategoriesPanel.SetActive(false);
        tasksSubcategoriesPanel.SetActive(true);
        currentSubcategoriesPanel = tasksSubcategoriesPanel;
    }
    public void focusMiscSubcategoriesPanel() {
        currentSubcategoriesPanel.SetActive(false);
        miscSubcategoriesPanel.SetActive(true);
        currentSubcategoriesPanel = miscSubcategoriesPanel;
    }

}
