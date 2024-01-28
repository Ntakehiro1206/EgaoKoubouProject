#nullable enable
using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class SplineController : MonoBehaviour
{
    [SerializeField]
    private KnotsStatusScriptableObject knotsStatus;
    int knotsNumber;
    bool canMove;

    enum State //enum = 列挙子
    {
        A,
        B
    }

    //// コールバック event
    //public void MyCallBackMethod(string result)
    //{
    //    Debug.Log("処理完了 : " + result);
    //}

    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float threshold = 0.8f;
    [SerializeField] private GameObject knotursor;
    [SerializeField] private float projectionScale = 0.1f;
    /// <summary>
    /// 状態
    /// </summary>
    private State state;
    private FacePartType selectedPartType;
    /// <summary>
    /// 選択されたノットのインデックス
    /// </summary>
    private int selectedKnotIndex;
    private Vector2 dragStartPoint;
    private FaceRigInfo? face;

    // Start is called before the first frame update
    void Start()
    {
        state = State.A;

        // 暫定: シーンから有効な FaceRigInfo を1つ取得する
        face = UnityEngine.Object.FindFirstObjectByType<FaceRigInfo>();
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

    public event Action<GameObject>? OnStartDrag;

    private void BeforeDD() //ドラッグ＆ドロップ前の処理
    {
        Vector3 mousePosLocal = Input.mousePosition;
        Vector2 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosLocal);
        BezierKnot[] knots = splineContainer.Spline.Knots.ToArray();

        bool isFocus = false;

        for (int i = 0; i < knots.Length; i++)
        {
            BezierKnot knot = knots[i];

            //knotsStatusというScriptableObjectがあります
            if (knotsStatus.list[i].canMove == false)
            {
                continue;
            }

            var worldPos = (Vector2)transform.TransformPoint(knot.Position);
            //knotのワールド座標をぜんなめ取得

            float distance = Vector2.Distance(worldPos, mousePosWorld);

            if (distance <= threshold)
            {
                isFocus = true;
                knotursor.transform.position = worldPos;

                if (Input.GetMouseButtonDown(0))　//カーソル重なりつつマウスクリックされたとき
                {
                    //ドラッグ処理開始時のコールバック
                    OnStartDrag?.Invoke(gameObject);

                    state = State.B;
                    selectedPartType = FacePartType.Mouth;
                    selectedKnotIndex = i;
                    dragStartPoint = worldPos;
                }

                break; //foreachを抜ける
            }

            GetComponent<LineRenderer>().material.color = isFocus ? Color.red : Color.white;
            knotursor.SetActive(isFocus);
        }

        if (face != null)
        {
            foreach (var part in face)
            {
                if (part == null) continue;

                float dist = Vector2.Distance(mousePosWorld, part.Handle.position);
                if (dist <= threshold)
                {
                    knotursor.transform.position = mousePosWorld;
                    isFocus = true;

                    if (Input.GetMouseButtonDown(0)) //カーソル重なりつつマウスクリックされたとき
                    {
                        //ドラッグ処理開始時のコールバック
                        OnStartDrag?.Invoke(gameObject);

                        state = State.B;
                        selectedPartType = part.Type;
                        dragStartPoint = mousePosWorld;
                    }
                    break;
                }
            }
        }

        knotursor.gameObject.SetActive(isFocus);
    }

    public event Action<GameObject, float3[]>? CompleteHandler;

    private void NowDD() //ドラッグ＆ドロップしてる時の処理
    {

        if (!Input.GetMouseButton(0))
        {
            state = State.A;

            // コールバック実行
            CompleteHandler?.Invoke(gameObject, splineContainer.Spline.Knots.Select(value => value.Position).ToArray());

            return; 
        }

        Vector3 mousePosLocal = Input.mousePosition;
        Vector2 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosLocal);

        if (selectedPartType == FacePartType.Mouth)
        {
            // 口パーツの操作
            var knots = splineContainer.Spline.Knots.ToArray();

            var selectedKnot = knots[selectedKnotIndex];
            selectedKnot.Position = transform.InverseTransformPoint(mousePosWorld);
            knots[selectedKnotIndex] = selectedKnot;

            splineContainer.Spline.Knots = knots;
            knotursor.transform.position = mousePosWorld;
        }
        else
        {
            // リグの操作
            var part = face?[selectedPartType];
            if (part == null)
            {
                Debug.LogWarning("パーツが見つからない");
                state = State.A;
                return;
            }

            Vector2 vector = mousePosWorld - dragStartPoint;
            float length = vector.magnitude;
            if (length < Mathf.Epsilon) return;

            Vector2 direction = vector.normalized;
            float projection = Vector2.Dot(vector, part.ControlDirection) * length;

            float rate = Mathf.Clamp(projection * projectionScale, -1f, 1f);
            part.ApplyTransform(rate);

            // ハンドル表示
            knotursor.transform.position = part.Handle.position;
        }
    }
}
