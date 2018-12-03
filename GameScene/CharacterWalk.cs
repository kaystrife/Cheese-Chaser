using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterWalk : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    private Queue<Vector3> pathPoints = new Queue<Vector3>();
    private Vector3 destination;

    public bool isWalking;
    bool hitWall;
    GameObject wall;

    Rigidbody2D rb;

    PlayerStatus status;
    AccessoryManager am;

    private void Start()
    {
        am = AccessoryManager.instance;
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
        destination = transform.position;

        DrawLine.OnNewPathCreated += SetPoints;


        hitWall = false;

        if(am!=null)
        {
            foreach(Accessory accessory in am.equippedAccessory)
            {
                moveSpeed += accessory.extraSpeed;
            }
        }


    }

    private void SetPoints(IEnumerable<Vector3> points)
    {
        pathPoints = new Queue<Vector3>(points);
    }

    private void Update()
    {

        if(pathPoints.Count>0)
        {
            UpdatePathing();
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            isWalking = true;
        }else
        {
            isWalking = false;
        }

        if(hitWall)
        {
            Repel();
        }
       
    }

    private void UpdatePathing()
    {
        if(ShouldSetDestination())
        {
            destination = pathPoints.Dequeue();
            destination = new Vector3(destination.x, destination.y, transform.position.z);
          
        }
    }

    private bool ShouldSetDestination()
    {
        float remainingDistance = (transform.position - destination).sqrMagnitude;

        if (remainingDistance > float.Epsilon)
        {
            return false;
        }

        return true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision != null)
        {
            pathPoints.Clear();
            wall = collision.gameObject;
            hitWall = true;

            //For Tutorial
            if (TutorialLifeManager.instance != null)
            {
                if (collision.transform.tag == "Enemy" && !TutorialLifeManager.instance.isHurt)
                {
                    TutorialLifeManager.instance.GetHurt(1);
                }

                return;
            }

            if (collision.transform.tag == "Enemy" && !status.isHurt)
            {
                status.GetHurt(-1);
            }
        }

    }

    void Repel()
    {
        if(wall!=null)
        {
            Vector2 direction = (transform.position - wall.transform.position).normalized;
            rb.AddForce(direction * 1000);
            isWalking = true;

            if (Vector2.Distance(transform.position, wall.transform.position) >= 1f)
            {
                rb.Sleep();
                hitWall = false;
                wall = null;
                destination = transform.position;
                isWalking = false;
            }
        }
       
    }

    private void OnEnable()
    {
        PlayerStatus.OnHurt += Stop;
    }

    private void OnDisable()
    {
        PlayerStatus.OnHurt -= Stop;
    }

    void Stop(bool b)
    {
        rb.Sleep();
        isWalking = false;
        pathPoints.Clear();
        destination = transform.position;
    }
}
