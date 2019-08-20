﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject mainPanel;

    public TabManager tabManager;
    public GameObject jarPrefab;

    public AddFliesManager addFliesManager;
    public FlySelectorManager flySelectorManager;
    
    public DialoguePopup dialoguePopup;

    public Storage storage;


    public void createNewJar(){

        GameObject newJar = Instantiate(jarPrefab);
        JarManager newJarManager = newJar.GetComponent<JarManager>();
        
        newJarManager.mainPanel = mainPanel;

        newJarManager.storage = storage;

        newJarManager.jarUIManager = tabManager.createNewJarUI();
        newJarManager.instatiateUIConnections();

        newJarManager.addFliesManager = addFliesManager;
        newJarManager.flySelectorManager = flySelectorManager;

        newJarManager.dialoguePopup = dialoguePopup;

    }

    // Start is called before the first frame update
    void Start()
    {
        createNewJar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}