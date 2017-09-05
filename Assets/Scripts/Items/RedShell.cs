using UnityEngine;
using System.Collections;

public class RedShell : Items {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (!isLaunch)
            return;

        if (col != null)
        {
            GameObject target = col.gameObject;
            if (target != null)
            {
                if (target.CompareTag("Player") || target.CompareTag("Enemy"))
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
