using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

	public Dictionary<JarProperty.PropertyType,JarProperty> jarProperties;

    public string name = "";

	private int mutationRate;
	public int getMutationRate() => mutationRate;
	private float survivability;
    public float getSurvivability() => survivability;
	public Dictionary<TraitData.TraitID,float> selectiveSurvivabilityAdvantage;
	public Dictionary<TraitData.TraitID,float> selectiveFitnessAdvantage;
	private float breedingSpeed;
	public float getBreedingSpeed() => breedingSpeed;
	private float fertility;
    public float getFertility() => fertility;
	public Dictionary<FlyStats.StatID,float> statModification;
	private int carryingCapacity;
    public int getCarryingCapacity() => carryingCapacity;

	private GamePhase tier;
	private TraitDB traitDB;

	public void changeJarProperty(JarProperty.PropertyType propertyType, JarProperty property) {
		jarProperties[propertyType] = property;
		setJarStats();
	}

	public Jar(TraitDB traitDB, DefaultProperties defaultProperties){

		this.traitDB = traitDB;

		maleParents = new List<Fly>();
		femaleParents = new List<Fly>();
		progeny = new List<Fly>();
		
		jarProperties = new Dictionary<JarProperty.PropertyType, JarProperty>();
		foreach (JarProperty property in defaultProperties.getDefaultProperties(traitDB.gameManager.getGamePhase())) {
			jarProperties.Add(property.propertyType,property);
		}

		selectiveSurvivabilityAdvantage = new Dictionary<TraitData.TraitID, float>();
		selectiveFitnessAdvantage = new Dictionary<TraitData.TraitID, float>();
		statModification = new Dictionary<FlyStats.StatID, float>();

		setJarStats();

		tier = traitDB.gameManager.getGamePhase();

	}

	private void setJarStats() {
		
		selectiveSurvivabilityAdvantage.Clear();
		selectiveFitnessAdvantage.Clear();
		statModification.Clear();

		mutationRate = 1;
		survivability = 1;
		breedingSpeed = .2f;
		fertility = 5;
		carryingCapacity = 0;

		foreach (JarProperty jarProperty in jarProperties.Values.ToList()) {

			mutationRate *= jarProperty.mutationRate;
			survivability *= jarProperty.survivability;
			breedingSpeed *= jarProperty.breedingSpeed;
			fertility *= jarProperty.fertility;
			carryingCapacity += jarProperty.carryingCapacity;

			foreach (TraitData.TraitID TID in jarProperty.selectiveSurvivabilityAdvantage.Keys.ToList()) {

				if (selectiveSurvivabilityAdvantage.ContainsKey(TID)) {
					selectiveSurvivabilityAdvantage[TID] *= jarProperty.selectiveSurvivabilityAdvantage[TID];
				} else {
					selectiveSurvivabilityAdvantage[TID] = jarProperty.selectiveSurvivabilityAdvantage[TID];
				}

			}

			foreach (TraitData.TraitID TID in jarProperty.selectiveFitnessAdvantage.Keys.ToList()) {

				if (selectiveFitnessAdvantage.ContainsKey(TID)) {
					selectiveFitnessAdvantage[TID] *= jarProperty.selectiveFitnessAdvantage[TID];
				} else {
					selectiveFitnessAdvantage[TID] = jarProperty.selectiveFitnessAdvantage[TID];
				}

			}

			foreach (FlyStats.StatID stat in jarProperty.statModification.Keys.ToList()) {

				if (statModification.ContainsKey(stat)) {
					statModification[stat] *= jarProperty.statModification[stat];
				} else {
					statModification[stat] = jarProperty.statModification[stat];
				}

			}

		}
		
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

			for (int i = 0; i < fertility; i++) {

				Fly newFly = new Fly(maleParents[UnityEngine.Random.Range(0,maleParents.Count)],mom,traitDB);

				float combinedSurvivability = survivability;
				foreach (TraitData trait in newFly.getExpressedTraits()) {
					if (selectiveSurvivabilityAdvantage.ContainsKey(trait.TID)) {
						combinedSurvivability *= selectiveSurvivabilityAdvantage[trait.TID];
					} 
				}

				if (UnityEngine.Random.Range(0f,1f) <= combinedSurvivability) progeny.Add(newFly);

				foreach (TraitData trait in newFly.getExpressedTraits()) {
					if (selectiveFitnessAdvantage.ContainsKey(trait.TID)) {
						newFly.setFitness(newFly.getFitness() * selectiveFitnessAdvantage[trait.TID]);
					} 
				}

			}

		}

		if (progeny.Count > carryingCapacity) removeToCarryingCapacity();

		return true;

	}

	private void removeToCarryingCapacity() {
		
		int numFliesToRemove = progeny.Count - carryingCapacity;
		Dictionary<float,Fly> weightedSelector = new Dictionary<float,Fly>();
		float runningWeightTotal = 0;
		List<Fly> fliesToRemove = new List<Fly>();

		foreach (Fly fly in progeny) {
			runningWeightTotal += fly.getFitness();
			weightedSelector.Add(runningWeightTotal,fly);
		}

		int numFliesRemoved = 0;
		float random;
		while (numFliesRemoved < numFliesToRemove) {
			
			random = UnityEngine.Random.Range(0, runningWeightTotal);

			foreach (float weight in weightedSelector.Keys.ToList()) {
				if (weight > random) {

					if (fliesToRemove.Contains(weightedSelector[weight])) {
						random += weightedSelector[weight].getFitness();
					} else {
						fliesToRemove.Add(weightedSelector[weight]);
						runningWeightTotal -= weightedSelector[weight].getFitness();
						numFliesRemoved++;
						break;
					}
					
				}
			}

		}

		foreach (Fly fly in fliesToRemove) {
			progeny.Remove(fly);
		}

	}

}