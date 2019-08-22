using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPropertyDB", menuName = "Jar/PropertyDB", order = 2)]
public class PropertyDB : ScriptableObject {

    public Dictionary<JarProperty.PropertyType,List<JarProperty>> researchProperties;
    public Dictionary<JarProperty.PropertyType,List<JarProperty>> unityProperties;
    public Dictionary<JarProperty.PropertyType,List<JarProperty>> conquestProperties;
    public Dictionary<JarProperty.PropertyType,List<JarProperty>> explorationProperties;

    public List<JarProperty> researchMaterials;
    public List<JarProperty> researchFeedstocks;
    public List<JarProperty> researchNutrients;
    public List<JarProperty> researchFurnishings;

    public List<JarProperty> unityMaterials;
    public List<JarProperty> unityFeedstocks;
    public List<JarProperty> unityNutrients;
    public List<JarProperty> unityFurnishings;

    public List<JarProperty> conquestMaterials;
    public List<JarProperty> conquestFeedstocks;
    public List<JarProperty> conquestNutrients;
    public List<JarProperty> conquestFurnishings;

    public List<JarProperty> explorationMaterials;
    public List<JarProperty> explorationFeedstocks;
    public List<JarProperty> explorationNutrients;
    public List<JarProperty> explorationFurnishings;


    private TraitDB.GamePhase gamePhase;
    public TraitDB.GamePhase getGamePhase()=>gamePhase;
    public void advanceGamePhase(){gamePhase++;}

    public Dictionary<TraitDB.GamePhase, Dictionary<JarProperty.PropertyType,List<JarProperty>>> propertyTiers;

    // Use this for initialization
    public void OnEnable() {

        researchProperties = new Dictionary<JarProperty.PropertyType, List<JarProperty>>();
        researchProperties.Add(JarProperty.PropertyType.material,researchMaterials);
        researchProperties.Add(JarProperty.PropertyType.feedstock,researchFeedstocks);
        researchProperties.Add(JarProperty.PropertyType.nutrients,researchNutrients);
        researchProperties.Add(JarProperty.PropertyType.furnishings,researchFurnishings);

        unityProperties = new Dictionary<JarProperty.PropertyType, List<JarProperty>>();
        unityProperties.Add(JarProperty.PropertyType.material,unityMaterials);
        unityProperties.Add(JarProperty.PropertyType.feedstock,unityFeedstocks);
        unityProperties.Add(JarProperty.PropertyType.nutrients,unityNutrients);
        unityProperties.Add(JarProperty.PropertyType.furnishings,unityFurnishings);

        conquestProperties = new Dictionary<JarProperty.PropertyType, List<JarProperty>>();
        conquestProperties.Add(JarProperty.PropertyType.material,conquestMaterials);
        conquestProperties.Add(JarProperty.PropertyType.feedstock,conquestFeedstocks);
        conquestProperties.Add(JarProperty.PropertyType.nutrients,conquestNutrients);
        conquestProperties.Add(JarProperty.PropertyType.furnishings,conquestFurnishings);

        explorationProperties = new Dictionary<JarProperty.PropertyType, List<JarProperty>>();
        explorationProperties.Add(JarProperty.PropertyType.material,explorationMaterials);
        explorationProperties.Add(JarProperty.PropertyType.feedstock,explorationFeedstocks);
        explorationProperties.Add(JarProperty.PropertyType.nutrients,explorationNutrients);
        explorationProperties.Add(JarProperty.PropertyType.furnishings,explorationFurnishings);

        propertyTiers = new Dictionary<TraitDB.GamePhase, Dictionary<JarProperty.PropertyType,List<JarProperty>>>();

        propertyTiers.Add(TraitDB.GamePhase.research, researchProperties);
        propertyTiers.Add(TraitDB.GamePhase.unity, unityProperties);
        propertyTiers.Add(TraitDB.GamePhase.conquest, conquestProperties);
        propertyTiers.Add(TraitDB.GamePhase.exploration, explorationProperties);
        gamePhase = TraitDB.GamePhase.research;

	}

    public Dictionary<JarProperty.PropertyType,List<JarProperty>> getCurrentPropertyTier()
    {
        //Debug.Log("Returning traits from " + gamePhase + " phase.  " + traitTiers[gamePhase].Count + " traits in total!");
        return propertyTiers[gamePhase];
    }

    public List<JarProperty> getDiscoveredProperties(JarProperty.PropertyType propertyType){
        List<JarProperty> propertiesToReturn = new List<JarProperty>();
        foreach(JarProperty property in getCurrentPropertyTier()[propertyType]){
            if (property.discovered) propertiesToReturn.Add(property);
        }
        return propertiesToReturn;
    }

}
