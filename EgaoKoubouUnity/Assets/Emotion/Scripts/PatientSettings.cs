using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.Windows;

public class PatientSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject _root = null;
    [SerializeField]
    private Vector3[] _positions = null;
    [SerializeField]
    private float     _moveTime = 0.5f;
         

    public void SetVisible(bool inVisible)
    {
        _root.SetActive(inVisible);
    }

    public IEnumerator In()
    {
        Vector3 src = _positions[0];
        Vector3 dst = _positions[1];

        float st = Time.time;
        while(Time.time < st + _moveTime)
        {
            float dt = (Time.time - st) / _moveTime;
            _root.transform.localPosition = Vector3.Lerp(src, dst, dt);
            yield return null;
        }

        _root.transform.localPosition = dst;
    }

    public IEnumerator Out()
    {
        Vector3 src = _positions[1];
        Vector3 dst = _positions[2];

        float st = Time.time;
        while(Time.time < st + _moveTime)
        {
            float dt = (Time.time - st) / _moveTime;
            _root.transform.localPosition = Vector3.Lerp(src, dst, dt);
            yield return null;
        }

        _root.transform.localPosition = dst;
    }

    public void Set(PatientData inData)
    {
        {
            var map = new Dictionary<string, MeshRenderer>();
            foreach (var render in _root.GetComponentsInChildren<MeshRenderer>(true))
            {
                map.Add(render.name, render);
            }

//             map["faceOutline"].sharedMaterial = inData._basic._faceBG;
//             // map["body"].sharedMaterial = null;
//             map["eye_L1"].sharedMaterial = inData._face._eye._left._material;
//             map["eye_R1"].sharedMaterial = inData._face._eye._right._material;
//             map["eyebrow_L1"].sharedMaterial = inData._face._eyebrow._left._material;
//             map["eyebrow_R1"].sharedMaterial = inData._face._eyebrow._right._material;
            //map["mouth"].sharedMaterial = inData._face._mouse.;
            // map["nose"].sharedMaterial = null;
            //map["hair"].sharedMaterial = inData._face._hair._material;
        }

        {
            var map = new Dictionary<string, SplineContainer>();
            foreach (var component in _root.GetComponentsInChildren<SplineContainer>(true))
            {
                map.Add(component.name, component);
            }

            //             map["faceOutline"].sharedMaterial = inData._basic._faceBG;
            //             // map["body"].sharedMaterial = null;
            //             map["eye_L1"].sharedMaterial = inData._face._eye._left._material;
            //             map["eye_R1"].sharedMaterial = inData._face._eye._right._material;
            //             map["eyebrow_L1"].sharedMaterial = inData._face._eyebrow._left._material;
            //             map["eyebrow_R1"].sharedMaterial = inData._face._eyebrow._right._material;

            // map["nose"].sharedMaterial = null;
            // map["hair"].sharedMaterial = inData._face._hair._material;

            var knots = map["mouth"].Spline.Knots.ToArray();
            for(int i = 0; i < knots.Length; ++i)
            {
                if (inData._face._mouse._positions.Length > i)
                {
                    Vector2 v = inData._face._mouse._positions[i];
                    knots[i].Position = new float3(v.x, v.y, 0.0f);
                }
            }
            map["mouth"].Spline.Knots = knots;




        }


    }



}
