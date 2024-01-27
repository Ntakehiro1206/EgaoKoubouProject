using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public float timeLimit = 60.0f;
    float currentTime;

    private TimerImage timerImage;
    private Needle needle;

    // Start is called before the first frame update
    void Start()
    {
        timerImage = GetComponentInChildren<TimerImage>();
        needle = GetComponentInChildren<Needle>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = CountDownTimer();
        timerImage.SetAmount(1.0f - time);

        if (currentTime < timeLimit)
        {
            needle.NeedleRotate(360 / -timeLimit * Time.deltaTime);
        }
    }

    float CountDownTimer()
    {
        //経過時間を取得
        if (currentTime < timeLimit)
        {
            currentTime += Time.deltaTime;
        }
        float time = currentTime / timeLimit;
        //Debug.Log(time);
        return time;
    }

}
