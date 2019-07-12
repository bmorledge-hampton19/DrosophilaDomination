using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlyReadoutManager : MonoBehaviour
{

    public Transform flyReadoutPanel;
    public GameObject flyReadoutPrefab;

    private List<FlyReadout> activeReadouts;
    public List<FlyReadout> getActiveReadouts()=>activeReadouts;
    private Dictionary<Fly,FlyReadout> flyReadouts;
    public Dictionary<Fly,FlyReadout> getFlyReadouts()=> flyReadouts;

    // Start is called before the first frame update
    void Awake()
    {
        
        flyReadouts = new Dictionary<Fly, FlyReadout>();
        activeReadouts = new List<FlyReadout>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateReadout(List<Fly> flies) {

        foreach(FlyReadout flyReadout in activeReadouts){
            flyReadout.hide();
        }

        activeReadouts.Clear();

        foreach (Fly fly in flies) {
            flyReadouts[fly].show();
            activeReadouts.Add(flyReadouts[fly]);
        }

    }

    public void initializeReadouts(List<Fly> flies, SelectionManager selectionManager) {

        foreach (Fly fly in flies) {
            if (!flyReadouts.ContainsKey(fly)) addFlyReadout(fly, selectionManager);
        }

    }

    public void addFlyReadout(Fly fly, SelectionManager selectionManager){

        GameObject newReadout = Instantiate(flyReadoutPrefab,flyReadoutPanel);
        newReadout.SetActive(true);
        newReadout.GetComponent<FlyReadout>().setFly(fly);
        if (selectionManager != null) newReadout.GetComponent<FlyReadout>().setSelectionManager(selectionManager);
        flyReadouts.Add(fly,newReadout.GetComponent<FlyReadout>());

    }

    public void deactivateMarkerButtons(){
        foreach (FlyReadout flyReadout in flyReadouts.Values.ToList()) {
            flyReadout.markerManager.disableToggle();
        }
    }

    public void deleteReadouts() {
        activeReadouts.Clear();
        foreach (FlyReadout flyReadout in flyReadouts.Values.ToList()){
            flyReadout.destroy();
        }
        flyReadouts.Clear();
    }

    public void deleteReadouts(List<Fly> flies){
        
        foreach (Fly fly in flies) {
            activeReadouts.Remove(flyReadouts[fly]);
            flyReadouts[fly].destroy();
            flyReadouts.Remove(fly);
        }

    }

    public void toggleAll(List<Fly> flies) {
        List<Fly> fliesCopy = new List<Fly>(flies);
        foreach (Fly fly in fliesCopy) {
            flyReadouts[fly].deactivateToggle();
        }
    }

}
