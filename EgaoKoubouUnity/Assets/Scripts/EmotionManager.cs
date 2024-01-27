using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _root = null;

    private void Awake()
    {
        if (_root != null)
        {
            SplineController[] splines = _root.GetComponentsInChildren<SplineController>();
            foreach (var spline in splines)
            {
                spline.onStarted    += OnStartedFace;
                spline.onFinishedDD += OnChangedFace;
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnChangedFace(GameObject inObj, float3[] inPositions)
    {

        SoundSystem.Instance.StopSfx(SfxNameType.DrillLoop);
        SoundSystem.Instance.PlaySfx(SfxNameType.DrillEnd);
    }

    private void OnStartedFace(GameObject inObj)
    {
        SoundSystem.Instance.PlaySfx(SfxNameType.DrillLoop);
    }
}
