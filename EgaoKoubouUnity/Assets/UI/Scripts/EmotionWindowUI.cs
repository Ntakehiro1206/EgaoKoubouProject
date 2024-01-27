using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionWindowUI : MonoBehaviour
{
    public float waitTime = 3.0f;
    public Image[] EmotionImages = new Image[2];

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                for (int i = 0; i < EmotionImages.Length; i++)
                {
                    EmotionImages[i].gameObject.SetActive(false);
                }
            }
        }

    }

    public void SetEmotion(int inType)
    {
        timer = waitTime;
        for (int i = 0; i < EmotionImages.Length; i++)
        {
            EmotionImages[i].gameObject.SetActive(false);
        }
            switch (inType)
        {
            case 0:
                EmotionImages[0].gameObject.SetActive(true);
                break;
            case 1:
                EmotionImages[1].gameObject.SetActive(true);
                break;
            case 2:
                EmotionImages[2].gameObject.SetActive(true);
                break;
        }
    }
}
