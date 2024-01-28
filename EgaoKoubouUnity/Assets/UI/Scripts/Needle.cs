using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{

    void Update()
    {

    }
    public void NeedleRotate(float angle)
    {
        transform.Rotate(new Vector3(0f, 0f, angle));
    }
}
