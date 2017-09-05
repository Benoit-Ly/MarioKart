using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    int id = 0;
    public int Id { get { return id; } set { id = value; } }

	void Start()
    {
        StageManager.Instance.addWaypoint(this);
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
                    StageManager.Instance.reportWaypointCollision(id, target_car.Id);
                }
            }
        }
    }

}
