using UnityEngine;
using System.Collections;

public class IAController : MonoBehaviour {

    Car car;
    CarController controller;

    Vector3 direction;
    Quaternion target_rotation;

    float brake = 0f;
    float accel = 1f;

    float target_lane = 0f;
    float current_lane = 0f;
    float lane_change_delay = 5.0f;
    float lane_step = 0.0f;

    void Awake()
    {
        enabled = false;
    }

    // Use this for initialization
    void Start () {
        car = GetComponent<Car>();
        controller = GetComponent<CarController>();
        target_rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        changeLane();
        lookAtWaypoint();
	}

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target_rotation, 4.5f * Time.fixedDeltaTime);
        controller.Move(0f, accel, brake, 0f);
    }

    void lookAtWaypoint()
    {
        Waypoint waypoint = StageManager.Instance.getWaypoint(car.CurrentWaypoint);

        Vector3 waypoint_pos = waypoint.transform.TransformPoint(new Vector3(current_lane / 2f, 0f, 0f));

        direction = waypoint_pos - transform.position;
        target_rotation = Quaternion.LookRotation(direction);
        target_rotation.x = transform.rotation.x;

        float angle = Quaternion.Angle(transform.rotation, target_rotation);

        if (angle >= 10f)
        {
            brake = 0.1f;
            accel = 0.8f;
        }
        else
        {
            brake = 0f;
            accel = 1f;
        }
    }

    void changeLane()
    {
        lane_change_delay -= Time.deltaTime;

        if (lane_change_delay <= 0f)
        {
            lane_step = 0f; ;
            target_lane = Random.Range(-1f, 1f);
            lane_change_delay = 5f;
        }

        current_lane = Mathf.Lerp(current_lane, target_lane, lane_step);
        lane_step += 0.1f;
    }
}
