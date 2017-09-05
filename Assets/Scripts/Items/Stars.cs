using UnityEngine;
using System.Collections;

public class Stars : Items {

    [SerializeField]
    GameObject starPrefab;

    Transform playerPosition;
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
                if (target.CompareTag("Player"))
                {
                    if (target.layer == LayerMask.NameToLayer("Player"))
                    {
                        Car car = target.GetComponent<Car>();
                        if (car != null)
                        {
                            car.IsOverBoost = true;
                        }
                        playerPosition = car.transform;
                        StartCoroutine(visualStar());
                        this.gameObject.GetComponent<BoxCollider>().enabled = false;
                        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
        }
    }

    IEnumerator visualStar()
    {
        GameObject item = Instantiate(starPrefab, playerPosition.position, Quaternion.identity) as GameObject;
        item.transform.parent = playerPosition;

        yield return new WaitForSeconds(9);
        Destroy(item);
        Destroy(this.gameObject);
    }
}
