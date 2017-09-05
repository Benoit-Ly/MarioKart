using UnityEngine;
using System.Collections;

public class RaceCamera : MonoBehaviour {

    Transform target;
    [SerializeField]
    float distance = 20.0f;
    [SerializeField]
    float height = 20.0f;
    [SerializeField]
    float heightDamping = 2.0f;
    [SerializeField]
    float rotationDamping = 3.0f;

    // Use this for initialization
    void Awake () {
        CarManager.Instance.OnPlayerLoad += setPlayerAsTarget;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target)
            return;

        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        transform.LookAt(target);
    }

    void setPlayerAsTarget()
    {
        target = CarManager.Instance.Player.transform;
    }

    void OnDestroy()
    {
        CarManager.Instance.OnPlayerLoad -= setPlayerAsTarget;
    }
}
