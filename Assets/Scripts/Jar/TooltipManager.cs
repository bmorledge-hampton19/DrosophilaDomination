using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{

    private GameObject jarTooltip;
    private JarProperty jarProperty;

    private float timeCursorEntered = 0;

    public void setup(GameObject jarTooltip) {
        this.jarTooltip = jarTooltip;
    }

    public void changeProperty(JarProperty jarProperty) {
        this.jarProperty = jarProperty;
    }

    public void cursorEntered() {
        timeCursorEntered = Time.time;
        Debug.Log("Cursor Entered");
    }

    public void cursorExited() {
        timeCursorEntered = 0;
        disableTooltip();
        Debug.Log("Cursor Exited");
    }

    void Update() {

        if (timeCursorEntered > 0 && Time.time > timeCursorEntered + 1.5f) {
            timeCursorEntered = 0;
            Debug.Log("Enabling Tooltip...");
            enableTooltip();
        }

    }
    
    private void enableTooltip() {
        jarTooltip.GetComponent<JarTTManager>().setupTooltip(jarProperty);
    }

    private void disableTooltip() {
        jarTooltip.GetComponent<JarTTManager>().destroyTooltip();
    }

}
