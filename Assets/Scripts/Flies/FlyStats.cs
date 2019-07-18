using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyStats
{
    
    public enum StatID{

        price = 10,
        strength = 11,
        speed = 12,
        primitiveIntelligence = 13,

    }

    private TraitDB.GamePhase gamePhase;

    public List<StatID> getGamePhaseStats() {

        List<StatID> statsToReturn = new List<StatID>();

        foreach (StatID stat in System.Enum.GetValues(typeof(StatID))) {
            if ((int)stat / 10 == (int)gamePhase) {
                statsToReturn.Add(stat);
            }
        }

        return statsToReturn;

    }

    private Dictionary<StatID,int> stats;

    public int getStat(StatID stat) => stats[stat];
    public void setStat(StatID stat, int value) {stats[stat] = value;}

    public FlyStats(TraitDB.GamePhase gamePhase) {
        stats = new Dictionary<StatID, int>();
        this.gamePhase = gamePhase;
        foreach (StatID stat in System.Enum.GetValues(typeof(StatID))) {
            if ((int)stat / 10 == (int)gamePhase) {
                stats.Add(stat,2);
            }
        }
    }

}
