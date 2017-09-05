using UnityEngine;
using System.Collections;

public class Pit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll != null)
        {
            GameObject target = coll.gameObject;

            if (target.CompareTag("Player"))
            {
                //PlayerManager.Instance.destroyPlayer();
            }
        }
    }
}
