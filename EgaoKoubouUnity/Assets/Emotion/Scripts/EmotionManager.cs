using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;


public interface IEmotionManager
{

}

public interface IPatientStatus
{
    string myId => "";
    int myMoney => 0;
    IPatientRequestStatus[] myStatuses => new IPatientRequestStatus[0];
}

public interface IPatientRequestStatus
{
    int myId => 0;
    EmotionDatatable.ResuestData myData => null;
    bool myResult => false;
}


public class EmotionManager : MonoBehaviour, IEmotionManager
{
    [SerializeField]
    private GameObject _root = null;
    [SerializeField]
    private EmotionDatatable _datatable = null;

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

        SoundSystem.Instance.StopSfx(SfxNameType.DrillLoop);
        SoundSystem.Instance.PlaySfx(SfxNameType.DrillEnd);
    }

    private void OnStartedFace(GameObject inObj)
    {
        SoundSystem.Instance.PlaySfx(SfxNameType.DrillLoop);
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

    private class PatientStatus : IPatientStatus
    {
        public int myMoney { get; private set; }

        public PatientStatus(int inMoney)
        {

        }

    }
}
