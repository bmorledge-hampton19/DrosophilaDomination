using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fly {

    public TraitDB traitDB;
    protected Dictionary<TraitData.TraitID, Trait> traits;
    private Dictionary<TraitData.PhenotypeGroupID, TraitData> expressedTraits;
    public List<TraitData> getExpressedTraits(){return expressedTraits.Values.ToList();}

    private bool isMale;
    public bool ismale() => isMale;

    public Fly(TraitData.TraitTier traitTier, Fly maleParent, Fly femaleParent) {
        
        traits = new Dictionary<TraitData.TraitID, Trait>();
        expressedTraits = new Dictionary<TraitData.PhenotypeGroupID, TraitData>();

        //Roll those dice!  Boy or girl?
        isMale = UnityEngine.Random.Range(0,2) == 1;

        //Determine which traits are possible.
        foreach (TraitData traitData in traitDB.getTraitTier(traitTier)) {
            traits.Add(traitData.TID, new Trait(traitData));
        }

        //Initialize Traits from parental cross.
        cross(maleParent, femaleParent);

    }

    private void cross(Fly maleParent, Fly femaleParent) {
        foreach (TraitData.TraitID key in traits.Keys.ToList()) {
            if (traits[key].isIntermediate() 
            && traits[traits[key].complimentaryTraitID].getAlleles() < 3) {
                traits[key].setAlleles(traits[traits[key].complimentaryTraitID].getAlleles());
            } else {
                int alleles = maleParent.traits[key].getGameteAlleles(true,!isMale)
                + femaleParent.traits[key].getGameteAlleles(false,!isMale);
            
                traits[key].setAlleles(alleles);
            }
            if (traits[key].isPresent(isMale)){
                if (!expressedTraits.ContainsKey(traits[key].PGID) || 
                expressedTraits[traits[key].PGID].priority > traits[key].priority) {
                    expressedTraits[traits[key].PGID] = traits[key] as TraitData;
                }
            }
        }
    }

    public bool hasSameTraits(List<TraitData> traits, bool isMale) => 
    (traits.SequenceEqual(expressedTraits.Values.ToList()) && this.isMale == isMale);

    public bool hasSameTraits(List<TraitData> traits) => 
    traits.SequenceEqual(expressedTraits.Values.ToList());


    public bool containsTraits(List<TraitData> traits) =>
    !traits.Except(expressedTraits.Values.ToList()).Any();
}
