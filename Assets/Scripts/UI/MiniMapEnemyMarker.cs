using UnityEngine;
using System.Collections;

public class MiniMapEnemyMarker : MonoBehaviour {
	void Update () {
        transform.position = transform.parent.gameObject.transform.position + new Vector3(0f,100f,0f);
        transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
    }
}
