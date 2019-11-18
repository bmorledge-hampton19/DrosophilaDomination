using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class FlyStats
{
    
    public enum StatID{

        [Description("None")]
        none = 0,
        [Description("Price")]
        price = 10,
        [Description("Strength")]
        strength = 11,
        [Description("Speed")]
        speed = 12,
        [Description("Intelligence")]
        primitiveIntelligence = 13,

    }

    private GamePhase gamePhase;

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

    public FlyStats(GamePhase gamePhase) {
        stats = new Dictionary<StatID, int>();
        this.gamePhase = gamePhase;
        foreach (StatID stat in System.Enum.GetValues(typeof(StatID))) {
            if ((int)stat / 10 == (int)gamePhase) {
                stats.Add(stat,2);
            }
        }
    }

}

[System.Serializable]
public class SingleStat {

    public FlyStats.StatID stat;
    public int value;

}