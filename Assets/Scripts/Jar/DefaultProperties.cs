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

    public Dictionary<GamePhase,List<JarProperty>> defaultProperties;

    public List<JarProperty> getDefaultProperties(GamePhase gamePhase) => defaultProperties[gamePhase];

    void OnEnable() {
        defaultProperties = new Dictionary<GamePhase, List<JarProperty>>();
        defaultProperties.Add(GamePhase.research, tier1Properties);
        defaultProperties.Add(GamePhase.unity, tier2Properties);
        defaultProperties.Add(GamePhase.conquest, tier3Properties);
        defaultProperties.Add(GamePhase.exploration, tier4Properties);
    }

}
