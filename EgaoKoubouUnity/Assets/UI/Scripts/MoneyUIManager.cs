using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class MoneyUIManager : MonoBehaviour
{
    [SerializeField]
    private MoneyItemUI _itemPrefab = null;
    [SerializeField]
    private int         _itemSize   = 1;

    private List<MoneyItemUI> _itemList = new List<MoneyItemUI>();

    void Update()
    {
        
    }

    public void Awake()
    {

    }


#if UNITY_EDITOR

    [ContextMenu("Create Item")]
    private void CreateItem()
    {
        _itemList.Clear();
        for (int i = 0; i < _itemSize; ++i)
        {
            var item = PrefabUtility.InstantiatePrefab(_itemPrefab, transform) as MoneyItemUI;
            _itemList.Add(item);
        }

        _itemList.Sort((a, b) => Mathf.CeilToInt(a.myAnchoredPostion.y - b.myAnchoredPostion.y));
    }

#endif

}
