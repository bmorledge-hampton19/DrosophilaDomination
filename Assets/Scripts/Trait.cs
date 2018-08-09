using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Genetics/Trait", order = 1)]
public class Trait : ScriptableObject
{
    public string traitName = "New Trait";
    public TraitID TID;
    public TraitTier tier;

    public PhenotypeGroupID PGID;
    public int priority = 0;
    public bool dominant = false;

    public enum TraitID
    {
        ebonyBody = 101,
        vestigialWings = 102,
        dumpyWings = 103,
        goldenEyes = 201
    }

    public enum TraitTier
    {
        basic = 1,
        whimsical = 2
    }

    public enum PhenotypeGroupID
    {
        bodyColor = 1,
        wingShape = 2,
        eyeColor = 3
    }

}