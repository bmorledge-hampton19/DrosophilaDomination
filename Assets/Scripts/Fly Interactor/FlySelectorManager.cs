using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlySelectorManager : FlyInteractor
{

    public Text flyDestinationText;
    public Text minFliesText;
    public Text maxFliesText;

    public delegate void fliesSelected(List<Fly> selectedFlies);
    public event fliesSelected sendFlies;
    public Storage flyStorage;

    public void setUpSelector(string flyDestination, int minFlies, int maxFlies){

        initializeInteractor(flyStorage.getFlies());

        mainFlyReadoutManager.deactivateMarkerButtons();

        flyDestinationText.text = "Destination: " + flyDestination;
        minFliesText.text = "Min: " + minFlies;
        maxFliesText.text = "Max: " + maxFlies;

        selectionManager.updateMinMax(minFlies,maxFlies);
        selectionManager.clearSelectedFlies();

    }
    public void selectionFinished(){
        mainFlyReadoutManager.deleteReadouts(selectionManager.getSelectedFlies());
        flyInteractor.SetActive(false);
        sendFlies(selectionManager.getSelectedFlies());
    }

    public void cancelSelection(){
        mainFlyReadoutManager.toggleAll(selectionManager.getSelectedFlies());
        selectionManager.clearSelectedFlies();
        selectionFinished();
    }

}
