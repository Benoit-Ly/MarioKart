using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIPosition : MonoBehaviour {

    int position = 1;
    public int Position {
        get { return position; }
        set
        {
            position = value;
            displayPosition();
        }
    }

    Text field;
    string label;

	// Use this for initialization
	void Start () {
        GUIManager.Instance.GuiPosition = this;

        field = GetComponent<Text>();
        label = field.text;
	}

    void displayPosition()
    {
        field.text = label + position.ToString();
    }
}
