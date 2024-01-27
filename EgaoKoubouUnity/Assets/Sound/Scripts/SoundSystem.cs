using _;
using System.Linq;
using UnityEngine;
public class SoundSystem : MonoBehaviour
{
    [SerializeField]
    private SoundDatatable _datatable = default;
    [SerializeField]
    private SoundObject    _soundPrefab = default;

    public static SoundSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnDestroy()
    {
        if (Instance != this)
            Instance = null;
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
