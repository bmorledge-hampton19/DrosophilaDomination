using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar {

	private List<Fly> maleParents;
	private List<Fly> femaleParents;
	public List<Fly> getParents() {
		List<Fly> parents = new List<Fly>(maleParents);
		parents.AddRange(femaleParents);
		return parents;
	}
	private List<Fly> progeny;
	public List<Fly> getProgeny() => progeny;

	private TraitDB.GamePhase tier;
	private TraitDB traitDB;

	public Jar(TraitDB traitDB){

		maleParents = new List<Fly>();
		femaleParents = new List<Fly>();
		progeny = new List<Fly>();
		
		this.traitDB = traitDB;

		tier = traitDB.getGamePhase();

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

		maleParents.Clear();
		femaleParents.Clear();

		return fliesToReturn;
	}

    public bool finishedBreeding() => (maleParents.Count < 0 && femaleParents.Count < 0);

    public bool generateProgeny(){

		if (maleParents.Count == 0 || femaleParents.Count == 0) return false;

		foreach (Fly mom in femaleParents) {
			for (int i = 0; i < 10; i++)
			progeny.Add(new Fly(maleParents[UnityEngine.Random.Range(0,maleParents.Count)],mom,traitDB));
		}

		return true;

	}

}
