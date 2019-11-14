using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public enum PlayerResource {
        money,
        authorCitations,
        streetCred
    }

    private Dictionary<PlayerResource,float> resourceBank;

    // Start is called before the first frame update
    public void init()
    {
        
        foreach(PlayerResource playerResource in EnumHelper.GetEnumerable<PlayerResource>())
            resourceBank.Add(playerResource,0);

    }

    public void init(Dictionary<PlayerResource,float> resourceBank) {
        this.resourceBank = resourceBank;
    }

    public void addResource(PlayerResource playerResource, float addThis) {
        resourceBank[playerResource] += addThis;
    }

    public bool removeResource(PlayerResource playerResource, float removeThis) {
        if (resourceBank[playerResource] > removeThis) {
            resourceBank[playerResource] -= removeThis;
            return true;
        }
        else return false;
    }

    public float getResource(PlayerResource playerResource) { return resourceBank[playerResource]; }

}
