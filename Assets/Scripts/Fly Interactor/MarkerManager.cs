using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MarkerManager : MonoBehaviour
{
    
    private List<Fly.Markers> selectedMarkers;
    public List<Fly.Markers> getSelectedMarkers()=>selectedMarkers;
    private bool toggleable = true;
    public void disableToggle(){toggleable = false;}

    public Sprite emptyButton;
    public Sprite filledButton;

    public List<GameObject> markerButtons;
    public Dictionary<Fly.Markers,GameObject> markerToButtons;
    public Dictionary<GameObject,Fly.Markers> buttonToMarkers;

    public delegate void markerManagerDelegate(Fly.Markers marker);
    public event markerManagerDelegate sendPressedMarker;

    void Awake()
    {
        selectedMarkers = new List<Fly.Markers>();
        markerToButtons = new Dictionary<Fly.Markers, GameObject>();
        buttonToMarkers = new Dictionary<GameObject, Fly.Markers>();

        initDictionaries();

    }

    private void initDictionaries(){

        for(int i = 1; i < 8; i++) {
            markerToButtons.Add((Fly.Markers)i, markerButtons[i-1]);
            buttonToMarkers.Add(markerButtons[i-1], (Fly.Markers)i);
        }

    }

    private void toggleMarker(Fly.Markers marker){
        if (selectedMarkers.Contains(marker)) {
            selectedMarkers.Remove(marker);
        } else selectedMarkers.Add(marker);
    }

    private void changeButtonImage(GameObject markerButton){
        Image buttonImage = markerButton.GetComponent<Image>();
        if (buttonImage.sprite == emptyButton) {
            buttonImage.sprite = filledButton;
        } else if (buttonImage.sprite == filledButton) {
            buttonImage.sprite = emptyButton;
        } else {
            Debug.Log("The marker button sprites aren't matching... Darn it.");
        }
    }

    public void buttonPressed() {

        GameObject markerButton = EventSystem.current.currentSelectedGameObject;

        if (toggleable) {
            changeButtonImage(markerButton);
            toggleMarker(buttonToMarkers[markerButton]);
        }

        if (sendPressedMarker != null) sendPressedMarker(buttonToMarkers[markerButton]);
    }

    public void toggleSelectively(Fly.Markers marker, bool enable){

        if (selectedMarkers.Contains(marker) != enable) {
            changeButtonImage(markerToButtons[marker]);
            toggleMarker(marker);
            if (sendPressedMarker != null) sendPressedMarker(marker);
        }

    }

    public void resetMarkers(){
        selectedMarkers.Clear();
        foreach (GameObject markerButton in markerButtons) {
            markerButton.GetComponent<Image>().sprite = emptyButton;
        }
    }

}
