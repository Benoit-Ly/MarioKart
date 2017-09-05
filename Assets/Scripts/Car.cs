using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{

    int laps = 1;
    int waypoint = 0;
    int checkpoint = 0;
    float distance = 0f;

    CarController controller;

    bool isActive = false;
    bool isDizzy = false;
    bool isExplode = false;
    bool isBoost = false;
    bool isOverBoost = false;
    bool isTouch = false;

    public int Lap { get { return laps; } }
    public int Waypt { get { return waypoint; } }
    public float Distance { get { return distance; } }

    int position = 1;
    public int Position { get { return position; } set { position = value; } }

    bool finished = false;
    public bool HasFinished { get { return finished; } }

    public int CurrentCheckpoint { get { return checkpoint; } }
    public int CurrentWaypoint { get { return waypoint; } set { waypoint = value; } }

    [SerializeField]
    GameObject marker = null;

    private int id = 0;
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    Items possessed_item = null;
    public Items Possessed_item
    {
        get { return possessed_item; }
        set
        {
            possessed_item = value;
            possessed_item.setOwner(gameObject);
        }
    }

    public bool IsDizzy
    {
        get { return isDizzy; }
        set { isDizzy = value; }
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public bool IsExplode
    {
        get { return isExplode; }
        set { isExplode = value; }
    }

    public bool IsBoost
    {
        get { return isBoost; }
        set { isBoost = value; }
    }

    public bool IsTouch
    {
        get { return isTouch; }
        set { isTouch = value; }
    }

    public bool IsOverBoost
    {
        get { return isOverBoost; }
        set { isOverBoost = value; }
    }

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CarController>();

        if (gameObject.CompareTag("Player"))
        {
            CarManager.Instance.addCar(this, true);
        }
        else
            CarManager.Instance.addCar(this);

        GameObject mapMarker = (GameObject)Instantiate(marker, transform.position, Quaternion.identity);
        mapMarker.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        updateDistance();
        dizzy();
        explode();
        touch();
        boost();
        overBoosted();
    }

    public void addLap()
    {
        if (gameObject.CompareTag("Player") && laps == 3 && finished == false)
        {
            finished = true;
            CarManager.Instance.finishRace();
        }

        checkpoint = 0;
        laps++;
    }

    public void addCheckpoint()
    {
        checkpoint++;
    }

    void updateDistance()
    {
        if (waypoint >= 0)
        {
            distance = StageManager.Instance.getDistanceToWaypoint(transform, waypoint);
        }
        else
            distance = -1f * StageManager.Instance.getDistanceToWaypoint(transform, 0);

    }

    void dizzy()
    {
        if (IsDizzy && !IsActive)
        {
            controller.Speed -= 30;
            if (controller.Speed < 0)
                controller.Speed = 0;
            IsDizzy = false;
        }
    }

    void explode()
    {
        if (IsExplode && !isActive)
        {
            controller.Speed -= 60;
            if (controller.Speed < 0)
                controller.Speed = 0;
            IsExplode = false;
        }
    }

    void touch()
    {
        if (IsTouch && !isActive)
        {
            controller.Speed -= 80;
            if (controller.Speed < 0)
                controller.Speed = 0;
            IsTouch = false;
        }
    }

    void boost()
    {
        if (IsBoost)
        {
            if (controller.Speed < controller.MaxSpeed)
            {
                controller.Speed += 30;
            }
            IsBoost = false;
        }
    }

    void overBoosted()
    {
        if (IsOverBoost)
        {
            StartCoroutine(Boosted());
            IsOverBoost = false;
        }
    }
    IEnumerator Boosted()
    {
        if (controller.Speed < controller.MaxSpeed)
        {
            isActive = true;
            controller.Speed += 20;
            yield return new WaitForSeconds(2);
            controller.Speed += 15;
            yield return new WaitForSeconds(2);
            controller.Speed += 10;
            yield return new WaitForSeconds(5);

            isActive = false;

            yield return null;
        }
    }
}