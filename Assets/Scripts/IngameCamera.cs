using UnityEngine;
using System.Collections;

public class IngameCamera : MonoBehaviour {

    GameObject player;

    [SerializeField]
    float speed = 1.0f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    if (player != null)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, player.transform.position, speed * Time.deltaTime);
            Camera.main.transform.rotation = player.transform.rotation;
        }
	}
}
