using UnityEngine;
using System.Collections;
using System;

public class GUIManager : MonoBehaviour {

    [SerializeField]
    private GameObject mainMenuUIPrefab = null;
    [SerializeField]
    private GameObject highscoreUIPrefab = null;
    [SerializeField]
    private GameObject levelSelectionUIPrefab = null;
    [SerializeField]
    private GameObject inGameUIPrefab = null;
    [SerializeField]
    private GameObject endRaceUIPrefab = null;

    GameObject mainMenuUI;
    GameObject highscoreUI;
    GameObject levelSelectionUI;
    GameObject inGameUI;
    GameObject endRaceUI;

    InGameUI inGameUIComponent;
    public InGameUI InGameUI { get { return inGameUIComponent; } }

    GUILap guiLap = null;
    public GUILap GuiLap { get { return guiLap; } set { guiLap = value; } }

    GUIPosition guiPosition = null;
    public GUIPosition GuiPosition { get { return guiPosition; } set { guiPosition = value; } }

    static private GUIManager p_instance = null;
    static public GUIManager Instance
    {
        get
        {
            return p_instance;
        }
    }

    void Awake()
    {
        if (GUIManager.Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        p_instance = this;
    }

    // Use this for initialization
    void Start()
    {
        mainMenuUI = (GameObject)Instantiate(mainMenuUIPrefab, Vector3.zero, Quaternion.identity);
        //mainMenuUI.SetActive(false);
        highscoreUI = (GameObject)Instantiate(highscoreUIPrefab, Vector3.zero, Quaternion.identity);
        highscoreUI.SetActive(false);
        levelSelectionUI = (GameObject)Instantiate(levelSelectionUIPrefab, Vector3.zero, Quaternion.identity);
        levelSelectionUI.SetActive(false);
        inGameUI = (GameObject)Instantiate(inGameUIPrefab, Vector3.zero, Quaternion.identity);
        inGameUIComponent = inGameUI.GetComponent<InGameUI>();
        inGameUI.SetActive(false);
        endRaceUI = (GameObject)Instantiate(endRaceUIPrefab, Vector3.zero, Quaternion.identity);
        endRaceUI.SetActive(false);

        CarManager.Instance.OnRegisterPlayerLap += updateLap;
        CarManager.Instance.OnCountdown += updateCountdown;
        CarManager.Instance.OnRaceStart += startTimer;
        CarManager.Instance.OnSortPosition += updatePosition;
        CarManager.Instance.OnRaceFinish += finishRace;
    }

    public void ActivateMainMenuUI(bool value)
    {
        mainMenuUI.SetActive(value);
    }

    public void ActivateHighScoreUi(bool value)
    {
        if (highscoreUI)
            highscoreUI.SetActive(value);
        else
        {
            highscoreUI = (GameObject)Instantiate(highscoreUIPrefab, Vector3.zero, Quaternion.identity);
            //highscoreUI.SetActive(true);
            highscoreUI.SetActive(value);
        }
        
    }

    public void ActivateLevelSelectionUI(bool value)
    {
        levelSelectionUI.SetActive(value);
    }

    public void ActivateInGameUI(bool value)
    {
        inGameUI.SetActive(value);
    }

    void updateCountdown(int count)
    {
        if (inGameUIComponent == null)
            return;

        inGameUIComponent.updateCountdown(count);
    }

    void startTimer()
    {
        inGameUIComponent.startTimer();
    }

    void updateLap()
    {
        if (guiLap == null)
            return;

        guiLap.NbLap = CarManager.Instance.Player.Lap;
    }

    void updatePosition()
    {
        if (guiPosition == null)
            return;

        guiPosition.Position = CarManager.Instance.Player.Position;
    }

    void finishRace()
    {
        inGameUI.SetActive(false);
        if (endRaceUI)
            endRaceUI.SetActive(true);
        else
        {
            endRaceUI = (GameObject)Instantiate(endRaceUIPrefab, Vector3.zero, Quaternion.identity);
            endRaceUI.SetActive(true);
        }
    }

    void OnDestroy()
    {
        CarManager.Instance.OnRegisterPlayerLap -= updateLap;
        CarManager.Instance.OnCountdown -= updateCountdown;
        CarManager.Instance.OnSortPosition -= updatePosition;
        CarManager.Instance.OnRaceFinish -= finishRace;
    }
}
