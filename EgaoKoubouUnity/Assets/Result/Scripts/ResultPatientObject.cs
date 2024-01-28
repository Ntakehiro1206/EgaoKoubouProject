using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPatientObject : MonoBehaviour
{
    [SerializeField]
    private RawImage _rawIamage = null;

    private ResultPatientData _patientData = null;

    public void SetVisible(bool inVisible)
    {
        gameObject.SetActive(inVisible);
    }

    public void SetResult(ResultPatientData inData)
    {
        _patientData = inData;

        _rawIamage.gameObject.SetActive(false);
        _rawIamage.texture = inData._resultTexture;
    }



    void Start()
    {
        _rawIamage.gameObject.SetActive(false);
    }


    void Update()
    {
        
    }

    public void Set()
    {

    }
}
