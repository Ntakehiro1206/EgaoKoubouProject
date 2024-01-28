using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("KnotsStatus"))]
public class KnotsStatusScriptableObject : ScriptableObject
{
    public List<KnotsStatus> list = new List<KnotsStatus>();

    [System.Serializable]
    public class KnotsStatus
    {
        public int knotsNumbur;
        public bool canMove;
    }
}
