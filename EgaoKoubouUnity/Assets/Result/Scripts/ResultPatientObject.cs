using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPatientObject : MonoBehaviour
{
    [SerializeField]
    private RawImage _rawIamage = null;



    private ResultPatientData _patientData = null;
    private bool _dropping = false;

    public bool IsMissing()
    {
        return _patientData != null && !_patientData._result;
    }

    public void SetVisible(bool inVisible)
    {
        gameObject.SetActive(inVisible);
    }

    public void SetResult(ResultPatientData inData)
    {
        _patientData = inData;

        _rawIamage.gameObject.SetActive(true);
        _rawIamage.texture = inData._resultTexture;
    }



    void Start()
    {
        //_rawIamage.gameObject.SetActive(false);
    }


    void Update()
    {
        
    }

    public void Drop(Transform inTrans)
    {
        _dropping = false;
        var rigidbody = _rawIamage.transform.parent.gameObject.AddComponent<Rigidbody>();
        rigidbody.AddTorque(Vector3.forward * 200.0f, ForceMode.Impulse);


        Vector3 down = Vector3.down;
        down = Quaternion.AngleAxis(Random.Range(-45.0f, 45.0f), Vector3.forward) * down;
        rigidbody.AddForce(down * 300.0f, ForceMode.Impulse);

        transform.SetParent(inTrans);


    }
}
