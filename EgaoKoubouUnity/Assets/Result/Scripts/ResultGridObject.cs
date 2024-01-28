using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGridObject : MonoBehaviour
{
    private ResultPatientObject[] _resultGridObjects;


    private void Awake()
    {
        _resultGridObjects = GetComponentsInChildren<ResultPatientObject>(true);
        SetVisible(false);
    }
    void Start()
    {
        
    }

    public void SetVisible(bool inVisible)
    {
        foreach (var obj in _resultGridObjects)
        {
            obj.SetVisible(inVisible);
        }
    }

    public void SetResult(ResultPatientData[] inResults)
    {
        int size = _resultGridObjects.Length > inResults.Length ? inResults.Length : _resultGridObjects.Length;
        for (int i = 0; i < _resultGridObjects.Length; i++)
        {
            


        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
