using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[CreateAssetMenu(menuName = "ScriptableObject/PatientTable", fileName = "PatientTable")]
public class PatientDatatable : ScriptableObject
{
    [SerializeField]
    private FaceDatatable _faceDatatable = default;
    

    
}
