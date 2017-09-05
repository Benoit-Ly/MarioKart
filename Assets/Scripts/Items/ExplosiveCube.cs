using UnityEngine;
using System.Collections;

public class ExplosiveCube : Items {

    
    // Use this for initialization
    void Start () {
        ray = new Ray(transform.position, Vector3.down);
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        updatePosition();
        dropItem();
        DestroyIfOutOfBound();
    }


    void OnCollisionEnter(Collision col)
    {
        if (col != null)
        {
            GameObject target = col.gameObject;
            if (target != null)
            {
                if (target.layer == LayerMask.NameToLayer("Player"))
                {
                    Car car = target.GetComponent<Car>();
                    if (car != null)
                    {
                        car.IsExplode = true;
                    }
                    Destroy(gameObject);
                }
            }
        }
    }


}
