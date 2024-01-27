using System.Linq;
using UnityEngine;

public interface ISoundSystem
{
    void PlayBgm(BgmNameType inType) { }
    void StopBgm(BgmNameType inType) { }
    void PlaySfx(SfxNameType inType) { }
    void StopSfx(SfxNameType inType) { }
}

public class NullSoundSystem : ISoundSystem
{

}


public class SoundSystem : MonoBehaviour, ISoundSystem
{
    [SerializeField]
    private SoundDatatable _datatable = default;
    [SerializeField]
    private SoundObject    _soundPrefab = default;


    private static NullSoundSystem locNullSystem = new NullSoundSystem();
    private static SoundSystem _instance = null;

    public static ISoundSystem Instance => HasInstance() ? _instance : locNullSystem;
    private static bool HasInstance() => _instance != null;
    public  static void SetInstance(SoundSystem inSystem) { _instance = inSystem; }

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

    public void PlayBgm(BgmNameType inType)
    {
        var data = _datatable.GetBgmData(inType);

        var obj = Instantiate(_soundPrefab, transform);
        obj.Play(data);
    }

    public void StopBgm(BgmNameType inType)
    {
        var data = _datatable.GetBgmData(inType);
        var sound = GetComponentsInChildren<SoundObject>().FirstOrDefault(obj => obj.HasClip(data.myClip));
        if (sound != null)
        {
            sound.Stop();
        }
    }

    public void PlaySfx(SfxNameType inType)
    {
        var data = _datatable.GetSfxData(inType);

        var obj = Instantiate(_soundPrefab, transform);
        obj.Play(data);
    }

    public void StopSfx(SfxNameType inType)
    {
        var data = _datatable.GetSfxData(inType);
        var sound = GetComponentsInChildren<SoundObject>().FirstOrDefault(obj => obj.HasClip(data.myClip));
        if (sound != null)
        {
            sound.Stop();
        }
    }

}
