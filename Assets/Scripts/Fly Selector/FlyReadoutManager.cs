using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyReadoutManager : MonoBehaviour
{

    private Dictionary<Fly,FlyReadout> flyReadouts;

    // Start is called before the first frame update
    void Start()
    {
        
        flyReadouts = new Dictionary<Fly, FlyReadout>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
