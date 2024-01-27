using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static EmotionDatatable;
using Unity.VisualScripting;

public class CounselingItemUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject checkMark;

    private IPatientRequestStatus _requestData = null;

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
        if (!checkMark.activeSelf)
        {
            if (_requestData.myResult)
            {
                Check(true);
            }
        }
    }

    public void SetValue(string inText)
    {
        text.text = inText;
    }

    public void Check(bool inCheck)
    {
        checkMark.SetActive(inCheck);
    }

    public void SetPatientStatus(IPatientRequestStatus inStatus )
    {
        _requestData = inStatus;
        SetValue(inStatus.mySource._comment);
        Check(false);
    }
}
