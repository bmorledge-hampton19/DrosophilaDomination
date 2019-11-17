using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum FunctionType{

    none = 0,
    unlock = 1,
    increase = 2

}

public enum UnlockType {

    none = 0,
    jarProperty = 1,
    task = 2,
    jar = 3

}

public enum TaskType {

    none = 0,
    blackMarket = 1,
    requests = 2,
    collosseum = 3


}

public enum IncreaseType {

    none = 0,
    grantPayout = 1

}

public enum IncreaseFunction {
    add = 0,
    multiply = 1

}

[System.Serializable]
public class UpgradeFunction
{ 
    

    public FunctionType functionType;
        public UnlockType unlockType;
            public TaskType taskType;
        public JarProperty jarProperty;
        
        public IncreaseType increaseType;
        public IncreaseFunction increaseFunction;
        public float increaseValue;

}