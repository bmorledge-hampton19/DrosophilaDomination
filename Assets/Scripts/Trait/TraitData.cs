using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Genetics/Trait", order = 1)]
public class TraitData : ScriptableObject
{
    public string traitName = "New Trait";
    public bool discovered;
    public TraitID TID;
    public TraitDB.GamePhase tier;
    public PhenotypeGroupID PGID;  //Lower priority takes presedence.
    public int priority;
    public DominanceBehavior dominance;
    public DominanceBehavior getDominance(){return dominance;}
    public bool isXLinked;
    public bool isIntermediatePhenotype;
    public TraitID complimentaryTraitID;

    public float fitnessModifier;
    public List<SingleStat> stats;

    public static bool operator ==(TraitData trait1, TraitData trait2) => (trait1.TID == trait2.TID);
    public static bool operator !=(TraitData trait1, TraitData trait2) => !(trait1 == trait2);

    public override bool Equals(object other) => this == other as TraitData;
    public override int GetHashCode() => (int)TID;

    public enum TraitID
    {
        [Description("Ebony Body")]
        ebonyBody = 101,
        [Description("Vestigial Wings")]
        vestigialWings = 102,
        [Description("Dumpy Wings")]
        dumpyWings = 103,
        [Description("Golden Eye")]
        goldenEye = 104

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
