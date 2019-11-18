using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyDB : DataObjectOrganizer {

        public List<JarProperty> getDiscoveredObjects(JarProperty.PropertyType propertyType){
        List<JarProperty> propertiesToReturn = new List<JarProperty>();
        foreach(JarProperty property in discoveredObjects[gameManager.getGamePhase()]){
            if (property.propertyType == propertyType) propertiesToReturn.Add(property);
        }
        return propertiesToReturn;
    }

}
