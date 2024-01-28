using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCloseButton : MonoBehaviour
{
    public GameObject OptionUI;

    public Button optionButton;

    void Start()
    {
        // optionButton = GetComponent<Button>();
    }

    public void OnClick()
    {
        OptionUI.SetActive(false);

        optionButton.interactable = true;
    }
}
