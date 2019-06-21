using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewTraitDB", menuName = "Genetics/TraitDB", order = 1)]
public class TraitDB : ScriptableObject {

    public List<TraitData> researchTraits;
    public List<TraitData> unityTraits;
    public List<TraitData> conquestTraits;
    public List<TraitData> explorationTraits;

    public enum GamePhase{
        research = 1,
        unity = 2,
        conquest = 3,
        exploration = 4
    }
    private GamePhase gamePhase;
    public GamePhase getGamePhase()=>gamePhase;
    public void advanceGamePhase(){gamePhase++;}

    public Dictionary<GamePhase, List<TraitData>> traitTiers;

    // Use this for initialization
    void Start () {

        traitTiers.Add(GamePhase.research, researchTraits);
        traitTiers.Add(GamePhase.unity, unityTraits);
        traitTiers.Add(GamePhase.conquest, conquestTraits);
        traitTiers.Add(GamePhase.exploration, explorationTraits);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<TraitData> getCurrentTraitTier()
    {
        return traitTiers[gamePhase];
    }

    public List<TraitData> getDiscoveredTraits(){
        List<TraitData> traitsToReturn = new List<TraitData>();
        foreach(TraitData trait in traitTiers[gamePhase]){
            if (trait.discovered) traitsToReturn.Add(trait);
        }
        return traitsToReturn;
    }

}
