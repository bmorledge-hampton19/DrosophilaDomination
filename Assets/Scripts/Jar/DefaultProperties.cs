using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDefaults", menuName = "Jar/Defaults", order = 2)]
public class DefaultProperties : ScriptableObject
{

    public List<JarProperty> tier1Properties;
    public List<JarProperty> tier2Properties;
    public List<JarProperty> tier3Properties;
    public List<JarProperty> tier4Properties;

    public Dictionary<TraitDB.GamePhase,List<JarProperty>> defaultProperties;

    public List<JarProperty> getDefaultProperties(TraitDB.GamePhase gamePhase) => defaultProperties[gamePhase];

    void OnEnable() {
        defaultProperties = new Dictionary<TraitDB.GamePhase, List<JarProperty>>();
        defaultProperties.Add(TraitDB.GamePhase.research, tier1Properties);
        defaultProperties.Add(TraitDB.GamePhase.unity, tier2Properties);
        defaultProperties.Add(TraitDB.GamePhase.conquest, tier3Properties);
        defaultProperties.Add(TraitDB.GamePhase.exploration, tier4Properties);
    }

}
