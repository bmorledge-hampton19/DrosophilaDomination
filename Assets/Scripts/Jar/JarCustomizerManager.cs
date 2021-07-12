using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JarCustomizerManager : MonoBehaviour
{

    Jar jar;
    private Dictionary<JarProperty.PropertyType,JarProperty> initialProperties;

    public delegate void propertiesChanged();
    public event propertiesChanged alterProperties;

    public GameObject materialSelectorPanel;
    public GameObject feedstockSelectorPanel;
    public GameObject nutrientSelectorPanel;
    public GameObject furnishingsSelectorPanel;

    private Dictionary<JarProperty.PropertyType,CustomizerDropdown> dropdowns;

    public JarTTManager jarInfo;
    public JarTTManager propertyInfo;

    public Button okButton;
    public Button cancelButton;

    public GameObject inputBlocker;

    public PropertyDB propertyDB;

    void Awake()
    {
        dropdowns = new Dictionary<JarProperty.PropertyType, CustomizerDropdown>();
        initialProperties = new Dictionary<JarProperty.PropertyType, JarProperty>();

        initializeDropdownDictionary();

        setupDropdownFunctions();

        setupMouseoverFunctions();

        setupButtonFunctions();

    }

    void Start() {

        

    }

    private void initializeDropdownDictionary() {
        
        dropdowns.Add(JarProperty.PropertyType.material,
            new CustomizerDropdown(materialSelectorPanel.GetComponentInChildren<Dropdown>(),JarProperty.PropertyType.material,propertyDB));

        dropdowns.Add(JarProperty.PropertyType.feedstock,
            new CustomizerDropdown(feedstockSelectorPanel.GetComponentInChildren<Dropdown>(),JarProperty.PropertyType.feedstock,propertyDB));

        dropdowns.Add(JarProperty.PropertyType.nutrients,
            new CustomizerDropdown(nutrientSelectorPanel.GetComponentInChildren<Dropdown>(),JarProperty.PropertyType.nutrients,propertyDB));

        dropdowns.Add(JarProperty.PropertyType.furnishings,
            new CustomizerDropdown(furnishingsSelectorPanel.GetComponentInChildren<Dropdown>(),JarProperty.PropertyType.furnishings,propertyDB));

    }

    private void setupDropdownFunctions() {

        foreach(JarProperty.PropertyType PT in dropdowns.Keys.ToList()) {
            dropdowns[PT].GetDropdown().onValueChanged.AddListener(delegate{propertyChanged(PT);});
        }

    }

    public void propertyChanged(JarProperty.PropertyType propertyType){

        jar.changeJarProperty(propertyType,dropdowns[propertyType].returnCurrentProperty());
        jarInfo.setupTooltip(jar);

    }

    private void setupMouseoverFunctions() {

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener( (eventData) => {propertyInfo.destroyTooltip();} );
        this.gameObject.GetComponentInChildren<EventTrigger>().triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener( (eventData) => {propertyInfo.setupTooltip(jar.jarProperties[JarProperty.PropertyType.material]);} );
        materialSelectorPanel.GetComponent<EventTrigger>().triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener( (eventData) => {propertyInfo.setupTooltip(jar.jarProperties[JarProperty.PropertyType.feedstock]);} );
        feedstockSelectorPanel.GetComponent<EventTrigger>().triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener( (eventData) => {propertyInfo.setupTooltip(jar.jarProperties[JarProperty.PropertyType.nutrients]);} );
        nutrientSelectorPanel.GetComponent<EventTrigger>().triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener( (eventData) => {propertyInfo.setupTooltip(jar.jarProperties[JarProperty.PropertyType.furnishings]);} );
        furnishingsSelectorPanel.GetComponent<EventTrigger>().triggers.Add(entry);

    }

    private void setupButtonFunctions() {
        okButton.onClick.AddListener(ok);
        cancelButton.onClick.AddListener(cancel);
    }

    private void finalize() {
        this.gameObject.SetActive(false);
        inputBlocker.SetActive(false);
    }

    private void ok() {
        alterProperties();
        finalize();
    }

    private void cancel() {

        foreach(JarProperty.PropertyType PT in initialProperties.Keys.ToList()) {
            jar.changeJarProperty(PT,initialProperties[PT]);
        }

        finalize();

    }

    public void setupCustomizer(Jar jar, PropertyDB propertyDB) {
    
        this.jar = jar;
        this.propertyDB = propertyDB;

        this.gameObject.SetActive(true);
        inputBlocker.SetActive(true);

        setupDropdowns();
        setupInitialProperties();
        jarInfo.setupTooltip(jar);

    }

    private void setupDropdowns() {

        foreach(JarProperty.PropertyType propertyType in System.Enum.GetValues(typeof(JarProperty.PropertyType))){
            dropdowns[propertyType].setupDropdown(jar.jarProperties[propertyType]);
        }

    }

    private void setupInitialProperties() {
        foreach(JarProperty.PropertyType PT in jar.jarProperties.Keys.ToList()) {
            initialProperties[PT] = jar.jarProperties[PT];
        }
    }

}





class CustomizerDropdown {
    Dropdown dropdown;
    public Dropdown GetDropdown() => dropdown;
    JarProperty.PropertyType propertyType;
    PropertyDB propertyDB;

    public CustomizerDropdown(Dropdown dropdown, JarProperty.PropertyType propertyType, PropertyDB propertyDB){
        this.dropdown = dropdown;
        this.propertyType = propertyType;
        this.propertyDB = propertyDB;
    }

    public void setupDropdown(JarProperty currentProperty) {

        dropdown.ClearOptions();

        foreach(JarProperty property in propertyDB.getPropertiesByType(propertyType)) {
            dropdown.AddOptions(new List<string> {property.objectName});
            if (property == currentProperty) dropdown.SetValueWithoutNotify(dropdown.options.Count-1);
        }

    }

    public JarProperty returnCurrentProperty() 
        => propertyDB.getPropertiesByType(propertyType)[dropdown.value];

    

}