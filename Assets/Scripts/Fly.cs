using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fly : MonoBehaviour
{

    private TraitDB traitDB;
    protected Dictionary<TraitData.TraitID, Trait> traits;
    private Dictionary<TraitData.PhenotypeGroupID, Trait> expressedTraits;
    public List<Trait> getExpressedTraits(){return expressedTraits.Values.ToList();}

    private bool isMale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Fly(TraitData.TraitTier traitTier, Fly maleParent, Fly femaleParent) {
        
        //Roll those dice!  Boy or girl?
        isMale = UnityEngine.Random.Range(0,2) == 1;

        //Determine which traits are possible.
        foreach (TraitData traitData in traitDB.getTraitTier(traitTier)) {
            traits.Add(traitData.TID, new Trait(traitData));
        }

        //Initialize Traits from parental cross.
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
                    expressedTraits[traits[key].PGID] = traits[key];
                }
            }
        }

    }


}
