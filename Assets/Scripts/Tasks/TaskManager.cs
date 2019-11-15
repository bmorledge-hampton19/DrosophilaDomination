using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public GrantWriterManager grantWriterManager;

    public GameObject marketButton;
    public GameObject requestsButton;
    public GameObject collosseumButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGrantWriting() {
        grantWriterManager.activate();
    }

    public void unlockBlackMarket() {
        marketButton.SetActive(true);
    }

    public void unlockRequests() {
        requestsButton.SetActive(true);
    }

    public void unlockCollosseum() {
        requestsButton.SetActive(true);
    }

}
