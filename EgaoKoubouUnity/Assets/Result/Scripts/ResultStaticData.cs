using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResultStaticData
{
    public static List<ResultPatientData> _patients = new List<ResultPatientData>();

    public static void Reset()
    {
        _patients.Clear();
    }

    public static void Add(bool inResult, RenderTexture inTexture)
    {
        var data = new ResultPatientData();
        data._result = inResult;
        data._resultTexture = inTexture;
        _patients.Add(data);
    }

    public static ResultPatientData[] GetPatients() => _patients.ToArray();





}

[System.Serializable]
public class ResultPatientData
{
    public bool _result = false;
    public RenderTexture _resultTexture = null;
}
