using Cosmetic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IEmotionManager
{

}

public interface IPatientStatus
{
    int myMaxMoney => 0;
    int myCurrentMoney => 0;
    IPatientRequestStatus[] myRequests => new IPatientRequestStatus[0];
}

public interface IPatientRequestStatus
{
    int myId => 0;
    bool myResult => false;

    SurgeryData mySource => default;
}

public class PatientStatus : IPatientStatus
{
    public int myMaxMoney { get; private set; }
    public int myCurrentMoney { get; set; }
    public IPatientRequestStatus[] myRequests => _requests;

    public PatientRequestStatus[] _requests = null;

    public PatientStatus(int inMoney, PatientRequestStatus[] inRequests)
    {
        myCurrentMoney = myMaxMoney = inMoney;
        _requests = inRequests;
    }
}

public class PatientRequestStatus : IPatientRequestStatus
{
    public int myId { get; private set; }
    public int myMaxMoney { get; private set; }
    public int myCurrentMoney { get; set; }
    public bool myResult { get; set; }

    public SurgeryData mySource { get; private set; }

    public PatientRequestStatus(int inIndex, SurgeryData inSource)
    {
        myId = inIndex;
        mySource = inSource;

    }
}



public class EmotionTestScene : MonoBehaviour
{
    [SerializeField]
    private FaceDatatable _faceDatatable = default;
    [SerializeField]
    private float         _gameplayTime  = 60.0f;
    [SerializeField]
    private string        _nextScene = "";

    private int m_patientIndex = 0;
    private float _startTime = 0.0f;
    private PatientSettings _patient = null;
    private EmotionManager  _emotion = null;
    private PatientStatus _currentStatus = null;

    private static int _requestIndex = 0;
    


    IEnumerator Start()
    {
        _patient = GetComponent<PatientSettings>();
        _emotion = GetComponent<EmotionManager>();
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
        _startTime = Time.time;
        while (_startTime + _gameplayTime > Time.time)
        {
            yield return PreGameProcess();
            yield return GameProcess();
            yield return PostGameProcess();
        }


    }

    IEnumerator PostProcess()
    {
        SoundSystem.Instance.StopBgm(BgmNameType.Gameplay);

        SoundSystem.Instance.PlaySfx(SfxNameType.GameOver);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(_nextScene);
    }


    IEnumerator PreGameProcess()
    {
        var surgerys = _faceDatatable.GetSurgeryGroupData(m_patientIndex);
        var patient  = _faceDatatable.GetPatientData(surgerys);
        _patient.Set(patient);

        SettingPlayData(surgerys, patient);

        _patient.SetVisible(true);
        yield return _patient.In();
        GameMainUIManager.Instance.SetMainVisible(true);
    }
    IEnumerator GameProcess()
    {
        float startTime = Time.time;
        while (_currentStatus.myCurrentMoney > 0 && _currentStatus.myRequests.Any(value => !value.myResult) && _startTime + _gameplayTime > Time.time)
        {
            yield return null;
        }
    }
    IEnumerator PostGameProcess()
    {
        
        if (!_currentStatus.myRequests.Any(value => !value.myResult))
        {
            SoundSystem.Instance.PlaySfx(SfxNameType.Clear2);
        }
        else
        {
            SoundSystem.Instance.PlaySfx(SfxNameType.Failure);
        }

        GameMainUIManager.Instance.SetMainVisible(false);
        yield return _patient.Out();

        m_patientIndex++;
    }

    private void SettingPlayData(SurgeryGroupData inSurgeryGroup, PatientData inPatient)
    {
        var surgerys = new List<PatientRequestStatus>();
        foreach (var surgeryIndex in inSurgeryGroup._surgerIndexList)
        {
            var data = _faceDatatable.GetSurgeryData(surgeryIndex);
            surgerys.Add( new PatientRequestStatus(++_requestIndex, data));
        }

        _currentStatus = new PatientStatus(inSurgeryGroup._money, surgerys.ToArray());
        _emotion.SetPatientStatus(_currentStatus);
        GameMainUIManager.Instance.SetPatientStatus(_currentStatus);
    }

    
}
