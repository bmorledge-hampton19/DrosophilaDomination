using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlySelectorManager : MonoBehaviour
{

    public GameObject flySelector;

    public List<Dropdown> traitSelectors;
    private Dictionary<int,TraitData.TraitID> dropdownToTrait;
    private List<TraitData.TraitID> selectedTraits;

    

    public delegate void fliesSelected(List<Fly> selectedFlies);
    public event fliesSelected sendFlies;
    public Storage flyStorage;
    public TraitDB traitDB;
    private List<Fly> selectedFlies;

    string flydestination;
    int minFlies;
    int maxFlies;

    // Start is called before the first frame update
    void Start()
    {
        
        dropdownToTrait = new Dictionary<int, TraitData.TraitID>();
        selectedTraits = new List<TraitData.TraitID>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setUpDropdowns(){

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
            dropdownToTrait[i] = trait.TID;
            i++;
        }

        selectedTraits.Clear();

    }

    public void updateSelectedTraits() {
        selectedTraits.Clear();
        foreach (Dropdown dropdown in traitSelectors) {
            if (dropdown.value != 0) {
                selectedTraits.Add(dropdownToTrait[dropdown.value]);
            }
        }
        
    }

    public void setUpSelector(string flyDestination, int minFlies, int maxFlies){
        this.flydestination = flyDestination; 
        this.minFlies = minFlies; 
        this.maxFlies = maxFlies;
        
        flySelector.SetActive(true);
    }
    public void selectionFinished(){
        flySelector.SetActive(false);
        sendFlies(selectedFlies);
    }

}
