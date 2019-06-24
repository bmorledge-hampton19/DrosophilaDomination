using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fly {

    public TraitDB traitDB;
    protected Dictionary<TraitData.TraitID, Trait> traits;
    private Dictionary<TraitData.PhenotypeGroupID, TraitData> expressedTraits;
    public List<TraitData> getExpressedTraits()=>expressedTraits.Values.ToList();
    public int getNumExpressedTraits()=>expressedTraits.Values.Count;
    private List<Markers> markers;

    private bool isMale;
    public bool ismale() => isMale;

    public enum Markers {
        red = 1,
        green = 2,
        blue = 3,
        yellow = 4,
        purple = 5,
        silver = 6,
        gold = 7
    }

    public Fly(Fly maleParent, Fly femaleParent) {
        
        traits = new Dictionary<TraitData.TraitID, Trait>();
        expressedTraits = new Dictionary<TraitData.PhenotypeGroupID, TraitData>();
        markers = new List<Markers>();

        //Roll those dice!  Boy or girl?
        isMale = UnityEngine.Random.Range(0,2) == 1;

        //Determine which traits are possible.
        foreach (TraitData traitData in traitDB.getCurrentTraitTier()) {
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

    public void addMarker(Markers marker){
        if (!markers.Contains(marker)){
            markers.Add(marker);
        }
    }
    public void removeMarker(Markers marker){
        if (markers.Contains(marker)){
            markers.Remove(marker);
        }
    }
    public List<Markers> getMarkers()=>markers;

    public bool hasSameTraits(List<TraitData> traits) => 
    traits.SequenceEqual(expressedTraits.Values.ToList());

    public bool containsMarkers(List<Markers> markers) =>
    !markers.Except(this.markers).Any();

    public bool containsTraits(List<TraitData> traits) =>
    !traits.Except(expressedTraits.Values.ToList()).Any();
}
