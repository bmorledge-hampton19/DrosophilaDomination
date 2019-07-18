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

	private Dictionary<JarProperty.PropertyType,JarProperty> jarProperties;
	public DefaultProperties defaultProperties;

	private int mutationRate;
	public int getMutationRate() => mutationRate;
	private float mortality;
	public Dictionary<TraitData.TraitID,float> selectiveMortality;
	private float breedingSpeed;
	public float getBreedingSpeed() => breedingSpeed;
	private float fertility;
	public Dictionary<FlyStats.StatID,float> statModfication;
	private int carryingCapacity;

	private TraitDB.GamePhase tier;
	private TraitDB traitDB;

	public void changeJarProperty(JarProperty.PropertyType propertyType, JarProperty property) {
		jarProperties[propertyType] = property;
		setJarStats();
	}

	public Jar(TraitDB traitDB){

		maleParents = new List<Fly>();
		femaleParents = new List<Fly>();
		progeny = new List<Fly>();
		
		jarProperties = new Dictionary<JarProperty.PropertyType, JarProperty>();
		foreach (JarProperty property in defaultProperties.getDefaultProperties(traitDB.getGamePhase())) {
			jarProperties.Add(property.propertyType,property);
		}

		selectiveMortality = new Dictionary<TraitData.TraitID, float>();
		statModfication = new Dictionary<FlyStats.StatID, float>();

		this.traitDB = traitDB;

		tier = traitDB.getGamePhase();

	}

	private void setJarStats() {
		
		selectiveMortality.Clear();
		statModfication.Clear();

		mutationRate = 1;
		mortality = 1;
		breedingSpeed = .2f;
		fertility = 5;
		carryingCapacity = 0;

		foreach (JarProperty jarProperty in jarProperties.Values.ToList()) {

			mutationRate *= jarProperty.mutationRate;
			mortality *= jarProperty.mortality;
			breedingSpeed *= jarProperty.breedingSpeed;
			fertility *= jarProperty.fertility;
			carryingCapacity += jarProperty.carryingCapacity;

			foreach (TraitData.TraitID TID in jarProperty.selectiveMortality.Keys.ToList()) {

				if (selectiveMortality.ContainsKey(TID)) {
					selectiveMortality[TID] *= jarProperty.selectiveMortality[TID];
				} else {
					selectiveMortality[TID] = jarProperty.selectiveMortality[TID];
				}

			}

			foreach (FlyStats.StatID stat in jarProperty.statModification.Keys.ToList()) {

				if (statModfication.ContainsKey(stat)) {
					statModfication[stat] *= jarProperty.statModification[stat];
				} else {
					statModfication[stat] = jarProperty.statModification[stat];
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

				float combinedMortality = mortality;
				foreach (TraitData trait in newFly.getExpressedTraits()) {
					if (selectiveMortality.ContainsKey(trait.TID)) {
						combinedMortality *= selectiveMortality[trait.TID];
					} 
				}

				if (UnityEngine.Random.Range(0f,1f) <= combinedMortality) progeny.Add(newFly);
			}

		}

		while (progeny.Count > carryingCapacity) {
			progeny.RemoveAt(UnityEngine.Random.Range(0,progeny.Count));
		}

		return true;

	}

}
