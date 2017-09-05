using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    GameObject gameMgr;

    void Start()
    {
        gameMgr = GameObject.Find("GUIManager");
    }
    public void ShowLevelSelection()
    {
        gameMgr.SendMessage("ActivateLevelSelectionUI", true);
        gameMgr.SendMessage("ActivateMainMenuUI", false);
    }
}
