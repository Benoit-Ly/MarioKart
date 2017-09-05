using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {


    GameObject gameMgr;

    void Start()
    {
        gameMgr = GameObject.Find("GUIManager");
    }
    public void GoBack()
    {
        gameMgr.SendMessage("ActivateHighScoreUi", false);
        gameMgr.SendMessage("ActivateInGameUI", false);
        gameMgr.SendMessage("ActivateMainMenuUI", true);
    }
}
