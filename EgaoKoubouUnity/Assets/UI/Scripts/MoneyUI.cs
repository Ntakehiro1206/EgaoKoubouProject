using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    public MoneyImageUI moneyImagesUI;
    [SerializeField]
    public int AmountMoney;

    public List<MoneyImageUI> moneyList = new List<MoneyImageUI>();


    // Start is called before the first frame update
    void Start()
    {
        InstantiateMoney();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateMoney()
    {
        for (int i = 0; i < AmountMoney; i++)
        {
            var money = Instantiate(moneyImagesUI, transform);
            moneyList.Add(money);
        }

    }

    public void UseMoney(int money)
    {
        if (moneyList.Count > 0)
        {
            var obj = moneyList[0];
            Destroy(obj.gameObject);
            moneyList.Remove(obj);
        }
    }

}
