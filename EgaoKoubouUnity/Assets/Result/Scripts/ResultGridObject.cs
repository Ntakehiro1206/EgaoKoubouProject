using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        for (int i = 0; i < _resultGridObjects.Length; i++)
        {
            if (i < inResults.Length)
            {
                _resultGridObjects[i].SetResult(inResults[i]);
            }
        }
    }

    public List<ResultPatientObject> GetMissingPatients()
    {
        return _resultGridObjects.Where(value => value.IsMissing()).ToList();
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
