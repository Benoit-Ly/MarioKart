﻿using UnityEngine;
using System.Collections;

public class camPlayer : MonoBehaviour {
    
        public Transform target;
        public float distance = 20.0f;
        public float height = 20.0f;
        public float heightDamping = 2.0f;
        public float rotationDamping = 3.0f;

        // Use this for initialization
        void Start() { 

        }

        // Update is called once per frame
        void FixedUpdate() {
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
}
