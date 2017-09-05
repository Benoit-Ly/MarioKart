using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresButton : MonoBehaviour {

    GameObject gameMgr;

    void Start()
    {
        gameMgr = GameObject.Find("GUIManager");
    }

       public void GoHighScoreScene()
    {
        gameMgr.SendMessage("ActivateMainMenuUI", false);
        gameMgr.SendMessage("ActivateHighScoreUi", true);
    }

}
