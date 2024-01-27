using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatientType { Human, Cat, }

[System.Serializable]
public class PatientBasicData
{
    public PatientType _patientType = default;
    public Material _faceBG = default;
}

public class PatientData
{
    public PatientBasicData   _basic = default;
    public Face.FaceGroupData _face  = default;
}



[CreateAssetMenu(menuName = "ScriptableObject/PatientTable", fileName = "PatientTable")]
public class PatientDatatable : ScriptableObject
{
    [SerializeField]
    private FaceDatatable _faceDatatable = default;
    [SerializeField]
    private PatientBasicData[] _patiets = default;

    public int GetPatientSize() => _patiets.Length;

    public PatientData GetPatientData(Cosmetic.SurgeryGroupData inSurgery)
    {
        PatientData data = new PatientData();
        return data;
    }
}
