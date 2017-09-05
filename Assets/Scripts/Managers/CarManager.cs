using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CarManager : Manager {

    public event Action OnPlayerLoad;
    public event Action<int> OnCountdown;
    public event Action OnRaceStart;
    public event Action OnSortPosition;
    public event Action OnRegisterPlayerLap;
    public event Action OnRaceFinish;

    static private CarManager p_instance = null;
    static public CarManager Instance
    {
        get
        {
            return p_instance;
        }
    }

    [SerializeField]
    int timerBeforeStart = 3;

    List<Car> cars = new List<Car>();
    List<Car> carPositions = new List<Car>();
    Comparison<Car> positionComparer = new Comparison<Car>(CompareDistance);
    private Car player = null;
    public Car Player
    {
        get { return player; }
    }

    void Awake()
    {
        if (CarManager.Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        p_instance = this;

    }

	// Use this for initialization
	void Start () {
        LevelManager.Instance.OnLoadLevel += initCountdown;
        StageManager.Instance.OnEnterCheckpoint += enterCheckpoint;
        StageManager.Instance.OnCrossWaypoint += crossWaypoint;
        ItemManager.Instance.OnGetItem += assignItem;
        StageManager.Instance.OnCrossFinishLine += crossFinishLine;
	}

    void initCountdown()
    {
        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        bool hasStarted = false;

        while (!hasStarted)
        {
            yield return new WaitForSeconds(1.0f);
            
            if (OnCountdown != null)
                OnCountdown(timerBeforeStart);

            if (timerBeforeStart > 0)
            {
                timerBeforeStart -= 1;
            }
            else
            {
                enablePlayerControls(true);
                enableIAControls(true);

                if (OnRaceStart != null)
                    OnRaceStart();

                hasStarted = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        sortPositions();
	}

    void enablePlayerControls(bool enable)
    {
        if (player == null)
            return;

        player.GetComponent<InputMgr>().enabled = enable;
        player.GetComponent<CarUserControl>().enabled = enable;
    }

    void enableAutoPilotToPlayer(bool enable)
    {
        IAController auto_pilot = player.GetComponent<IAController>();

        if (auto_pilot == null)
            auto_pilot = player.gameObject.AddComponent<IAController>();

        auto_pilot.enabled = enable;

    }

    void enableIAControls(bool enable)
    {
        for(int index = 0; index < cars.Count; index++)
        {
            if (cars[index].CompareTag("Enemy"))
            {
                IAController controller = cars[index].GetComponent<IAController>();
                controller.enabled = enable;
            }
            
        }
    }

    void sortPositions()
    {
        carPositions.Sort(positionComparer);

        for(int index = 0; index < carPositions.Count; index++)
        {
            carPositions[index].Position = index + 1;
        }

        if (OnSortPosition != null)
            OnSortPosition();
    }

    public static int CompareDistance(Car c1, Car c2)
    {

        int compare = -1 * c1.Lap.CompareTo(c2.Lap);

        if (compare == 0)
        {
            compare = -1 * c1.Waypt.CompareTo(c2.Waypt);

            if (compare == 0)
                compare = c1.Distance.CompareTo(c2.Distance);
        }

        return compare;
    }

    public void addCar(Car new_car, bool is_player = false)
    {
        if (new_car == null)
            return;

        if (cars.IndexOf(new_car) == -1)
        {
            new_car.Id = cars.Count;
            cars.Add(new_car);
            carPositions.Add(new_car);
        }

        if (is_player)
        {
            player = new_car;

            enableAutoPilotToPlayer(false);
            enablePlayerControls(false);
            
            if (OnPlayerLoad != null)
                OnPlayerLoad();
        }
    }

    void resetCarList()
    {
        cars.Clear();
    }

    public bool isPlayer(int car_id)
    {
        if (player == null)
            return false;

        return (player.Id == car_id);
    }

    void enterCheckpoint(int checkpoint_id, int car_id)
    {
        if (car_id < 0 || car_id >= cars.Count)
            return;

        if (cars[car_id].CurrentCheckpoint == checkpoint_id)
            cars[car_id].addCheckpoint();
    }

    void crossWaypoint(int waypoint_id, int next_waypoint, int car_id)
    {
        if (car_id < 0 || car_id >= cars.Count)
            return;

        if (cars[car_id].CurrentWaypoint == waypoint_id)
            cars[car_id].CurrentWaypoint = next_waypoint;

        
    }

    void crossFinishLine(int car_id)
    {
        if (car_id < 0 || car_id >= cars.Count)
            return;

        if (cars[car_id].CurrentCheckpoint == StageManager.Instance.nbCheckpoints)
            cars[car_id].addLap();

        if (car_id == player.Id)
            OnRegisterPlayerLap();
    }

    public void finishRace()
    {
        enableAutoPilotToPlayer(true);
        enablePlayerControls(false);

        if (OnRaceFinish != null)
            OnRaceFinish();
    }

    void assignItem(GameObject go, GameObject item)
    {
        if (go == null || item == null)
            return;

        Car car = go.GetComponent<Car>();

        if (car != null)
        {
            car.Possessed_item = item.GetComponent<Items>();
        }
    }

    void OnDestroy()
    {
        StageManager.Instance.OnEnterCheckpoint -= enterCheckpoint;
        StageManager.Instance.OnCrossWaypoint -= crossWaypoint;
        ItemManager.Instance.OnGetItem -= assignItem;
        StageManager.Instance.OnCrossFinishLine -= crossFinishLine;
    }
}
