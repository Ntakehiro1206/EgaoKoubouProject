using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestAlphaSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ui = default;

    private CounselingSheetUI sheetUI;
    private EmotionWindowUI   emotionUI;

    private 
    
    void Start()
    {
        sheetUI   = _ui.GetComponentInChildren<CounselingSheetUI>();
        emotionUI = _ui.GetComponentInChildren<EmotionWindowUI>();

        SoundSystem.Instance.PlayBgm(BgmNameType.Entrance);

        sheetUI.AddItem("鼻を高くしたい");
        sheetUI.AddItem("頬骨を削りたい");
        sheetUI.AddItem("二重にしたい");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
