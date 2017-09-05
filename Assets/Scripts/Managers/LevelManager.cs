using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : Manager {

    static private LevelManager p_instance = null;
    static public LevelManager Instance
    {
        get
        {
            return p_instance;
        }
    }

    public event Action OnLoadLevel;

    bool levelLoaded = false;
    [SerializeField]
    List<GameObject> levelList;
    GameObject currentLevelLoaded;

    void Awake()
    {
        if (LevelManager.Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        p_instance = this;
    }

	// Use this for initialization
	void Start () {
        //SceneManager.LoadScene(1);
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void LoadLevel(int num)
    {
        //currentLevelLoaded = (GameObject)Instantiate(levelList[num], Vector3.zero, Quaternion.identity);

        SceneManager.LoadScene(1);

        if (OnLoadLevel != null && !levelLoaded)
        {
            levelLoaded = true;
            OnLoadLevel();
        }
    }
}
