using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public float timeLimit = 60.0f;
    float currentTime;


    private TimerImage timerImage;

    // Start is called before the first frame update
    void Start()
    {
        timerImage = GetComponentInChildren<TimerImage>();

    }

    // Update is called once per frame
    void Update()
    {
        float time  = CountDownTimer();
        timerImage.SetAmount(1.0f - time);
    }

    float CountDownTimer()
    {
        //経過時間を取得
        if (currentTime < timeLimit)
        {
            currentTime += Time.deltaTime;
        }
        float time = currentTime / timeLimit;
        Debug.Log(time);
        return time;
    }
}
