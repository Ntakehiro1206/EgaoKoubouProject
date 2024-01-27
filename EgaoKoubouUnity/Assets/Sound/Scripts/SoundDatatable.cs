//------------------------------------------------------------
// (C) Copyright 2022 tgkg Inc.
//------------------------------------------------------------
using UnityEngine;

public enum BgmNameType
{
    Entrance,
}

public enum SfxNameType
{
    ClickButton,
    DrillLoop,
    DrillEnd,
    Clear2,
    Cotinue,
    Failure,
    GameOver,
    Money2,
    Move,
    MoveFace,
    Write,
    LimitTime,
    NextPage,
}


[CreateAssetMenu( menuName = "ScriptableObject/SoundDatatable", fileName = "SoundDatatable" )]
public class SoundDatatable : ScriptableObject
{
    [SerializeField, EnumListLabel(typeof(BgmNameType))]
    private SoundData[] _bgmList = default;
    [SerializeField, EnumListLabel(typeof(SfxNameType))]
    private SoundData[] _sfxList = default;

    public SoundData GetBgmData(BgmNameType aType)
    {
        return _bgmList[(int)aType];
    }

    public SoundData GetSfxData(SfxNameType aType)
    {
        return _sfxList[(int)aType];
    }

    [System.Serializable]
    public class SoundData
    {
        [SerializeField]
        private AudioClip _clip;
        [SerializeField]
        private bool      _loop;
        [SerializeField]
        private float     _volume = 1.0f;

        public AudioClip myClip => _clip;
        public bool myLoop => _loop;
        public float myVolume => _volume;

    }
} // class SoundDatatable

