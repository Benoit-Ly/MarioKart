using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision coll)
    {
        if (coll != null)
        {
            GameObject coll_obj = coll.gameObject;

            if (coll_obj != null && coll_obj.layer == LayerMask.NameToLayer("Player"))
            {
                ItemManager.Instance.reportItemBoxCollision(coll_obj);
                Destroy(gameObject);
            }
        }
    }
}
