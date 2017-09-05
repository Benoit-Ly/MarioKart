using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Items : MonoBehaviour
{
    Vector3 Target = new Vector3();
    float firingAngle = 45f;
    float gravity = 9f;

    Transform Projectile;
    private Transform myTransform;

    [SerializeField]
    protected float speed = 2f;
    protected GameObject owner;
    protected bool isLaunch = false;

    bool isRespawning = false;

    protected Transform Player;
    Items curItem;
    int index;
    [SerializeField]
    bool debugMode = false;
    [SerializeField]
    int debugItemIndex = 0;
    [SerializeField]
    List<Items> items;
    protected bool HasItem = false;
    protected Ray ray;

    enum e_Action
    {
        SPIN,
        BLOW,
        DESTROY
    };

    [SerializeField]
    e_Action actionOnHit;


    void Awake()
    {
        myTransform = transform;
    }


    // Use this for initialization
    void Start()
    {
        ray = new Ray(transform.position, Vector3.down);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("launchPos").transform;
        DestroyIfOutOfBound();
    }


    protected void DestroyIfOutOfBound()
    {
        ray.origin = transform.position;
        ray.direction = Vector3.down;
        Debug.DrawRay(ray.origin, ray.direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Respawn") && hit.distance < 2f && isLaunch)
                Destroy(gameObject);
        }
    }

    void RandItem()
    {
        if (!debugMode)
            index = Random.Range(0, items.Count);
        else
            index = debugItemIndex;

        if (index < items.Count)
            curItem = items[index];
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.layer == LayerMask.NameToLayer("Player") && !HasItem && !isRespawning)
        {
            if (col.gameObject.name == "Enemy")
            {
                ItemManager.Instance.RespawnAt(transform.position);
                Destroy(gameObject);
                return;
            }
            GameObject item_obj_prefab = ItemManager.Instance.getRandomItem();
            GameObject item_obj = Instantiate(item_obj_prefab, Player.position, Quaternion.identity) as GameObject;
            Items item_inst = item_obj.GetComponent<Items>();
            
            item_inst.setOwner(col.gameObject);
            HasItem = true;

            ItemManager.Instance.RespawnAt(transform.position);
            Destroy(gameObject);
            isRespawning = true;
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Player") && HasItem && !isRespawning)
        {
            ItemManager.Instance.RespawnAt(transform.position);
            Destroy(gameObject);

            isRespawning = true;
        }
    }

    void FindWhichItem()
    {
        if (this.gameObject.name == "MisteryCube(Clone)")
            this.gameObject.name = "MisteryCube";

        if (this.gameObject.name == "Banana(Clone)")
            this.gameObject.name = "Banana";
    }

    protected void updatePosition()
    {
        if (owner != null && !isLaunch) 
        {
            transform.position = GameObject.FindGameObjectWithTag("PosItem").transform.position;
        }
    }
        
    public void setOwner(GameObject new_owner)
    {
        if (new_owner != null)
        owner = new_owner;
    }

    protected virtual void dropItem()
    {
        if (Input.GetKeyDown("space"))
        {
            if (this.gameObject.name == "Mushroom(Clone)" || this.gameObject.name == "Yellow star(Clone)" )
            {
                transform.position = GameObject.FindWithTag("FrontPos").transform.position;
                isLaunch = true;
                HasItem = false;
            }
            else if (this.gameObject.name == "MisteryCube(Clone)" || this.gameObject.name == "Banana(Clone)")
            {
                StartCoroutine(LaunchObject(Player));
                FindWhichItem();
                isLaunch = true;
                HasItem = false;
            }
            else
            {
                transform.position += Time.deltaTime * speed * transform.forward;
                isLaunch = true;
                HasItem = false;
            }
        }

    }

    public int getFeedBackType()
    {
        return (int)actionOnHit;
    }

    protected IEnumerator LaunchObject(Transform player)
    {
        Target = player.position + player.forward * 50f;

        float target_Distance = Vector3.Distance(myTransform.position, Target);
        float item_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);


        float Vx = Mathf.Sqrt(item_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(item_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = (target_Distance / Vx);

        RaycastHit rc;

        
        transform.rotation = Quaternion.LookRotation(Target - myTransform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            //Utilisation d'un raycast car le calcul n'est pas totalement bon
            Physics.Raycast(transform.position, Vector3.down, out rc, 10f);
            
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            elapse_time += Time.deltaTime;

            if (rc.distance <= 1)
                break;

            yield return null;

        }
          
    }
}

