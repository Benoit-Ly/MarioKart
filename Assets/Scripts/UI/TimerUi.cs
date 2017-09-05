using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerUi : MonoBehaviour {

    Text textUi;
    bool start = false;
    float time = 0f;
	// Use this for initialization
	void Start () {
        textUi = GetComponent<Text>();

        GUIManager.Instance.InGameUI.Timer = this;
    }
	
    public void StartTimer()
    {
        start = true;
        InvokeRepeating("IncrementTimer", 0.01f, 0.01f);
    }

    void OnStopTimer()
    {
        start = false;
    }

    void IncrementTimer()
    {
        if (start)
        {
            time++;

            textUi.text = "Time : " + ParseTimeToString(time);
        }
    }

    string ParseTimeToString(float time)
    {

        int min = (int)time / 6000;
        int sec;
        if (min > 0)
            sec = (int)((time - min * 6000) /100);
        else
            sec = (int)time / 100;
        int dsec = (int)(time - (sec * 100) - (min * 6000));
        
        string result = ParseToTime(min) + ":" + ParseToTime(sec) + ":" + ParseToTime(dsec);

        return result;
    }

    string ParseToTime(int value)
    {
        if (value < 10)
        {
            if (value <= 0)
                return "00";
            else
                return "0" + value.ToString();
        }
        else
            return value.ToString();
    }
}
