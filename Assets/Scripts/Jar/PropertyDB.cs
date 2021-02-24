using System.Collections;
using System.Collections.Generic;
using static EnumHelper;
using static JarProperty;
using UnityEngine;

public class PropertyDB : DataObjectOrganizer {

    Dictionary<GamePhase, Dictionary<PropertyType,List<JarProperty>>> discoveredPropertiesByTypeByPhase;

    public List<JarProperty> getDiscoveredProperties(PropertyType propertyType){
        return discoveredPropertiesByTypeByPhase[gameManager.getGamePhase()][propertyType];
    }

    public new void Awake() {
        base.Awake();

        discoveredPropertiesByTypeByPhase = new Dictionary<GamePhase, Dictionary<PropertyType, List<JarProperty>>>();
        foreach(GamePhase gamePhase in GetEnumerable<GamePhase>()) {

            discoveredPropertiesByTypeByPhase[gamePhase] = new Dictionary<PropertyType, List<JarProperty>>();

            foreach(PropertyType propertyType in GetEnumerable<PropertyType>()) {

                discoveredPropertiesByTypeByPhase[gamePhase][propertyType] = new List<JarProperty>();

            }

            foreach(JarProperty jarProperty in discoveredObjects[gamePhase]) {

                discoveredPropertiesByTypeByPhase[gamePhase][jarProperty.propertyType].Add(jarProperty);

            }

        }

    }

    public void discoverObject(JarProperty jarProperty) {
        base.discoverObject(jarProperty);
        discoveredPropertiesByTypeByPhase[gameManager.getGamePhase()][jarProperty.propertyType].Add(jarProperty);
    }


}
