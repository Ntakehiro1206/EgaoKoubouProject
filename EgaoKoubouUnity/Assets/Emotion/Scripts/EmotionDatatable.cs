using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EmotionPositonType
{
    faceOutline,
    eye_L1,
    eye_R1,
    eyebrow_L1,
    eyebrow_R1,
    mouth,
    nose,
    hair,
    body,
}

[CreateAssetMenu(menuName = "ScriptableObject/EmotionDatatable", fileName = "EmotionDatatable")]
public class EmotionDatatable : ScriptableObject
{
    [SerializeField]
    private ResuestData[]  _requests = default;



    [System.Serializable]
    public class ResuestData
    {
        public string _comment = "";
        public EmotionPositonType _type = default;
        public Vector3[] _positions = default;
    }
}
