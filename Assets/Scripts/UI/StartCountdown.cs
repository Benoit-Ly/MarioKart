using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartCountdown : MonoBehaviour {

    Text uiText;

    void Start()
    {
        uiText = GetComponent<Text>();
        GUIManager.Instance.InGameUI.Countdown = this;
    }

    public void updateCoundown(int count)
    {
        StartCoroutine(Countdown(count));
    }

    IEnumerator Countdown(int count)
    {
        if (count > 0)
            uiText.text = count.ToString();
        else
            uiText.text = "GO";

        yield return new WaitForSeconds(0.5f);
        uiText.text = "";
    }
}
