using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class SplineController : MonoBehaviour
{
    enum State //enum = 列挙子
    {
        A,
        B
    }

    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float threshold = 0.8f;
    [SerializeField] private GameObject knotursor;
    /// <summary>
    /// 状態
    /// </summary>
    private State state;
    /// <summary>
    /// 選択されたノットのインデックス
    /// </summary>
    private int selectedKnotIndex;

    // Start is called before the first frame update
    void Start()
    {
        state = State.A;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.A)
        {
            BeforeDD();
        }

        if (state == State.B)
        {
            NowDD();
        }
    }

    private void BeforeDD() //ドラッグ＆ドロップ前の処理
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePos);
        BezierKnot[] knots = splineContainer.Spline.Knots.ToArray();

        for (int i = 0; i < knots.Length; i++)
        {
            BezierKnot knot = knots[i];
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
                    state = State.B;
                    selectedKnotIndex = i;
                }

                break; //foreachを抜ける
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
        if (!Input.GetMouseButton(0))
        {
            state = State.A;
            return; 
        }

        Vector3 mousePos = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePos);
        var knots = splineContainer.Spline.Knots.ToArray();

        var selectedKnot = knots[selectedKnotIndex];
        selectedKnot.Position = transform.InverseTransformPoint(target);
        knots[selectedKnotIndex] = selectedKnot;

        splineContainer.Spline.Knots = knots;

        knotursor.transform.position = target;
    }
}
