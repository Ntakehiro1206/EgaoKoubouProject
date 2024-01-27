using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionTestScene : MonoBehaviour
{
    [SerializeField]
    private FaceDatatable _faceDatatable = default;

    private int m_patientIndex = 0;
    private PatientSettings _patient = null;


    IEnumerator Start()
    {
        _patient = GetComponent<PatientSettings>();
        _patient.SetVisible(false);

        yield return null;
        yield return PreProcess();
        yield return MainProcess();
        yield return PostProcess();
    }

    IEnumerator PreProcess()
    {

        GameMainUIManager.Instance.SetMainVisible(false);

        SoundSystem.Instance.PlayBgm(BgmNameType.Gameplay);
        yield return null;
    }
    IEnumerator MainProcess()
    {
        while (!IsFinished())
        {
            yield return PreGameProcess();
            yield return GameProcess();
            yield return PostGameProcess();
        }
    }

    IEnumerator PostProcess()
    {
        SoundSystem.Instance.StopBgm(BgmNameType.Gameplay);
        yield return null;
    }


    bool IsFinished()
    {
        return false;
    }



    IEnumerator PreGameProcess()
    {
        var surgerys = _faceDatatable.GetSurgeryGroupData(m_patientIndex);
        var patient  = _faceDatatable.GetPatientData(surgerys);
        _patient.Set(patient);

        _patient.SetVisible(true);
        yield return _patient.In();
        GameMainUIManager.Instance.SetMainVisible(true);
    }
    IEnumerator GameProcess()
    {
        yield return new WaitForSeconds(10.0f);
        yield return null;
    }
    IEnumerator PostGameProcess()
    {
        GameMainUIManager.Instance.SetMainVisible(false);
        yield return _patient.Out();

        m_patientIndex++;
    }
}
