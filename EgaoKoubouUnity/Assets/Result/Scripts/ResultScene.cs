using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScene : MonoBehaviour
{
    [SerializeField]
    private ResultGridObject _resultGrid = null;
    
    IEnumerator Start()
    {
        yield return PreSceneProcess();
        yield return MainSceneProcess();
        yield return PostSceneProcess();
    }

    IEnumerator PreSceneProcess()
    {
        _resultGrid.SetResult(ResultStaticData.GetPatients());
        _resultGrid.SetVisible(true);
        
        yield return null;
    }
    IEnumerator MainSceneProcess()
    {
        yield return null;
    }
    IEnumerator PostSceneProcess()
    {
        yield return null;
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
