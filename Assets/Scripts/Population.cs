using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    public TraitDB allTraits;

    int n = 0;

    Dictionary<Trait.TraitID, double> alleleFrequency;

    Trait.TraitTier traitTierGroup;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void initAlleleFrequency()
    {
        foreach (Trait trait in allTraits.getTraitTier(traitTierGroup))
        {
            alleleFrequency.Add(trait.TID, 0);
        }
    }
}
