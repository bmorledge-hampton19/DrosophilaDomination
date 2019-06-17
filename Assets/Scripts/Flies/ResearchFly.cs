using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchFly : Fly
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public ResearchFly(ResearchFly maleFly, ResearchFly femaleFly) : base(TraitData.TraitTier.research,maleFly,femaleFly) {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
