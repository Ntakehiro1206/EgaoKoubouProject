using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IGameUI
{
    void SetMainVisible(bool inVisible) { }


    void SetPatientStatus(IPatientStatus inStatus) { }
    void RegisterEmotion(IEmotionManager inEmotion) { }
}

public class NullGameUI : IGameUI { }

public class GameMainUIManager : MonoBehaviour, IGameUI
{
    private static NullGameUI       _null = new NullGameUI();
    private static GameMainUIManager _instance = null;

    private CounselingSheetUI   _counseling;
    private EmotionWindowUI     _emotion;
    private MoneyUI             _money;

    public static IGameUI Instance => HasInstance() ? _instance : _null;
    private static bool HasInstance() => _instance != null;
    public static void SetInstance(GameMainUIManager inValue) { _instance = inValue; }

    private void Awake()
    {
        if (HasInstance())
        {
            Destroy(gameObject);
            return;
        }
        SetInstance(this);
    }
    private void OnDestroy()
    {
        if (_instance == this)
            SetInstance(null);
    }

    void Start()
    {
        _counseling = GetComponentInChildren<CounselingSheetUI>();
        _emotion    = GetComponentInChildren<EmotionWindowUI>();
        _money      = GetComponentInChildren<MoneyUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMainVisible(bool inVisible)
    {
        //_counseling.gameObject.SetActive(inVisible);
        _money.gameObject.SetActive(inVisible);
    }

    public void SetPatientStatus(IPatientStatus inStatus)
    {
        _counseling.SetPatientStatus(inStatus);
        _money.SetPatientStatus(inStatus);
    }


    public void RegisterEmotion(IEmotionManager inEmotion)
    {

    }
}
