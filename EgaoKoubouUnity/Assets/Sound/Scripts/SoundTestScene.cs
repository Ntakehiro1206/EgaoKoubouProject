using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundSystem.Instance.PlayBgm(_.BgmNameType.Entrance);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("down");
            SoundSystem.Instance.PlaySfx(_.SfxNameType.DrillLoop);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("up");
            SoundSystem.Instance.StopSfx(_.SfxNameType.DrillLoop);
            SoundSystem.Instance.PlaySfx(_.SfxNameType.DrillEnd);
        }
    }
}
