using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyReadout : MonoBehaviour
{

    public GameObject flyReadout;
    public Fly fly;

    public MarkerManager markerManager;

    public Text sext;
    public Text traitText;
    public Toggle toggle;

    public void setFly(Fly fly){

        this.fly = fly;

        foreach (Fly.Markers marker in fly.getMarkers()){
            markerManager.toggleSelectively(marker,true);
        }

        if (fly.ismale()) sext.text = "M";
        else sext.text = "F";

        string traitNames = "";
        foreach (TraitData trait in fly.getExpressedTraits()){
            traitNames += (trait.name + ", ");
        }
        char[] charsToTrim = {',',' '};
        traitNames = traitNames.TrimEnd(charsToTrim);
        if (fly.getExpressedTraits().Count == 0) traitNames = "Wild Type";
        traitText.text = traitNames;

        markerManager.sendPressedMarker += updateFlyMarker;

    }

    public void setSelectionManager(SelectionManager selectionManager) {

        toggle.onValueChanged.AddListener(delegate{ selectionManager.updateSelectedFlies(toggle.isOn,this); });

    }

    public void updateFlyMarker(Fly.Markers marker) {
        if (markerManager.getSelectedMarkers().Contains(marker)) {
            fly.addMarker(marker);
        } else {
            fly.removeMarker(marker);
        }
    }

    public void hide(){
        flyReadout.SetActive(false);
    }

    public void show(){
        flyReadout.SetActive(true);
    }

    public void destroy(){
        Destroy(flyReadout);
    }

    public bool isSelected(){
        return toggle.isOn;
    }

    public void activateToggle(){toggle.isOn = true;}
    public void deactivateToggle(){toggle.isOn = false;}

}