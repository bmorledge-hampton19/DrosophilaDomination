using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObjectOrganizer : MonoBehaviour
{

    public GameManager gameManager;

    public List<DataObject> researchObjects;
    public List<DataObject> unityObjects;
    public List<DataObject> conquestObjects;
    public List<DataObject> explorationObjects;

    protected Dictionary<GamePhase,List<DataObject>> discoveredObjects;

    public Dictionary<GamePhase, List<DataObject>> objectTiers;

    // Use this for initialization
    public void Awake() {

        objectTiers = new Dictionary<GamePhase, List<DataObject>>();

        objectTiers.Add(GamePhase.research, researchObjects);
        objectTiers.Add(GamePhase.unity, unityObjects);
        objectTiers.Add(GamePhase.conquest, conquestObjects);
        objectTiers.Add(GamePhase.exploration, explorationObjects);

        discoveredObjects = new Dictionary<GamePhase, List<DataObject>>();
        discoveredObjects[GamePhase.research] = new List<DataObject>();
        discoveredObjects[GamePhase.unity] = new List<DataObject>();
        discoveredObjects[GamePhase.conquest] = new List<DataObject>();
        discoveredObjects[GamePhase.exploration] = new List<DataObject>();

        foreach(DataObject dataObject in researchObjects) 
            if (dataObject.discoveredOnStart) discoverObject(dataObject);
        foreach(DataObject dataObject in unityObjects) 
            if (dataObject.discoveredOnStart) discoverObject(dataObject);
        foreach(DataObject dataObject in conquestObjects) 
            if (dataObject.discoveredOnStart) discoverObject(dataObject);
        foreach(DataObject dataObject in explorationObjects) 
            if (dataObject.discoveredOnStart) discoverObject(dataObject);
            
	}

    public List<DataObject> getCurrentObjectTier()
    {
        return objectTiers[gameManager.getGamePhase()];
    }

    public void discoverObject(DataObject dataObject) {
        discoveredObjects[dataObject.gamePhase].Add(dataObject);
    }

    public List<DataObject> getDiscoveredObjects() {
        return discoveredObjects[gameManager.getGamePhase()];
    }

}
