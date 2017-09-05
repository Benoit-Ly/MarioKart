using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

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
            GameObject coll_obj = coll.gameObject;

            if (coll_obj != null && coll_obj.layer == LayerMask.NameToLayer("Player"))
            {
                GameObject target = Tools.getObjectRoot(coll_obj);

                if (target != null)
                {
                    Car target_car = target.GetComponent<Car>();
                    StageManager.Instance.crossFinishLine(target_car.Id);
                }
            }
        }
    }
}
