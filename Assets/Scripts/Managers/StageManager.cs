using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StageManager : Manager {

    static private StageManager p_instance;
    static public StageManager Instance
    {
        get
        {
            return p_instance;
        }
    }

    public event Action<int, int> OnEnterCheckpoint;
    public event Action<int, int, int> OnCrossWaypoint;
    public event Action<int> OnCrossFinishLine;

    List<Checkpoint> checkpoints = new List<Checkpoint>();
    Comparison<Checkpoint> checkpointComparer = new Comparison<Checkpoint>(compareCheckpointSiblingIndex);
    public int nbCheckpoints { get { return checkpoints.Count; } }

    List<Waypoint> waypoints = new List<Waypoint>();
    Comparison<Waypoint> waypointComparer = new Comparison<Waypoint>(compareWaypointSiblingIndex);

    void Awake()
    {
        if (StageManager.Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        p_instance = this;
    }

	// Use this for initialization
	void Start () {
        //LevelManager.Instance.OnLoadLevel += resetCheckpoints;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public int compareCheckpointSiblingIndex(Checkpoint c1, Checkpoint c2)
    {
        if (c1 == null || c2 == null)
            return 0;

        return Tools.compareSiblingIndex(c1.transform, c2.transform);
    }

    static public int compareWaypointSiblingIndex(Waypoint w1, Waypoint w2)
    {
        if (w1 == null || w2 == null)
            return 0;

        return Tools.compareSiblingIndex(w1.transform, w2.transform);
    }

    public void addWaypoint(Waypoint new_waypoint)
    {
        if (new_waypoint == null)
            return;

        if (waypoints.IndexOf(new_waypoint) == -1)
        {
            waypoints.Add(new_waypoint);
            waypoints.Sort(waypointComparer);
            new_waypoint.Id = new_waypoint.transform.GetSiblingIndex();
        }
    }

    public Waypoint getWaypoint(int index)
    {
        if (index < 0 || index >= waypoints.Count)
            return null;

        return waypoints[index];
    }

    public void addCheckpoint(Checkpoint new_checkpoint)
    {
        if (new_checkpoint == null)
            return;

        if (checkpoints.IndexOf(new_checkpoint) == -1)
        {
            checkpoints.Add(new_checkpoint);
            checkpoints.Sort(checkpointComparer);
            new_checkpoint.Id = new_checkpoint.transform.GetSiblingIndex();
        }
    }

    void resetCheckpoints()
    {
        checkpoints.Clear();
    }

    public void enterCheckpoint(int id, int car_id)
    {
        if (id >= checkpoints.Count || id < 0)
            return;

        if (OnEnterCheckpoint != null)
            OnEnterCheckpoint(id, car_id);
    }

    public void reportWaypointCollision(int id, int car_id)
    {
        int next_waypoint = id + 1;
        if (next_waypoint == waypoints.Count)
            next_waypoint = 0;

        OnCrossWaypoint(id, next_waypoint, car_id);
    }

    public void crossFinishLine(int car_id)
    {
        OnCrossFinishLine(car_id);
    }

    public float getDistanceToWaypoint(Transform target, int index)
    {
        /*if (target == null || index >= waypoints.Count)
            return 0f;*/

        float distance = Vector3.Distance(target.position, waypoints[index].transform.position);

        return distance;
    }

    void OnDestroy()
    {
        LevelManager.Instance.OnLoadLevel -= resetCheckpoints;
    }
}
