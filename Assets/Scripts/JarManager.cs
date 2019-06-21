using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarManager : MonoBehaviour {

	public Storage storage;
	private Jar jar;
	private int ID;
	public void SetID(int ID){this.ID = ID;}
	
	private ProgressBar progressBar;
	private JarActionButton jarActionButton;

	// Use this for initialization
	void Start () {
		
		jar = new Jar(TraitDB.GamePhase.research);

		progressBar = GameObject.Find("Progress Bar").GetComponent<ProgressBar>();
		progressBar.fillActions += breedFlies;

		jarActionButton = GameObject.Find("Jar Management Panel/Jar Action Button").GetComponent<JarActionButton>();
		jarActionButton.pressActions += jarAction;
		

	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnDisable() {
		progressBar.fillActions-=breedFlies;
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
	}
	public void selectNewParents(){
		List<Fly> newParents = new List<Fly>();
		//TODO: Fly Selector goes here!
		jar.addParents(newParents);
	}
	public void beginBreeding(){
		progressBar.activate(.05f);
	}

}
