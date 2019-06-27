using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarManager : MonoBehaviour {

	public Storage storage;
	public TraitDB traitDB;
	private Jar jar;
	private int ID;
	public static int jarNum = 0;
	public void SetID(int ID){this.ID = ID;}
	
	public ProgressBar progressBar;
	public JarActionButton jarActionButton;
	public FlySelectorManager flySelectorManager;
	public GameObject mainPanel;

	// Use this for initialization
	void Start () {
		
		jar = new Jar(traitDB);

		progressBar.fillActions += breedFlies;

		jarActionButton.pressActions += jarAction;

		jarNum++;
		ID = jarNum;

	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnDisable() {
		progressBar.fillActions-=breedFlies;
		jarActionButton.pressActions -= jarAction;
	}

	public void breedFlies(){
		jar.generateProgeny();
	}

	private void jarAction(JarActionButton.ButtonState buttonState){
		switch ((int)buttonState){
            case 1:
                emptyJar();
                break;
            case 2:
                selectNewParents();
                break;
            case 3:
                beginBreeding();
                break;
        }
	}

	public void emptyJar(){
		//TODO: Add additional dialogue if flies are not finished breeding.
		storage.addFlies(jar.emptyJar());
		jarActionButton.advanceState();
	}
	public void selectNewParents(){
		flySelectorManager.setUpSelector(("Jar " + ID), 2, 8);
		mainPanel.SetActive(false);
		flySelectorManager.sendFlies += addNewParents;
	}
	public void beginBreeding(){
		progressBar.activate(.05f);
		jarActionButton.advanceState();
	}

	private void addNewParents(List<Fly> newParents) {
		mainPanel.SetActive(true);
		if (newParents.Count != 0) {
			jar.addParents(newParents);
			jarActionButton.advanceState();
		}
		flySelectorManager.sendFlies -= addNewParents;
	}

}
