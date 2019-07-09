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
	public DialoguePopup dialoguePopup;

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

	public void breedFlies(){
		jar.generateProgeny();
	}

	private void jarAction(JarActionButton.ButtonState buttonState){
		switch ((int)buttonState){
            case 1:
                checkOnBreeders();
                break;
            case 2:
                selectNewParents();
                break;
            case 3:
                beginBreeding();
                break;
        }
	}

	private void checkOnBreeders(){
		if(progressBar.finished()) emptyJar();
		else {
			string dialogueText = "The flies are not finished breeding yet.  Are you sure you want to discard the parents without generating progeny?";
			dialoguePopup.setup(dialogueText,delegate{terminateJarEarly();});
		}
	}

	private void terminateJarEarly(){

		jar.emptyJar();
		jarActionButton.advanceState();
		progressBar.deactivate();

	}

	public void emptyJar(){
		storage.addFlies(jar.emptyJar());
		jarActionButton.advanceState();
	}
	public void selectNewParents(){
		flySelectorManager.setUpSelector(("Jar " + ID), 2, 8);
		flySelectorManager.sendFlies += addNewParents;
		mainPanel.SetActive(false);
	}
	public void beginBreeding(){
		progressBar.activate(.005f);
		jarActionButton.advanceState();
	}

	private void addNewParents(List<Fly> newParents) {
		mainPanel.SetActive(true);
		if (newParents.Count != 0) {
			jar.addParents(newParents);
			jarActionButton.advanceState();
			storage.removeFlies(newParents);
		}
		flySelectorManager.sendFlies -= addNewParents;
	}

}
