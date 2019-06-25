using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MarkerManager : MonoBehaviour
{
    
    private List<Fly.Markers> selectedMarkers;

    public Sprite emptyButton;
    public Sprite filledButton;

    public List<Fly.Markers> getSelectedMarkers()=>selectedMarkers;

    // Start is called before the first frame update
    void Start()
    {
        selectedMarkers = new List<Fly.Markers>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void redMarkerPressed(){
        toggleMarker(Fly.Markers.red);
    }
    public void greenMarkerPressed(){
        toggleMarker(Fly.Markers.green);
    }
    public void blueMarkerPressed(){
        toggleMarker(Fly.Markers.blue);
    }
    public void yellowMarkerPressed(){
        toggleMarker(Fly.Markers.yellow);
    }
    public void purpleMarkerPressed(){
        toggleMarker(Fly.Markers.purple);
    }
    public void silverMarkerPressed(){
        toggleMarker(Fly.Markers.silver);
    }
    public void goldMarkerPressed(){
        toggleMarker(Fly.Markers.gold);
    }

    private void toggleMarker(Fly.Markers marker){
        if (selectedMarkers.Contains(marker)) {
            selectedMarkers.Remove(marker);
        } else selectedMarkers.Add(marker);
    }

    public void changeButtonImage(){
        Image buttonImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        if (buttonImage.sprite == emptyButton) {
            buttonImage.sprite = filledButton;
        } else if (buttonImage.sprite == filledButton) {
            buttonImage.sprite = emptyButton;
        } else {
            Debug.Log("The marker button sprites aren't matching... Darn it.");
        }
    }

}
