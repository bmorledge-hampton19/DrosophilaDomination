using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PropertySelectorManager : MonoBehaviour
{
    
    private Dictionary<JarProperty,GameObject> options;
    public JarProperty.PropertyType propertyType;
    private Dictionary<int,JarProperty> dropdownToProperty;
    
    public JarProperty selectedProperty;

    public PropertyDB propertyDB;
    public Dropdown dropdown;
    public GameObject parentOfOptions;

    void Awake() {
        options = new Dictionary<JarProperty, GameObject>();
        dropdownToProperty = new Dictionary<int, JarProperty>();
    }

    void OnEnable() {

        List<JarProperty> discoveredProperties = propertyDB.getDiscoveredProperties(propertyType);
        if (options.Count != discoveredProperties.Count) updateProperties(discoveredProperties);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateProperties(List<JarProperty> discoveredProperties) {

        dropdown.ClearOptions();

        foreach (JarProperty property in propertyDB.getDiscoveredProperties(propertyType)) {
            dropdown.AddOptions(new List<string>(){property.name});
            dropdownToProperty[dropdownToProperty.Count] = property;
            
        }

        

    }

}
