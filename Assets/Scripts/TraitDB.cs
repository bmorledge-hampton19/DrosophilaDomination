using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitDB : MonoBehaviour {

    public List<Trait> basicTraits;
    public List<Trait> whimsicalTraits;

    public Dictionary<Trait.TraitTier, List<Trait>> traitTiers;

    // Use this for initialization
    void Start () {

        traitTiers.Add(Trait.TraitTier.basic, basicTraits);
        traitTiers.Add(Trait.TraitTier.whimsical, whimsicalTraits);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Trait> getTraitTier(Trait.TraitTier traitTier)
    {
        return traitTiers[traitTier];
    }

}
