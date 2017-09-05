using UnityEngine;
using System.Collections;

public class SpawnItem : MonoBehaviour {

    [SerializeField]
    GameObject itemPrefab;
    Transform itemPos;
    Vector3 itemPosition;

    
    // Use this for initialization
    void Start () {

        itemPos = transform;
        itemPosition = transform.position;
        for (int i = 0; i < 4 ; ++i)
        {
            Spawn();
            itemPosition.x += 4f;
           
        }
    }
    
    void Spawn(Vector3 pos = new Vector3())
    {
        GameObject item = Instantiate(itemPrefab, itemPosition, Quaternion.identity) as GameObject;
        item.transform.parent = itemPos;
        if (pos != Vector3.zero)
        {
            item.transform.position = pos;
        }
    }

    public void RespawnAt(Vector3 pos)
    { 
        StartCoroutine(Respawn(pos));
    }

    IEnumerator Respawn(Vector3 pos)
    {
        yield return new WaitForSeconds(3f);
        Spawn(pos);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
