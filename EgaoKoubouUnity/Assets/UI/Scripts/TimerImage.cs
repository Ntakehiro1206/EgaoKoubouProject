using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerImage : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
       
    }

    public void SetAmount(float inValue)
    {
        image.fillAmount = inValue;
    }
}
