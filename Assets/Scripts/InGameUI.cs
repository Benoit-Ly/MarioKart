using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {

    StartCountdown countdown;
    TimerUi timer;

    public StartCountdown Countdown { get { return countdown; } set { countdown = value; } }
    public TimerUi Timer { get { return timer; } set { timer = value; } }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startTimer()
    {
        if (timer == null)
            return;

        timer.StartTimer();
    }

    public void updateCountdown(int count)
    {
        if (countdown == null)
            return;

        countdown.updateCoundown(count);
    }
}
