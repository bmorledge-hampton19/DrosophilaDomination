using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{

    public TraitDB traitDB;

    public List<Dropdown> traitSelectors;
    public Dropdown sexSelector;
    public Dropdown hybridizationSelector;
    private Dictionary<int,TraitData> dropdownToTrait;

    private List<TraitData> selectedTraits;
    private int selectedSex;  // 0 = No selection, 1 = Female, 2 = Male
    private int selectedHybridization; // 0 = Any traits, 1 = wild type, 2 = 1 trait, 3 = 2 traits, 4 = 1 or 2 traits, 5 = mutts

    public List<TraitData> getSelectedTraits() =>selectedTraits;
    public int getSelectedSex() =>selectedSex;
    public int getSelectedHybridization() =>selectedHybridization;

    // Start is called before the first frame update
    void Awake()
    {
        dropdownToTrait = new Dictionary<int, TraitData>();
        selectedTraits = new List<TraitData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUpDropdowns(){

        if (dropdownToTrait.Count != traitDB.getDiscoveredTraits().Count){
            foreach (Dropdown dropdown in traitSelectors) {
                dropdown.ClearOptions();
                dropdown.AddOptions(new List<string>(){"No Trait Selected"});
                //List<Dropdown.OptionData> optionData = new List<Dropdown.OptionData>();
                foreach (TraitData trait in traitDB.getDiscoveredTraits()) {
                    //optionData.Add(new Dropdown.OptionData(trait.name));
                    dropdown.AddOptions(new List<string>(){trait.name});
                }
                //dropdown.AddOptions(optionData);
            }

            int i = 1;
            foreach (TraitData trait in traitDB.getDiscoveredTraits()) {
                dropdownToTrait[i] = trait;
                i++;
            }

            selectedTraits.Clear();
        }

    }

    public void resetDropdowns(){
        foreach (Dropdown traitSelector in traitSelectors) {
            traitSelector.value = 0;
        }
        hybridizationSelector.value = 0;
        sexSelector.value = 0;
    }

    public void updateSelectedTraits() {
        selectedTraits.Clear();
        foreach (Dropdown dropdown in traitSelectors) {
            if (dropdown.value != 0) {
                selectedTraits.Add(dropdownToTrait[dropdown.value]);
            }
        }
    }

    public void updateSelectedSex() {
        selectedSex = sexSelector.value;
    }

    public void updateSelectedHybridization() {
        selectedHybridization = hybridizationSelector.value;
    }

}
