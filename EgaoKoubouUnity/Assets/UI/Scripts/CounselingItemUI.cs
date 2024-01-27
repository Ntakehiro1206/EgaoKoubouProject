using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounselingItemUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject checkMark;

    void Start()
    {
        //checkMark.SetActive(false);
    }
    // Start is called before the first frame update
    void Awake()
    {
        checkMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(string inText)
    {
        text.text = inText;
    }

    public void Check(bool inCheck)
    {
        checkMark.SetActive(inCheck);
    }
}
