using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    public MoneyImageUI moneyImagesUI;
    [SerializeField]
    public int AmountMoney;



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

    public void UseMoney(int money)
    {
        if (moneyList.Count > 0)
        {
            var obj = moneyList[0];
            Destroy(obj.gameObject);
            moneyList.Remove(obj);
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
