using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour {

    private List<GameObject> flyGraphics;
	private List<Fly> maleParents;
	private List<Fly> femaleParents;
	private List<Fly> progeny;

	private TraitData.TraitTier tier;

	public Jar(TraitData.TraitTier tier){

		maleParents = new List<Fly>();
		femaleParents = new List<Fly>();
		progeny = new List<Fly>();
		

		this.tier = tier;

	}

	public void addParents(List<Fly> parents) {
		foreach(Fly fly in parents) {
			if (fly.ismale()) {
				maleParents.Add(fly);
			} else {
				femaleParents.Add(fly);
			}
		}
	}

	public List<Fly> emptyJar() {
		List<Fly> fliesToReturn = new List<Fly>(progeny);
		progeny.Clear();
		return fliesToReturn;
	}

	public bool generateProgeny(){

		if (maleParents.Count == 0 || femaleParents.Count == 0) return false;

		foreach (Fly mom in femaleParents) {
			for (int i = 0; i < 10; i++)
			progeny.Add(new Fly(tier,maleParents[UnityEngine.Random.Range(0,maleParents.Count)],mom));
		}

		maleParents.Clear();
		femaleParents.Clear();

		return true;

	}

	// Use this for initialization
	void Start () {
		
	}
	


	// Update is called once per frame
	void Update () {
		
	}
}
