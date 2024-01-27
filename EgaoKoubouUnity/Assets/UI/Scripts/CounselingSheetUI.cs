using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CounselingSheetUI : MonoBehaviour
{
    public CounselingItemUI ui;
    public int addMax = 8;
    //private int addCount = 0;
    private List<CounselingItemUI> _itemList = new List<CounselingItemUI>();


    CounselingItemUI[] box = new CounselingItemUI[10];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(string inText)
    {
        if (_itemList.Count < addMax)
        {
            var newItem =  Instantiate(ui, transform);
            newItem.SetValue(inText);
            _itemList.Add(newItem);
        }
    }

    public void DeletionItem()
    {
        for (int i = 0; i < _itemList.Count; i++)
        {
            Destroy(_itemList[i].gameObject);
        }
        _itemList.Clear();
    }

    public void Check(bool inCheck)
    {
        if (_itemList.Count < addMax)
        {
            var newItem = Instantiate(ui, transform);
            newItem.Check(inCheck);
            _itemList.Add(newItem);
        }
    }

    public void SetPatientStatus(IPatientStatus inStatus)
    {
        DeletionItem();

        for (int i = 0; i < inStatus.myRequests.Length; ++i)
        {
            var newItem = Instantiate(ui, transform);
            _itemList.Add(newItem);
            newItem.SetPatientStatus(inStatus.myRequests[i]);
        }
    }
}
