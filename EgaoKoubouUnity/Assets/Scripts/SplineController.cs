using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEngine.GraphicsBuffer;

public class SplineController : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float threshold = 0.8f;
    [SerializeField] private GameObject knotursor;

    // Start is called before the first frame update
    void Start()
    {
        if (splineContainer == null) return;

        var spline = splineContainer.Spline;

        spline[0] = new BezierKnot(new float3(0, 0, 0));
        spline[2] = new BezierKnot(new float3(0.5f, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        BeforeDD();

        //// todo: knotsの編集

        //splineContainer.Spline.Knots = knots;
    }

    private void BeforeDD() //ドラッグ＆ドロップ前の処理
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePos);
        var knots = splineContainer.Spline.Knots;

        foreach (var knot in knots)
        {
            var worldPos = (Vector2)transform.TransformPoint(knot.Position);
            //knotのワールド座標をぜんなめ取得

            float distance = Vector2.Distance(worldPos, target);

            if (distance <= threshold)
            {
                bool isFocus = true; //カーソルが重なっているか判定
                GetComponent<LineRenderer>().material.color = Color.red;
                knotursor.SetActive(true);
                knotursor.transform.position = worldPos;

                if (isFocus == true && Input.GetMouseButtonDown(0))　//カーソル重なりつつマウスクリックされたとき
                {
                    NowDD();
                    Debug.Log("NOW DD RUN");
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("NOW DD THE END");
                }

                break;
            }
            else
            {
                knotursor.SetActive(false);
                GetComponent<LineRenderer>().material.color = Color.white;
            }
        }
    }

    private void NowDD() //ドラッグ＆ドロップしてる時の処理
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePos);
        var knots = splineContainer.Spline.Knots;

        foreach (var knot in knots)
        {
            var worldPos = (Vector2)transform.TransformPoint(knot.Position);
            //knotのワールド座標をぜんなめ取得

        }

        // todo: knotsの編集
        splineContainer.Spline.Knots = knots;

    }
}
