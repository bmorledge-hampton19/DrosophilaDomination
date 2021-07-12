using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObject : ScriptableObject
{
    
    public string objectName;
    public GamePhase gamePhase;
    public bool discoveredOnStart;
    private bool discovered = false;

    public bool isDiscovered() {return discovered;}
    public void discover() {discovered = true;}

}