using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProperty", menuName = "Jar/Property", order = 1)]
public class JarProperty : ScriptableObject
{

    public string propertyName;

    public enum PropertyType{
        material = 0,
        feedstock = 1,
        furnishings = 2,
        nutrients = 3
    }

    public PropertyType propertyType;

    public int mutationRate = 1;
    public float survivability = 1;

    public List<TraitData.TraitID> selectiveAdvantageTargets;
    public List<float> selectiveSurvivabilityAdvantageStrength;
    public List<float> selectiveFitnessAdvantageStrength;
    public Dictionary<TraitData.TraitID,float> selectiveSurvivabilityAdvantage;
    public Dictionary<TraitData.TraitID,float> selectiveFitnessAdvantage;

    public float breedingSpeed = 1;
    public float fertility = 1;
    public int carryingCapacity = 0;

    public List<FlyStats.StatID> statModificationTargets;
    public List<float> statModificationStrength;
    public Dictionary<FlyStats.StatID,float> statModification;

    public bool discovered = false;

    void OnEnable() {

        selectiveSurvivabilityAdvantage = new Dictionary<TraitData.TraitID, float>();
        if (selectiveAdvantageTargets != null) {
            for (int i = 0; i < selectiveAdvantageTargets.Count; i++) {
                selectiveSurvivabilityAdvantage.Add(selectiveAdvantageTargets[i],selectiveSurvivabilityAdvantageStrength[i]);
            }
        }

        selectiveFitnessAdvantage = new Dictionary<TraitData.TraitID, float>();
        if (selectiveAdvantageTargets != null) {
            for (int i = 0; i < selectiveAdvantageTargets.Count; i++) {
                selectiveFitnessAdvantage.Add(selectiveAdvantageTargets[i],selectiveFitnessAdvantageStrength[i]);
            }
        }

        statModification = new Dictionary<FlyStats.StatID, float>();
        if (statModificationTargets != null) {
            for (int i = 0; i < statModificationTargets.Count; i++) {
                statModification.Add(statModificationTargets[i],statModificationStrength[i]);
            }
        }

    }

}
