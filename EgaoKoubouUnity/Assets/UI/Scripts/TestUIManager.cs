using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIManager : MonoBehaviour
{
    private CounselingSheetUI sheetUI;
    private EmotionWindowUI emotionUI;
    private MoneyUI MoneyUI;

    private string[] locRequstNames = new string[]
    {
        "A",
        "B",
        "C",
    };

    // Start is called before the first frame update
    void Start()
    {
        sheetUI = GetComponentInChildren<CounselingSheetUI>();
        emotionUI = GetComponentInChildren<EmotionWindowUI>();
        MoneyUI = GetComponentInChildren<MoneyUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoneyUI.UseMoney(1);
        }

        if (Input.GetMouseButtonDown(1))
        {
            sheetUI.DeletionItem();
        }

        if (Input.GetMouseButtonDown(1))
        {

            if (Input.GetKey(KeyCode.A))
            {
                emotionUI.SetEmotion(0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                emotionUI.SetEmotion(1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                emotionUI.SetEmotion(2);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sheetUI.Check(true);
        }

        if (Input.GetKey(KeyCode.S))
        {
        }
    }
}
