using UnityEngine;
using System;

public class Trait
{

private TraitData traitData;

public Trait(TraitData traitData) {
    alleles = 3; // signifies an uninitialized value, not a trisomic fly.
    this.traitData = traitData;
}

private int alleles;
public void setAlleles(int newAlleles){alleles = newAlleles;}
public int getAlleles(){return alleles;}
public TraitData getTraitData() => traitData;

public int getGameteAlleles(bool parentIsMale, bool maleGivesX){
    
    if (traitData.isXLinked && parentIsMale) {
        if (maleGivesX) return alleles;
        else return 0;
    } else {
        if (alleles == 1) return UnityEngine.Random.Range(0,2);
        else return alleles/2;
    }

}

public bool isPresent(bool flyIsMale) 
{
    if (traitData.isDominant()) return alleles > 0;
    else if (traitData.isRecessive()) {
         if (traitData.isXLinked && flyIsMale) return alleles > 0;
         else return alleles > 1;
    }
    else if (traitData.isIntermediate()) {

        if (traitData.isIntermediatePhenotype) return alleles == 1;
        else return alleles == 2;

    }
    else throw new InvalidOperationException();

}

}