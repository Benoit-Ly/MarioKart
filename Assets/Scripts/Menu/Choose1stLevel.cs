using UnityEngine;
using UnityEngine.SceneManagement;

public class Choose1stLevel : MonoBehaviour {

    // Use this for initialization
    GameObject gameMgr;
    GameObject levelMgr;

    void Start()
    {
        gameMgr = GameObject.Find("GUIManager");
        levelMgr = GameObject.Find("LevelManager");
    }
    public void Load1stLevel()
    {
        LevelManager.Instance.LoadLevel(0);
        gameMgr.SendMessage("ActivateInGameUI", true);
        gameMgr.SendMessage("ActivateLevelSelectionUI", false);
        
    }
}
