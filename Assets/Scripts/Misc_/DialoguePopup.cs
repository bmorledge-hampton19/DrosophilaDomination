using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialoguePopup : MonoBehaviour
{

    public GameObject inputBlocker;
    public GameObject dialoguePopup;
    public Text popupText;
    public Button yesButton;
    public Button noButton;

    private void resetButtons(){

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(delegate{inputBlocker.SetActive(false);});
        noButton.onClick.AddListener(delegate{inputBlocker.SetActive(false);});

        yesButton.onClick.AddListener(delegate{dialoguePopup.SetActive(false);});
        noButton.onClick.AddListener(delegate{dialoguePopup.SetActive(false);});

    }

    public void setup(string text, UnityAction yesOption, UnityAction noOption){

        resetButtons();

        inputBlocker.SetActive(true);
        dialoguePopup.SetActive(true);

        popupText.text = text;
        yesButton.onClick.AddListener(yesOption);
        noButton.onClick.AddListener(noOption);

    }

    public void setup(string text, UnityAction yesOption){

        resetButtons();

        inputBlocker.SetActive(true);
        dialoguePopup.SetActive(true);

        popupText.text = text;
        yesButton.onClick.AddListener(yesOption);

    }

}
