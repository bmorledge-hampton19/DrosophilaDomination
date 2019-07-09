using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public delegate void whenFilled();
    public event whenFilled fillActions;

    private bool active;
    public bool finished()=>!active;

    private float fillRate;
    public float FillRate { get => fillRate; set => fillRate = value; }

    private float currentFill;
    public float CurrentFill { get => currentFill; set => currentFill = value; }
    public RectTransform fillImage; 
    public Text progressText;
    public Text statusText;
    private float barWidth;   

    // Start is called before the first frame update
    void Start()
    {
        currentFill = 1;
        active = false;

        RectTransform fillImageParent = fillImage.parent as RectTransform;
        barWidth = fillImageParent.rect.width;
        
        setImageFill();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (active) {
            currentFill += fillRate;
            if (currentFill > 1){
                currentFill = 1;
                active = false;
                statusText.text = "Done!";
                if (fillActions!=null) fillActions();
            }
            setImageFill();
        }

    }

    public void activate(float fillRate){
        currentFill = 0;
        statusText.text = "Breeding...";
        this.fillRate = fillRate;
        active = true;
    }

    public void deactivate(){
        active = false;
        currentFill = 1;
        statusText.text = "Done!";
        setImageFill();
    }

    private void setImageFill(){
        fillImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,barWidth*currentFill);
        progressText.text = ((int)(CurrentFill*100)).ToString() + "%";
    }

}