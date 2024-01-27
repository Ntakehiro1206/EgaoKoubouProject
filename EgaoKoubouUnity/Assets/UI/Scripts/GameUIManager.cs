using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    private CounselingSheetUI sheetUI;

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


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //sheetUI.AddItem("gfh");
            if (Input.GetKey(KeyCode.A))
            {
                sheetUI.AddItem(locRequstNames[0]);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                sheetUI.AddItem(locRequstNames[1]);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                sheetUI.AddItem(locRequstNames[2]);
            }
            else
            {
                sheetUI.AddItem("");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            sheetUI.DeletionItem();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sheetUI.Check(true);
        }
    }
}
