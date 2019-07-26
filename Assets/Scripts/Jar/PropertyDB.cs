using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPropertyDB", menuName = "Jar/PropertyDB", order = 2)]
public class PropertyDB : ScriptableObject {

    public Dictionary<JarProperty.PropertyType,List<JarProperty>> researchProperties;
    public Dictionary<JarProperty.PropertyType,List<JarProperty>> unityProperties;
    public Dictionary<JarProperty.PropertyType,List<JarProperty>> conquestProperties;
    public Dictionary<JarProperty.PropertyType,List<JarProperty>> explorationProperties;

    private TraitDB.GamePhase gamePhase;
    public TraitDB.GamePhase getGamePhase()=>gamePhase;
    public void advanceGamePhase(){gamePhase++;}

    public Dictionary<TraitDB.GamePhase, Dictionary<JarProperty.PropertyType,List<JarProperty>>> propertyTiers;

    // Use this for initialization
    public void OnEnable() {

        Debug.Log("TraitDB is enabled!");

        researchProperties = new Dictionary<JarProperty.PropertyType, List<JarProperty>>();
        unityProperties = new Dictionary<JarProperty.PropertyType, List<JarProperty>>();
        conquestProperties = new Dictionary<JarProperty.PropertyType, List<JarProperty>>();
        explorationProperties = new Dictionary<JarProperty.PropertyType, List<JarProperty>>();

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
