using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{

    public GameObject jarsTab;
    public GameObject storageTab;
    public GameObject tasksTab;

    private List<GameObject> jarUIs;
    public GameObject jarUIPrefab;

    private GameObject focusTab;

    public JarUIManager createNewJarUI() {
        GameObject newJarUI = Instantiate(jarUIPrefab, jarsTab.GetComponent<RectTransform>());
        jarUIs.Add(newJarUI);
        return newJarUI.GetComponent<JarUIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jarUIs = new List<GameObject>();
        focusTab = jarsTab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toJarsTab() {changeTab(jarsTab);}
    public void toStorageTab() {changeTab(storageTab);}
    public void toTasksTab() {changeTab(tasksTab);}

    public void changeTab(GameObject newTab) {
        focusTab.SetActive(false);
        newTab.SetActive(true);
        focusTab = newTab;
    }

}
