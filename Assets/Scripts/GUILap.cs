using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUILap : MonoBehaviour {

    int nbLap = 1;
    public int NbLap {
        get { return nbLap; }
        set
        {
            nbLap = value;

        }
    }

    Text field;
    string label;

	// Use this for initialization
	void Start () {
        GUIManager.Instance.GuiLap = this;
        field = GetComponent<Text>();
        label = field.text;
	}
	
	// Update is called once per frame
	void Update () {
        field.text = label + nbLap.ToString() + "/3";
	}
}
