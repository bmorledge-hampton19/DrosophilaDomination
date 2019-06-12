using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Genetics/Trait", order = 1)]
public class TraitData : ScriptableObject
{
    public string traitName = "New Trait";
    public TraitID TID;
    public TraitTier tier;
    public PhenotypeGroupID PGID;
    public int priority;
    public DominanceBehavior dominance;
    public bool isXLinked;
    public bool isIntermediatePhenotype;
    public TraitData complimentaryTrait;

    public enum TraitID
    {
        ebonyBody = 101,
        vestigialWings = 102,
        dumpyWings = 103,


    }

    public enum TraitTier
    {
        research = 1,
        unity = 2,
        conquest = 3,
        exploration = 4
    }

    public enum PhenotypeGroupID
    {
        wingStructure = 0,
        wingComposition = 1,
        bodyStructure = 2,
        bodyComposition = 3,
        headStructure = 4,
        headComposition = 5,
        limbs = 6,
        eyes = 7,
        other = 8
    }

    public enum DominanceBehavior
    {

        dominant = 0,
        recessive = 1,
        intermediate = 2

    }

}

public class Trait : TraitData 
{


public Trait(TraitData traitData) {
    alleles = 3; // signifies an uninitialized value, not a trisomic fly ;)
    traitName = traitData.traitName;
    TID = traitData.TID;
    tier = traitData.tier;
    PGID = traitData.PGID;
    priority = traitData.priority;
    dominance = traitData.dominance;
    isXLinked = traitData.isXLinked;
    isIntermediatePhenotype = traitData.isIntermediatePhenotype;
    complimentaryTrait = traitData.complimentaryTrait;
}

public int alleles;

public int getGameteAlleles(bool isMale){
    
    if (isXLinked && isMale) {
        return alleles;
    } else {
        if (alleles == 1) return UnityEngine.Random.Range(0,1);
        else return alleles/2;
    }

}

public bool isExpressed() 
{
    if (dominance == DominanceBehavior.dominant) return alleles > 0;
    else if (dominance == DominanceBehavior.recessive) return alleles > 1;
    else if (dominance == DominanceBehavior.intermediate) {

        if (isIntermediatePhenotype) return alleles == 1;
        else return alleles == 2;

    }
    else throw new InvalidOperationException();

}

}