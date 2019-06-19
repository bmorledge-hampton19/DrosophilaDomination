using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    private bool active;

    private float fillRate;
    public float FillRate { get => fillRate; set => fillRate = value; }

    private float currentFill;
    public float CurrentFill { get => currentFill; set => currentFill = value; }
    private RectTransform fillImage; 
    private float barWidth;   

    // Start is called before the first frame update
    void Start()
    {
        currentFill = 0;
        fillRate = 0.005f;
        active = true;

        fillImage = transform.Find("Loading Bar Frame/Fill Frame/Fill") as RectTransform;
        RectTransform fillImageParent = fillImage.parent as RectTransform;
        barWidth = fillImageParent.rect.width;
        Debug.Log("Progress Bar Width: " + barWidth);
        
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
            }
            setImageFill();
        }

    }

    private void setImageFill(){
        Debug.Log("Fill bar being set to size: " + (barWidth*currentFill));
        fillImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,barWidth*currentFill);
    }

}
