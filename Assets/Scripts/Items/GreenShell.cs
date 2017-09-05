using UnityEngine;
using System.Collections;

public class GreenShell : Items {

    Rigidbody rigidbody;
    Vector3 direction = Vector3.zero;

    Ray rayFront;
    Ray rayLeft;
    Ray rayRight;
    Ray rayFLeft;
    Ray rayFRight;
    Ray rayBack;
    Ray rayBLeft;
    Ray rayBRight;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        RayInit();
        ray = new Ray(transform.position, Vector3.down);
    }


    void RayInit()
    {
        rayFront = new Ray(transform.position, transform.forward);
        rayFLeft = new Ray(transform.position, transform.forward - transform.right);
        rayFRight = new Ray(transform.position, transform.forward + transform.right);
        rayLeft = new Ray(transform.position, -transform.right);
        rayRight = new Ray(transform.position, transform.right);
        rayBack = new Ray(transform.position, -transform.forward);
        rayBLeft = new Ray(transform.position, -transform.forward - transform.right);
        rayBRight = new Ray(transform.position, -transform.forward + transform.right);
    }

    // Update is called once per frame
   public override void Update() {
        updatePosition();
        dropItem();
        DestroyIfOutOfBound();
        Raytrace();

        if (!isLaunch)
            updateDirection();
    }


    void Raytrace()
    {
        rayFront.origin = transform.position;
        rayFront.direction = transform.forward;
        rayBack.origin = transform.position;
        rayBack.direction = -transform.forward;

        rayLeft.origin = transform.position;
        rayLeft.direction = -transform.right;
        rayFLeft.origin = transform.position;
        rayFLeft.direction = transform.forward - transform.right;
        rayBLeft.origin = transform.position;
        rayBLeft.direction = -transform.forward - transform.right;

        rayRight.origin = transform.position;
        rayRight.direction = transform.right;
        rayFRight.origin = transform.position;
        rayFRight.direction = transform.forward + transform.right;
        rayBRight.origin = transform.position;
        rayBRight.direction = -transform.forward + transform.right;

        RaycastHit hit;
        RaycastHit BHit;
        RaycastHit leftHit;
        RaycastHit rightHit;
        RaycastHit leftFHit;
        RaycastHit rightFHit;
        RaycastHit leftBHit;
        RaycastHit rightBHit;

        Physics.Raycast(rayLeft, out leftHit);
        Physics.Raycast(rayRight, out rightHit);
        Physics.Raycast(rayFLeft, out leftFHit);
        Physics.Raycast(rayFRight, out rightFHit);
        Physics.Raycast(rayBLeft, out leftBHit);
        Physics.Raycast(rayBRight, out rightBHit);
        Physics.Raycast(rayFront, out hit);
        Physics.Raycast(rayBack, out BHit);


        if (hit.distance < 1f || BHit.distance < 1f || leftHit.distance < 1f || rightHit.distance < 1f || leftFHit.distance < 1f || rightFHit.distance < 1f || leftBHit.distance < 1f || rightBHit.distance < 1f)
        {
            if ((leftHit.distance < rightHit.distance || leftFHit.distance < rightFHit.distance) && (leftBHit.distance > leftFHit.distance))
            { 
                transform.Rotate(Vector3.up, 45f);
                direction = transform.forward;
            }
            else if ((leftHit.distance > rightHit.distance || leftFHit.distance > rightFHit.distance) && (rightBHit.distance > rightFHit.distance))
            {
                transform.Rotate(Vector3.up, -45f);
                direction = transform.forward;
            }
            else if ((leftHit.distance < rightHit.distance || leftBHit.distance < rightBHit.distance) && (leftBHit.distance < leftFHit.distance))
            {
                transform.Rotate(Vector3.up, -225f);
                direction = transform.forward;
            }
            else if ((leftHit.distance > rightHit.distance || leftBHit.distance > rightBHit.distance) && (rightBHit.distance < rightFHit.distance))
            {
                transform.Rotate(Vector3.up, 225f);
                direction = transform.forward;
            }
            else if(BHit.distance < hit.distance)
            {
                transform.Rotate(Vector3.up, 180f);
                direction = transform.forward;
            }
        }
    }

    void FixedUpdate()
    {
        if (isLaunch)
            moveForward();
    }
    
    void moveForward()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void updateDirection()
    {
        direction = owner.transform.forward;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!isLaunch)
            return;

        if (col != null)
        {
            GameObject target = col.gameObject;
            if (target != null)
            {
                if (target.layer == LayerMask.NameToLayer("Player"))
                {
                    Car car = target.GetComponent<Car>();
                    if (car != null)
                    {
                        car.IsTouch = true;
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
