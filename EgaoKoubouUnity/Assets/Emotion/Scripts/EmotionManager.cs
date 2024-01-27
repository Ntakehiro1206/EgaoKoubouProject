using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Splines;




public class EmotionManager : MonoBehaviour, IEmotionManager
{
    [SerializeField]
    private GameObject _root = null;
    [SerializeField]
    private FaceDatatable _datatable = null;
    [SerializeField]
    private int _money = 10000;

    private PatientStatus _patientStatus;

    private List<FaceData> _faces = new List<FaceData>();

    private void Awake()
    {
        if (_root != null)
        {
            SplineController[] splines = _root.GetComponentsInChildren<SplineController>();
            foreach (var spline in splines)
            {
                _faces.Add(new FaceData(spline.gameObject, spline.GetComponent<SplineContainer>().Spline.Knots.Select(value => value.Position).ToArray()));
                spline.OnStartDrag     += OnStartedFace;
                spline.CompleteHandler += OnChangedFace;
            }
        }
    }

    void Start()
    {
        GameMainUIManager.Instance.RegisterEmotion(this);
    }

    void Update()
    {

    }

    private void OnChangedFace(GameObject inObj, float3[] inPositions)
    {
        DrawDebugString(inObj, inPositions);

        if (_datatable.TryFacePartType(inObj.name, out Face.PartType partType))
        {
            Check(partType, inPositions);
        }

        SoundSystem.Instance.StopSfx(SfxNameType.DrillLoop);
        SoundSystem.Instance.PlaySfx(SfxNameType.DrillEnd);

        _patientStatus.myCurrentMoney = _patientStatus.myCurrentMoney - _money;
    }

    private void OnStartedFace(GameObject inObj)
    {
        SoundSystem.Instance.PlaySfx(SfxNameType.DrillLoop);
    }

    private void Check(Face.PartType inType, float3[] inPositions)
    {
        if (inType != Face.PartType.Mouse)
            return;
        if (_patientStatus == null)
            return;

        foreach(var req in _patientStatus._requests)
        {
            var angles = req.mySource._angles;
            if (angles == null || angles.Length < 2)
                continue;

            Vector2[] v = new Vector2[3];
            v[0] = new Vector2(inPositions[0].x, inPositions[0].y);
            v[1] = new Vector2(inPositions[1].x, inPositions[1].y);
            v[2] = new Vector2(inPositions[2].x, inPositions[2].y);

            Vector2 v01 = (v[0] - v[1]).normalized;
            Vector2 v02 = (v[2] - v[1]).normalized;

            float dot01 = Vector2.Dot(v01, Vector2.up);
            float dot02 = Vector2.Dot(v02, Vector2.up);
            float angle01 = Mathf.Acos(dot01) * Mathf.Rad2Deg;
            float angle02 = Mathf.Acos(dot02) * Mathf.Rad2Deg;

            if (angle01 >= angles[0] && angle01 <= angles[1]
                && angle02 >= angles[0] && angle02 <= angles[1])
            {
                req.myResult = true;
            }
            else
            {
                req.myResult = false;
            }

            Debug.Log($"{angle01}, {angle02}");
        }



    }

    private void DrawDebugString(GameObject inObj, float3[] inPositions)
    {
#if UNITY_EDITOR
        var data = _faces.FirstOrDefault(value => value.myName == inObj.name);
        if (data != null)
        {
            float3[] initials = data._initialPosition;
            float3[] current  = inPositions;

            string pointText = "";
            for (int i = 0; i < initials.Length; ++i)
            {
                float3 src = initials[i];
                float3 dst = inPositions[i];
                float3 sub = dst - src;
                string[] pt = new string[3];
                for (int j = 0; j < 3; ++j)
                    pt[j] = sub[j] >= 0.0f ? "+" : "";


                pointText += $"\t[{i}] x={dst.x:F1}({pt[0]}{sub.x:F1}), y={dst.y:F1}({pt[1]}{sub.y:F1})\n";
            }

            Debug.Log($"[{inObj.name}]\n{pointText}");
        }
#endif // UNITY_EDITOR
    }

    public void SetPatientStatus(PatientStatus inStatus)
    {
        _patientStatus = inStatus;
    }

    [System.Serializable]
    private class FaceData
    {
        public GameObject _object = null;
        public float3[]   _initialPosition = default;
        public string     myName => _object.name;

        public FaceData(GameObject inObject, float3[] inInitialPosition)
        {
            _object          = inObject;
            _initialPosition = inInitialPosition;
        }
    }

}
