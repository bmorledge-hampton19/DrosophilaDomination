using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Jar/Property", order = 2)]
public class JarProperty : ScriptableObject
{

    public enum PropertyType{
        material = 0,
        feedstock = 1,
        furnishings = 2,
        nutrients = 3
    }

    public PropertyType propertyType;

    public int mutationRate = 1;
    public float mortality = 1;

    public List<TraitData.TraitID> selectiveMortalityTargets;
    public List<float> selectiveMortalityStrength;
    public Dictionary<TraitData.TraitID,float> selectiveMortality;

    public float breedingSpeed = 1;
    public float fertility = 1;
    public int carryingCapacity = 0;

    public List<FlyStats.StatID> statModificationTargets;
    public List<int> statModificationStrength;
    public Dictionary<FlyStats.StatID,int> statModification;

    public void Awake() {

        if (selectiveMortalityTargets != null) {
            selectiveMortality = new Dictionary<TraitData.TraitID, float>();
            for (int i = 0; i < selectiveMortalityTargets.Count; i++) {
                selectiveMortality.Add(selectiveMortalityTargets[i],selectiveMortalityStrength[i]);
            }
        }

        if (statModificationTargets != null) {
            statModification = new Dictionary<FlyStats.StatID, int>();
            for (int i = 0; i < statModificationTargets.Count; i++) {
                statModification.Add(statModificationTargets[i],statModificationStrength[i]);
            }
        }

    }

}
