using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyInteractor : MonoBehaviour
{

    public GameObject flyInteractor;

    public DropdownManager dropdownManager;
    public MarkerManager markerManager;
    public SelectionManager selectionManager;
    public FlyReadoutManager mainFlyReadoutManager;

    protected List<Fly> flies;
    protected List<Fly> fliesInView;

    
    void Awake()
    {
        fliesInView = new List<Fly>();
    }

    public void updateFliesInView() {
        
        fliesInView.Clear();

        foreach(Fly fly in flies){
            if (fly.containsTraits(dropdownManager.getSelectedTraits())) fliesInView.Add(fly);
        }

        if (dropdownManager.getSelectedSex() != 0){
            if (dropdownManager.getSelectedSex() == 1) removeFliesBasedOnSex(true);
            else removeFliesBasedOnSex(false);
        }

        if (dropdownManager.getSelectedHybridization() != 0) {
            removeFliesBasedOnHybridization(dropdownManager.getSelectedHybridization());
        }

        if (markerManager != null && markerManager.getSelectedMarkers().Count > 0){
            removeFliesBasedOnMarkers(markerManager.getSelectedMarkers());
        }

        mainFlyReadoutManager.updateReadout(fliesInView);

    }

    public void resetSearch(){
        dropdownManager.resetDropdowns();
        if (markerManager!=null) markerManager.resetMarkers();
        updateFliesInView();
    }

    protected void removeFliesBasedOnSex(bool removeMale){
        List<Fly> fliesToRemove = new List<Fly>();
        foreach(Fly fly in fliesInView) {
            if (removeMale && fly.ismale()) fliesToRemove.Add(fly);
            else if (!removeMale && !fly.ismale()) fliesToRemove.Add(fly);
        }
        foreach(Fly fly in fliesToRemove) fliesInView.Remove(fly);
    }
    protected void removeFliesBasedOnHybridization(int selection){
        // 1 = wild type, 2 = 1 trait, 3 = 2 traits, 4 = 3 traits, 5 = 1-3 traits, 6 = mutts

        List<Fly> fliesToRemove = new List<Fly>();
        foreach(Fly fly in fliesInView) {
            
            switch(selection){
            case 1:
                if (fly.getNumExpressedTraits() != 0) fliesToRemove.Add(fly);
                break;
            case 2:
                if (fly.getNumExpressedTraits() != 1) fliesToRemove.Add(fly);
                break;
            case 3:
                if (fly.getNumExpressedTraits() != 2) fliesToRemove.Add(fly);
                break;
            case 4:
                if (fly.getNumExpressedTraits() != 3) fliesToRemove.Add(fly);
                break;
            case 5:
                if (fly.getNumExpressedTraits() < 1 || fly.getNumExpressedTraits() > 3) fliesToRemove.Add(fly);
                break;
            case 6:
                if (fly.getNumExpressedTraits() < 4) fliesToRemove.Add(fly);
                break;
            }

        }
        foreach(Fly fly in fliesToRemove) fliesInView.Remove(fly);

    }
    protected void removeFliesBasedOnMarkers(List<Fly.Markers> markers){
        List<Fly> fliesToRemove = new List<Fly>();
        foreach(Fly fly in fliesInView) {
            if (!fly.containsMarkers(markers)) fliesToRemove.Add(fly);
        }
        foreach(Fly fly in fliesToRemove) fliesInView.Remove(fly);
    }

    protected void initializeInteractor(List<Fly> flies){

        this.flies = flies;
        flyInteractor.SetActive(true);
        dropdownManager.setUpDropdowns();
        if (markerManager != null) {markerManager.sendPressedMarker += delegate{updateFliesInView();};}
        mainFlyReadoutManager.initializeReadouts(flies,selectionManager);
        resetSearch();

    }

}
