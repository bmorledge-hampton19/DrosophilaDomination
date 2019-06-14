using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Genetics/Trait", order = 1)]
public class TraitData : ScriptableObject
{
    public string traitName = "New Trait";
    public TraitID TID;
    public TraitTier tier;
    public PhenotypeGroupID PGID;  //Lower priority takes presedence.
    public int priority;
    protected DominanceBehavior dominance;
    public DominanceBehavior getDominance(){return dominance;}
    public bool isXLinked;
    public bool isIntermediatePhenotype;
    public TraitID complimentaryTraitID;

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

    public bool isDominant(){return dominance == DominanceBehavior.dominant;}
    public bool isRecessive(){return dominance == DominanceBehavior.recessive;}
    public bool isIntermediate(){return dominance == DominanceBehavior.intermediate;}
}

public class Trait : TraitData 
{


public Trait(TraitData traitData) {
    alleles = 3; // signifies an uninitialized value, not a trisomic fly.
    traitName = traitData.traitName;
    TID = traitData.TID;
    tier = traitData.tier;
    PGID = traitData.PGID;
    priority = traitData.priority;
    dominance = traitData.getDominance();
    isXLinked = traitData.isXLinked;
    isIntermediatePhenotype = traitData.isIntermediatePhenotype;
    complimentaryTraitID = traitData.complimentaryTraitID;
}

private int alleles;
public void setAlleles(int newAlleles){alleles = newAlleles;}
public int getAlleles(){return alleles;}

public int getGameteAlleles(bool parentIsMale, bool maleGivesX){
    
    if (isXLinked && parentIsMale) {
        if (maleGivesX) return alleles;
        else return 0;
    } else {
        if (alleles == 1) return UnityEngine.Random.Range(0,2);
        else return alleles/2;
    }

}

public bool isPresent(bool flyIsMale) 
{
    if (isDominant()) return alleles > 0;
    else if (isRecessive()) {
         if (isXLinked && flyIsMale) return alleles > 0;
         else return alleles > 1;
    }
    else if (isIntermediate()) {

        if (isIntermediatePhenotype) return alleles == 1;
        else return alleles == 2;

    }
    else throw new InvalidOperationException();

}

}