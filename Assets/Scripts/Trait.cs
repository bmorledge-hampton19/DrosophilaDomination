using UnityEngine;
using System;

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