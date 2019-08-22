using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarUIManager : MonoBehaviour
{
    public GameObject jarUI;
    public GameObject jarTooltip;

    public Dictionary<JarProperty.PropertyType,TTMouseoverManager> tooltipManagers;
    public TTMouseoverManager materialTTManager;
    public TTMouseoverManager feedstockTTManager;
    public TTMouseoverManager nutrietntTTManager;
    public TTMouseoverManager furnishingsTTManager;

    public JarActionButton jarActionButton;
    public Button customizeButton;

    public RectTransform progressBarImage;
    public Text progressText;
    public Text statusText;

    void Awake() {

        tooltipManagers = new Dictionary<JarProperty.PropertyType, TTMouseoverManager>();
        tooltipManagers[JarProperty.PropertyType.material] = materialTTManager;
        tooltipManagers[JarProperty.PropertyType.feedstock] = feedstockTTManager;
        tooltipManagers[JarProperty.PropertyType.nutrients] = nutrietntTTManager;
        tooltipManagers[JarProperty.PropertyType.furnishings] = furnishingsTTManager;

    }

    public void setUpTooltipManagers(Dictionary<JarProperty.PropertyType,JarProperty> properties) {

        foreach (JarProperty.PropertyType propertyType in System.Enum.GetValues(typeof(JarProperty.PropertyType))) {
			tooltipManagers[propertyType].setup(jarTooltip);
            changeProperty(properties[propertyType]);
		}

    }

    public void changeProperty(JarProperty newProperty) {

        tooltipManagers[newProperty.propertyType].changeProperty(newProperty);
        tooltipManagers[newProperty.propertyType].gameObject.GetComponent<Text>().text = 
            EnumHelper.GetDescription(newProperty.propertyType) + ": " + newProperty.name;

    }

    public void setUpProgressBar(ProgressBar progressBar){
        progressBar.fillImage = progressBarImage;
        progressBar.progressText = progressText;
        progressBar.statusText = statusText;
        progressBar.initFillImage();
    }



    public void disableUI(){
        jarUI.SetActive(false);
    }

    public void enableUI(){
        jarUI.SetActive(true);
    }

}
