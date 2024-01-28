using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour
{
    [SerializeField]
    private ResultGridObject _resultGrid = null;

    [SerializeField]
    private Transform _dropPoint = null;
    [SerializeField]
    private string _next = "";
    
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
        
        yield return new WaitForSeconds(2.0f);
    }
    IEnumerator MainSceneProcess()
    {
        var missingList = _resultGrid.GetMissingPatients();

        for (int i = 0; i < 4; ++i)
        {
            yield return new WaitForSeconds(0.23f);
            SoundSystem.Instance.PlaySfx(SfxNameType.ResultPop);

            if (missingList.Count > 0)
            {
                var obj = missingList[0];
                obj.Drop(_dropPoint);
                missingList.RemoveAt(0);
            }
        }

        for (int i = 0; i < 6; ++i)
        {
            yield return new WaitForSeconds(0.16f);
            SoundSystem.Instance.PlaySfx(SfxNameType.ResultPop);

            if (missingList.Count > 0)
            {
                var obj = missingList[0];
                obj.Drop(_dropPoint);
                missingList.RemoveAt(0);
            }
        }

        for (int i = 0; i < 6; ++i)
        {
            yield return new WaitForSeconds(0.12f);
            SoundSystem.Instance.PlaySfx(SfxNameType.ResultPop);

            if (missingList.Count > 0)
            {
                var obj = missingList[0];
                obj.Drop(_dropPoint);
                missingList.RemoveAt(0);
            }
        }

        for (int i = 0; i < 4; ++i)
        {
            yield return new WaitForSeconds(0.08f);
            SoundSystem.Instance.PlaySfx(SfxNameType.ResultPop);

            if (missingList.Count > 0)
            {
                var obj = missingList[0];
                obj.Drop(_dropPoint);
                missingList.RemoveAt(0);
            }
        }

        if (missingList.Count > 0)
        {
            var obj = missingList[0];
            obj.Drop(_dropPoint);
            missingList.RemoveAt(0);
        }


        yield return new WaitForSeconds(3.0f);
    }
    IEnumerator PostSceneProcess()
    {
        yield return null;
        SceneManager.LoadScene(_next);
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
