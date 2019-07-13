using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyStorageManager : FlyInteractor
{
    
    public MarkerManager enableAll,disableAll;
    public Text totalText;

    public Storage storage;

    private bool storageSetUp = false;

    void Awake()
    {
        
        fliesInView = new List<Fly>();

        initMarkerManagers();

    }

    void Start(){

        setUpStorage();


    }

    void OnEnable() {

        if (storageSetUp) updateStorage();

    }

    private void initMarkerManagers(){

        enableAll.disableToggle();
        disableAll.disableToggle();

        enableAll.sendPressedMarker += enableAllMarkers;
        disableAll.sendPressedMarker += disableAllMarkers;

    }

    public void grossToggleMarkers(Fly.Markers marker, bool enable){

        foreach( FlyReadout readout in mainFlyReadoutManager.getActiveReadouts()){
            readout.markerManager.toggleSelectively(marker,enable);
        }

    }

    public void enableAllMarkers(Fly.Markers marker){grossToggleMarkers(marker,true);}
    public void disableAllMarkers(Fly.Markers marker){grossToggleMarkers(marker,false);}

    private void setUpStorage(){

        initializeInteractor(storage.getFlies());

        selectionManager.updateMinMax(1,int.MaxValue);
        totalText.text = "Total: " + storage.getFlies().Count;

        storageSetUp = true;

    }
    
    public void updateStorage(){

        mainFlyReadoutManager.initializeReadouts(storage.getFlies(),selectionManager);

        selectionManager.updateMinMax(1,int.MaxValue);
        selectionManager.clearSelectedFlies();
        totalText.text = "Total: " + storage.getFlies().Count;

        dropdownManager.setUpDropdowns();

        updateFliesInView();

    }

    public void discardFlies(){

        List<Fly> fliesToDelete = selectionManager.getSelectedFlies();

        mainFlyReadoutManager.deleteReadouts(fliesToDelete);
        storage.removeFlies(fliesToDelete);

        selectionManager.clearSelectedFlies();

        totalText.text = "Total: " + storage.getFlies().Count;

        updateFliesInView();

    }

}
