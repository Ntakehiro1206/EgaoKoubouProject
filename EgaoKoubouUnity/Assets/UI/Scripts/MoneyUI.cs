using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    public MoneyImageUI moneyImagesUI;
    [SerializeField]
    public int AmountMoney;

    private IPatientStatus _status = default;
    private int _cacheMoney = 0;

    private List<MoneyImageUI> moneyList = new List<MoneyImageUI>();


    // Start is called before the first frame update
    void Start()
    {
        // InstantiateMoney();
        foreach (Transform ts in transform)
        {
            moneyList.Add(ts.GetComponent<MoneyImageUI>());
        }
        moneyList = moneyList.OrderByDescending(value => value.name).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (_status == null)
            return;

        if (_cacheMoney != _status.myCurrentMoney)
        {
            _cacheMoney = _status.myCurrentMoney;
            UseMoney();
        }

    }

//     private void InstantiateMoney()
//     {
//         for (int i = 0; i < AmountMoney; i++)
//         {
//             var money = Instantiate(moneyImagesUI, transform);
//             money.name = $"[{i:D2}]{money.name}";
//             moneyList.Add(money);
//         }
//         moneyList = moneyList.OrderByDescending(value => value.name).ToList();
// 
//     }

    public void UseMoney(int money = 0)
    {
        foreach (var item in moneyList)
        {
            if (item.gameObject.activeSelf)
            {
                item.gameObject.SetActive(false);
                SoundSystem.Instance.PlaySfx(SfxNameType.Money2);
                break;
            }
        }
    }

    public void SetVisibleAll(bool inVisible)
    {
        foreach(var item in moneyList)
        {
            item.gameObject.SetActive(inVisible);
        }
    }

    public void SetPatientStatus(IPatientStatus inStatus)
    {
        SetVisibleAll(false);

        _status = inStatus;
        _cacheMoney = inStatus.myCurrentMoney;
        int count = inStatus.myMaxMoney / 10000;

        for ( int i = 0; i < moneyList.Count; i++)
        {
            moneyList[i].gameObject.SetActive(i >= count);
        }
    }

#if UNITY_EDITOR

    [ContextMenu("Create Item")]
    private void CreateItem()
    {
        moneyList.Clear();
        for (int i = 0; i < AmountMoney; ++i)
        {
            var item = PrefabUtility.InstantiatePrefab(moneyImagesUI, transform) as MoneyImageUI;
            item.name = $"[{i:D2}]{item.name}";
            // moneyList.Add(item);
        }

        // moneyList.Sort((a, b) => Mathf.CeilToInt(a.myAnchoredPostion.y - b.myAnchoredPostion.y));
    }

#endif

}
