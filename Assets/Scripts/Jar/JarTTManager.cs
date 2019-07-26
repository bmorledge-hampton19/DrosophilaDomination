using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class JarTTManager : MonoBehaviour
{

    JarProperty property;

    public Text propertyName;

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

    void Awake() {
        modifiers = new List<GameObject>();
        advantages = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        lockToMouse();

    }

    private void lockToMouse() {

        transform.position = Input.mousePosition;

    }

    public void setupTooltip(JarProperty property) {

        this.gameObject.SetActive(true);

        this.property = property;
        propertyName.text = property.propertyName;

        determineScalingRatio();

        setupGroupedStats();
        setupStatModifiers();
        setupTraitAdvantages();
        lockToMouse();

    }

    private void determineScalingRatio() {

        heightStandard = modifiersAndAdvantages.rect.height/6;

    }

    private void setupGroupedStats() {

        string statText = "";

        statText += "Breeding Speed: x" + property.breedingSpeed + "\n";
        statText += "Fertility: x" + property.fertility + "\n";
        statText += "Survivability: x" + property.survivability + "\n";
        statText += "Capacity: ";
        if (property.carryingCapacity >= 0) statText += "+";
        statText += property.carryingCapacity + "\n";
        statText += "Mutation Rate: x" + property.mutationRate;

        groupedStats.text = statText;

    }

    private void setupStatModifiers() {

        modifierPanel.gameObject.SetActive(false);

        if (property.statModification.Count > 0) {

            modifierPanel.gameObject.SetActive(true);
            modifierTitleLayout.preferredHeight = heightStandard;

            foreach (FlyStats.StatID stat in property.statModification.Keys.ToList()) {

                GameObject newModifier = Instantiate(modifierPrefab,modifierParent);
                newModifier.GetComponentInChildren<Text>().text = EnumHelper.GetDescription(stat) + ": x" + property.statModification[stat];
                newModifier.GetComponent<LayoutElement>().preferredHeight = heightStandard * 2 / 3;

            }

        }

    }

    private void setupTraitAdvantages() {

        advantagePanel.gameObject.SetActive(false);

        if (property.selectiveAdvantageTargets.Count > 0) {

            advantagePanel.gameObject.SetActive(true);
            advantageTitleLayout.preferredHeight = heightStandard;

            foreach (TraitData.TraitID TID in property.selectiveAdvantageTargets) {

                GameObject newAdvantage = Instantiate(advantagePrefab,advantageParent);
                List<Text> textFields = new List<Text>(newAdvantage.GetComponentsInChildren<Text>());

                textFields[0].text = EnumHelper.GetDescription(TID) + ":";
                textFields[1].text = "\tSurvivability: x" + property.selectiveSurvivabilityAdvantage[TID];
                textFields[2].text = "\tFitness: x" + property.selectiveFitnessAdvantage[TID];

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

    }

}
