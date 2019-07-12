using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class AddFliesManager : FlyInteractor
{

    public FlyReadoutManager parentsReadoutManager;

    public delegate void markersSet();
    public event markersSet addFlies;

    public MarkerManager enableAll,disableAll;

    void Awake()
    {
        
        fliesInView = new List<Fly>();

        initMarkerManagers();

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

    public void setUpAdder(List<Fly> progeny, List<Fly> parents){

        initializeInteractor(progeny);

        foreach (Fly fly in parents){
            parentsReadoutManager.initializeReadouts(parents, null);
        }
        parentsReadoutManager.deactivateMarkerButtons();

    }
    


    public void finishedMarking(){

        Dictionary<Fly,FlyReadout> flyReadouts = mainFlyReadoutManager.getFlyReadouts();

        foreach (Fly fly in flyReadouts.Keys.ToList()){
            foreach (Fly.Markers marker in flyReadouts[fly].markerManager.getSelectedMarkers()) {
                fly.addMarker(marker);
            }
        }

        mainFlyReadoutManager.deleteReadouts();
        parentsReadoutManager.deleteReadouts();

        flyInteractor.SetActive(false);
        addFlies();

    }

}
