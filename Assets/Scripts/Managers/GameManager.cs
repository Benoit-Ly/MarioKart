using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static private GameManager p_instance = null;
    static public GameManager Instance
    {
        get
        {
            return p_instance;
        }
    }

    void Awake()
    {
        if (GameManager.Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        p_instance = this;
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
