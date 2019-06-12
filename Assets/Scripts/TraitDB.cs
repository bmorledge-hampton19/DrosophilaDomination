using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitDB : MonoBehaviour {

    public List<TraitData> researchTraits;
    public List<TraitData> unityTraits;
    public List<TraitData> conquestTraits;
    public List<TraitData> explorationTraits;

    public Dictionary<TraitData.TraitTier, List<TraitData>> traitTiers;

    // Use this for initialization
    void Start () {

        traitTiers.Add(Trait.TraitTier.research, researchTraits);
        traitTiers.Add(Trait.TraitTier.unity, unityTraits);
        traitTiers.Add(Trait.TraitTier.conquest, conquestTraits);
        traitTiers.Add(Trait.TraitTier.exploration, explorationTraits);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<TraitData> getTraitTier(Trait.TraitTier traitTier)
    {
        return traitTiers[traitTier];
    }

}
