using System.Collections;
using System.Collections.Generic;
using static EnumHelper;
using static JarProperty;
using UnityEngine;
using System.Linq;

public class PropertyDB : DataObjectOrganizer<JarProperty> {

    Dictionary<GamePhase, Dictionary<PropertyType,List<JarProperty>>> propertiesByTypeByPhase;

    public new void Awake() {
        base.Awake();

        propertiesByTypeByPhase = new Dictionary<GamePhase, Dictionary<PropertyType, List<JarProperty>>>();

        foreach(GamePhase gamePhase in GetEnumerable<GamePhase>()) {
            propertiesByTypeByPhase[gamePhase] = new Dictionary<PropertyType, List<JarProperty>>();
            foreach(PropertyType propertyType in GetEnumerable<PropertyType>()) {
                propertiesByTypeByPhase[gamePhase][propertyType] = new List<JarProperty>();
            }

        }

        foreach(GamePhase gamePhase in GetEnumerable<GamePhase>()) {
            foreach(JarProperty property in objectTiers[gamePhase]) {
                propertiesByTypeByPhase[gamePhase][property.propertyType].Add(property);
            }
        }

    }

    public void discoverJarProperty(JarProperty jarProperty) {
        jarProperty.discover();
    }

    public List<JarProperty> getPropertiesByType(PropertyType propertyType, bool? discovered = null){
        
        var properties = propertiesByTypeByPhase[gameManager.getGamePhase()][propertyType];

        if (discovered is null)
            return properties;
        else if ((bool)discovered)
            return properties.Where(upgrade => upgrade.isDiscovered()).ToList();
        else
            return properties.Where(upgrade => !upgrade.isDiscovered()).ToList();

    }


}
