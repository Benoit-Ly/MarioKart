using UnityEngine;
using System.Collections;

public class Mushroom : Items {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public override void Update () {
        updatePosition();
        base.Update();
        dropItem();
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
                        car.IsBoost = true;
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}
