using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    public GameObject OptionUI;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }
    public void OnClick()
    {
        OptionUI.SetActive(true);

        button.interactable = false;
    }
}
