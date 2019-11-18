using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class JarTTManager : MonoBehaviour
{

    float breedingSpeed;
    float fertility;
    float survivability;
    int carryingCapacity;
    int mutationRate;

    Dictionary<TraitData.TraitID,float> selectiveSurvivabilityAdvantage;
	Dictionary<TraitData.TraitID,float> selectiveFitnessAdvantage;
    Dictionary<FlyStats.StatID,float> statModification;


    public Text Title;

    public Text groupedStats;
    public RectTransform modifiersAndAdvantages;
    float heightStandard;

    public LayoutElement modifierTitleLayout;
    public GameObject modifierPanel;
    public RectTransform modifierParent;
    public GameObject modifierPrefab;
    private List<GameObject> modifiers;

    public LayoutElement advantageTitleLayout;
    public GameObject advantagePanel;
    public RectTransform advantageParent;
    public GameObject advantagePrefab;
    public List<GameObject> advantages;

    public bool followMouse = false;
    bool cleaned = true;

    void Awake() {
        modifiers = new List<GameObject>();
        advantages = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (followMouse) lockToMouse();

    }

    public void setToFollowMouse() {followMouse = true;}

    private void lockToMouse() {

        transform.position = Input.mousePosition + new Vector3(5,5);

    }

    public void setupTooltip(JarProperty property) {

        Title.text = property.objectName;

        breedingSpeed = property.breedingSpeed;
        fertility = property.fertility;
        survivability = property.survivability;
        carryingCapacity = property.carryingCapacity;
        mutationRate = property.mutationRate;

        selectiveFitnessAdvantage = property.selectiveFitnessAdvantage;
        selectiveSurvivabilityAdvantage = property.selectiveSurvivabilityAdvantage;
        statModification = property.statModification;

        setupTooltip();

    }

    public void setupTooltip(Jar jar) {

        Title.text = jar.name;

        breedingSpeed = jar.getBreedingSpeed();
        fertility = jar.getFertility();
        survivability = jar.getSurvivability();
        carryingCapacity = jar.getCarryingCapacity();
        mutationRate = jar.getMutationRate();

        selectiveFitnessAdvantage = jar.selectiveFitnessAdvantage;
        selectiveSurvivabilityAdvantage = jar.selectiveSurvivabilityAdvantage;
        statModification = jar.statModification;

        setupTooltip();

    }

    private void setupTooltip() {

        if (!cleaned) destroyTooltip();

        this.gameObject.SetActive(true);
        determineScalingRatio();
        setupGroupedStats();
        setupStatModifiers();
        setupTraitAdvantages();
        if (followMouse) lockToMouse();

        cleaned = false;

    }

    private void determineScalingRatio() {

        heightStandard = modifiersAndAdvantages.rect.height/6;

    }

    private void setupGroupedStats() {

        string statText = "";

        statText += "Breeding Speed: x" + breedingSpeed + "\n";
        statText += "Fertility: x" + fertility + "\n";
        statText += "Survivability: x" + survivability + "\n";
        statText += "Capacity: ";
        if (carryingCapacity >= 0) statText += "+";
        statText += carryingCapacity + "\n";
        statText += "Mutation Rate: x" + mutationRate;

        groupedStats.text = statText;

    }

    private void setupStatModifiers() {

        modifierPanel.gameObject.SetActive(false);

        if (statModification.Count > 0) {

            modifierPanel.gameObject.SetActive(true);
            modifierTitleLayout.preferredHeight = heightStandard;

            foreach (FlyStats.StatID stat in statModification.Keys.ToList()) {

                GameObject newModifier = Instantiate(modifierPrefab,modifierParent);
                modifiers.Add(newModifier);
                newModifier.GetComponentInChildren<Text>().text = EnumHelper.GetDescription(stat) + ": x" + statModification[stat];
                newModifier.GetComponent<LayoutElement>().preferredHeight = heightStandard * 2 / 3;

            }

        }

    }

    private void setupTraitAdvantages() {

        advantagePanel.gameObject.SetActive(false);

        if (selectiveSurvivabilityAdvantage.Count > 0) {

            advantagePanel.gameObject.SetActive(true);
            advantageTitleLayout.preferredHeight = heightStandard;

            foreach (TraitData.TraitID TID in selectiveSurvivabilityAdvantage.Keys.ToList()) {

                GameObject newAdvantage = Instantiate(advantagePrefab,advantageParent);
                advantages.Add(newAdvantage);
                List<Text> textFields = new List<Text>(newAdvantage.GetComponentsInChildren<Text>());

                textFields[0].text = EnumHelper.GetDescription(TID) + ":";
                textFields[1].text = "\tSurvivability: x" + selectiveSurvivabilityAdvantage[TID];
                textFields[2].text = "\tFitness: x" + selectiveFitnessAdvantage[TID];

                foreach (LayoutElement layoutElement in newAdvantage.GetComponentsInChildren<LayoutElement>()) {
                    layoutElement.preferredHeight = heightStandard * 2 / 3;
                }

            }

        }

    }

    public void destroyTooltip() {

        foreach (GameObject modifier in modifiers) {
            Destroy(modifier);
        }
        modifiers.Clear();

        foreach (GameObject advantage in advantages) {
            Destroy(advantage);
        }
        advantages.Clear();

        this.gameObject.SetActive(false);

        cleaned = true;

    }

}
