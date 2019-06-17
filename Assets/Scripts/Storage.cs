using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{

    private List<Fly> flies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Storage(){
        flies = new List<Fly>();
    }

    public List<Fly> getFlies(List<TraitData> traits) {
        List<Fly> fliesToReturn = new List<Fly>();
        foreach(Fly fly in flies){
            if (fly.containsTraits(traits)) fliesToReturn.Add(fly);
        }
        return fliesToReturn;
    }

    public void addFlies(List<Fly> flies){
        this.flies.AddRange(flies);
    }

    public bool removeFlies(List<TraitData> traits, int count) {

        int fliesFound = 0;
        List<Fly> fliesToRemove = new List<Fly>();

        foreach (Fly fly in flies) {
            if (fly.hasSameTraits(traits)) {
                fliesFound++;
                fliesToRemove.Add(fly);
                if (fliesFound == count) {
                    removeFlies(fliesToRemove);
                    return true;
                }
            }
        }

        return false;

    }

    public void removeFlies(List<Fly> fliesToRemove){
        foreach (Fly fly in fliesToRemove) {
                flies.Remove(fly);
        }
    }

}
