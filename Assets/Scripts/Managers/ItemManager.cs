using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ItemManager : Manager {

    static private ItemManager p_instance = null;
    static public ItemManager Instance
    {
        get
        {
            return p_instance;
        }
    }

    public event Action<GameObject, GameObject> OnGetItem;

    [SerializeField]
    GameObject itemBoxPrefab;
    [SerializeField]
    bool debugMode = false;
    [SerializeField]
    int debugItemIndex = 0;
    [SerializeField]
    List<GameObject> items;

    void Awake()
    {
        if (ItemManager.Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        p_instance = this;
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject getRandomItem()
    {
        int index;
        if (debugMode)
            index = debugItemIndex;
        else
            index = UnityEngine.Random.Range(0, items.Count - 1);

        return items[index];
    }

    public void reportItemBoxCollision(GameObject go)
    {
        GameObject item = getRandomItem();

        if (OnGetItem != null)
            OnGetItem(go, item);
    }

    public void RespawnAt(Vector3 pos)
    {
        StartCoroutine(RespawnItemBox(pos));
    }

    IEnumerator RespawnItemBox(Vector3 pos)
    {
        yield return new WaitForSeconds(3f);
        Instantiate(itemBoxPrefab, pos, Quaternion.identity);
    }
}
