using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{

    public GameObject jarsTab;
    public GameObject storageTab;
    public GameObject tasksTab;

    private List<GameObject> jarUIs;
    public List<GameObject> jarPlaceholders;
    public GameObject jarUIPrefab;

    private GameObject focusTab;

    public JarUIManager createNewJarUI() {
        Destroy(jarPlaceholders[0]);
        jarPlaceholders.RemoveAt(0);
        GameObject newJarUI = Instantiate(jarUIPrefab, jarsTab.GetComponent<RectTransform>());
        newJarUI.GetComponent<RectTransform>().SetSiblingIndex(2-jarPlaceholders.Count);
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
