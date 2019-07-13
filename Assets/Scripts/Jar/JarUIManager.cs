using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarUIManager : MonoBehaviour
{
    public GameObject jarUI;

    public JarActionButton jarActionButton;

    public RectTransform progressBarImage;
    public Text progressText;
    public Text statusText;

    public void setUpProgressBar(ProgressBar progressBar){
        progressBar.fillImage = progressBarImage;
        progressBar.progressText = progressText;
        progressBar.statusText = statusText;
        progressBar.initFillImage();
    }

    public void disableUI(){
        jarUI.SetActive(false);
    }

    public void enableUI(){
        jarUI.SetActive(true);
    }

}
