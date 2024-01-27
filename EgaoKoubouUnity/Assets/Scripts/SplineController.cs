using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class SplineController : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;

    // Start is called before the first frame update
    void Start()
    {
        if (splineContainer == null) return;

        var spline = splineContainer.Spline;

        spline[0] = new BezierKnot(new float3(0, 0, 0));
        spline[2] = new BezierKnot(new float3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
