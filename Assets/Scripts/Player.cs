using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerResource {
    money,
    [Description("Author Citations")]
    authorCitations,
    [Description("Street Cred")]
    streetCred
}

public class Player : MonoBehaviour
{

    public delegate void PlayerResourceChanged(PlayerResource playerResource);
    public event PlayerResourceChanged playerResourceChanged;

    private Dictionary<PlayerResource,float> resourceBank = new Dictionary<PlayerResource, float>();

    public void init()
    {
        
        foreach(PlayerResource playerResource in EnumHelper.GetEnumerable<PlayerResource>())
            resourceBank.Add(playerResource,0);

    }

    public void init(Dictionary<PlayerResource,float> resourceBank) {
        this.resourceBank = resourceBank;
        foreach(PlayerResource playerResource in EnumHelper.GetEnumerable<PlayerResource>())
            if(resourceBank.ContainsKey(playerResource)) this.resourceBank[playerResource] = resourceBank[playerResource];
            else this.resourceBank[playerResource] = 0;
    }

    public void addResource(PlayerResource playerResource, float addThis) {
        resourceBank[playerResource] += addThis;
        if(playerResourceChanged != null) playerResourceChanged(playerResource);
    }

    public bool removeResource(PlayerResource playerResource, float removeThis) {
        if (resourceBank[playerResource] > removeThis) {
            resourceBank[playerResource] -= removeThis;
            if(playerResourceChanged != null) playerResourceChanged(playerResource);
            return true;
        }
        else return false;
    }

    public float getResource(PlayerResource playerResource) { return resourceBank[playerResource]; }

}
