using Face;
using Cosmetic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Face
{
    public enum EyeType { Upswept, Droopy }
    public enum PartType { Eye, Eyebrow, Nose, Mouse, Hair }

    [System.Serializable]
    public class EyeData
    {
        public EyeType  _eyeType = default;
        public Part     _left  = default;
        public Part     _right = default;

        [System.Serializable]
        public class Part
        {
            public Material _material = default;
            public Vector2  _position = Vector2.zero;
        }
    }

    [System.Serializable]
    public class NoseData
    {
        public Material _material = default;
        public Vector2  _position = Vector2.zero;
    }
    [System.Serializable]
    public class MouseData
    {
        public Vector2  _position = Vector2.zero;
    }
    [System.Serializable]
    public class HairData
    {
        public Material _material = default;
        public Color    _color    = default;
    }

    public class FaceGroupData
    {
        public EyeData _eye;
        public EyeData _eyebrow;
        public NoseData _nose;
        public MouseData _mouse;
        public HairData _hair;
    }
}

namespace Cosmetic
{
    [System.Serializable]
    public class SurgeryData
    {
        public string        _comment      = default;
        public Face.PartType _facePartType = default;
    }

    [System.Serializable]
    public class SurgeryGroupData
    {
        public int[] _surgerIndexList = default;
        public int[] _difficulty      = default;
        public int   _money           = 0;

        public bool InRange(int inIndex)
        {
            if (_difficulty.Length == 0)
                return true;
            if (_difficulty.Length == 1)
                return inIndex <= _difficulty[0];
            return inIndex >= _difficulty[0] && inIndex <= _difficulty[1];
        }
    }





}

[CreateAssetMenu(menuName = "ScriptableObject/FaceDatatable", fileName = "FaceDatatable")]
public class FaceDatatable : ScriptableObject
{
    [Header("顔パーツ情報")]
    [SerializeField]
    private EyeData[]   _eyeList     = default;
    [SerializeField]
    private EyeData[]   _eyebrowList = default;
    [SerializeField]
    private NoseData[]  _noseList = default;
    [SerializeField]
    private MouseData[] _mouseList = default;
    [SerializeField]
    private HairData[]  _hairList = default;

    [Header("整形情報")]
    [SerializeField]
    private SurgeryData[] _surgeryList = default;
    [SerializeField]
    private SurgeryGroupData[] _surgeryGroupList = default;

    public FaceGroupData GetFaceGroupData(FaceGroupCondition inCondition)
    {
        FaceGroupData resultFace = new FaceGroupData();

        EyeData[] eyeList = _eyeList;
        if (inCondition._specifyEye)
            eyeList = eyeList.Where(value => value._eyeType == inCondition._eyeType).ToArray();
        EyeData[] eyebrowList = _eyebrowList;
        if (inCondition._specifyEyebrow)
            eyebrowList = eyebrowList.Where(value => value._eyeType == inCondition._eyebrowType).ToArray();

        resultFace._eye     = eyeList[0];
        resultFace._eyebrow = eyebrowList[0];
        resultFace._nose    = _noseList[0];
        resultFace._mouse   = _mouseList[0];
        resultFace._hair    = _hairList[0];

        if (eyeList.Length > 0)
            resultFace._eye = _eyeList[Random.Range(0, _eyeList.Length)];
        if (eyebrowList.Length > 0)
            resultFace._eyebrow = eyebrowList[Random.Range(0, eyebrowList.Length)];
        if (_noseList.Length > 0)
            resultFace._nose = _noseList[Random.Range(0, _noseList.Length)];
        if (_mouseList.Length > 0)
            resultFace._mouse = _mouseList[Random.Range(0, _mouseList.Length)];
        if (_hairList.Length > 0)
            resultFace._hair = _hairList[Random.Range(0, _hairList.Length)];

        return resultFace;
    }

    public SurgeryGroupData GetSurgeryGroupData(int inVisitorsCount)
    {
        var groups = _surgeryGroupList.Where(value => value.InRange(inVisitorsCount)).ToArray();
        if (groups.Length == 0)
            return null;

        if (groups.Length == 1)
            return groups[0];

        int i = Random.Range(0, groups.Length);
        return groups[i];
    }



    public struct FaceGroupCondition
    {
        public bool _specifyEye;
        public bool _specifyEyebrow;
        public EyeType _eyeType;
        public EyeType _eyebrowType;
    }


}


