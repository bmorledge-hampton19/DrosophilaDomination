using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DataObjectOrganizer<T>: MonoBehaviour where T : DataObject
{

    public GameManager gameManager;

    public List<T> researchObjects;
    public List<T> unityObjects;
    public List<T> conquestObjects;
    public List<T> explorationObjects;

    public Dictionary<GamePhase, List<T>> objectTiers;

    // Use this for initialization
    virtual public void Awake() {

        objectTiers = new Dictionary<GamePhase, List<T>>();

        objectTiers.Add(GamePhase.research, researchObjects);
        objectTiers.Add(GamePhase.unity, unityObjects);
        objectTiers.Add(GamePhase.conquest, conquestObjects);
        objectTiers.Add(GamePhase.exploration, explorationObjects);
            
	}

    void Start()
    {
        foreach(T dataObject in objectTiers[gameManager.getGamePhase()]) 
            if (dataObject.discoveredOnStart) dataObject.discover();
    }

    public List<T> getCurrentObjectTier(bool? discovered = null)
    {

        var objectTier = objectTiers[gameManager.getGamePhase()];

        if (discovered is null)
            return objectTier;
        else if ((bool)discovered)
            return objectTier.Where(dataObject => dataObject.isDiscovered()).ToList();
        else
            return objectTier.Where(dataObject => !dataObject.isDiscovered()).ToList();

    }

    public List<T> getDiscoveredObjects() {
        return getCurrentObjectTier(true);
    }

}
