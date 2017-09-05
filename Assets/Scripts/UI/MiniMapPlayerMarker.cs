using UnityEngine;
using System.Collections;

public class MiniMapPlayerMarker : MonoBehaviour {
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

	void Update () {
        if (player.activeInHierarchy)
        {
            transform.position = player.transform.position + new Vector3(0f, 100f, 0f);
            transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
        }
    }
}
