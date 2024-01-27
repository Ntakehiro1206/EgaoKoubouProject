using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

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
        Vector3 mousePos = Input.mousePosition;
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePos);

        foreach (var knot in splineContainer.Spline.Knots)
        {
            var worldPos = (Vector2)transform.TransformPoint(knot.Position);

            //knotのワールド座標をぜんなめ取得

            float distance = Vector2.Distance(worldPos, target);

            if (distance <= threshold)
            {
                GetComponent<LineRenderer>().material.color = Color.red;
                knotursor.SetActive(true);
                knotursor.transform.position = worldPos;
                break;
            }
            else
            {
                knotursor.SetActive(false);
                GetComponent<LineRenderer>().material.color = Color.white;
            }
            Debug.Log(distance);
        }


    }
}
