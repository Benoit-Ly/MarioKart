using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private int id = 0;
    public int Id { get { return id; } set { id = value; } }

	// Use this for initialization
	void Start () {
        StageManager.Instance.addCheckpoint(this);
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
                    StageManager.Instance.enterCheckpoint(id, target_car.Id);
                }
            }
        }
    }
}
