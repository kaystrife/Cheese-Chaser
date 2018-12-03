using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : MonoBehaviour {

    float moveSpeed = 1.0f;
    Vector3 destination = new Vector3();
    bool isWalking;
    bool hitWall;
    float turnAngle;
    GameObject wall;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {

        isWalking = false;
        hitWall = false;
        rb = GetComponent<Rigidbody2D>();
        //determine a direction first
        GetNewPosition();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(isWalking)
        {
            Move();
        }

        if(hitWall)
        {
            Repel();
        }
	}

    void GetNewPosition()
    {
        int randomDir = Random.Range(0, 4);
        int randomDis = Random.Range(5, 10);

        transform.Rotate(0f, 0f, turnAngle);

        switch(randomDir)
        {
            case 0: //up
                destination = new Vector3(transform.position.x, transform.position.y + randomDis, 0f);
                turnAngle = 0f;
                break;
            case 1: //right
                destination = new Vector3(transform.position.x + randomDis, transform.position.y, 0f);
                transform.Rotate(0f, 0f, -90f);
                turnAngle = 90f;
                break;
            case 2: //down
                destination = new Vector3(transform.position.x, transform.position.y - randomDis, 0f);
                transform.Rotate(0f, 0f, 180f);
                turnAngle = -180f;
                break;
            case 3: //left
                destination = new Vector3(transform.position.x - randomDis, transform.position.y, 0f);
                transform.Rotate(0f, 0f, 90f);
                turnAngle = -90f;
                break;
        }

        isWalking = true;
    }

    void Move()
    {
        Vector3 currentPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        float remainingDistance = (transform.position - destination).sqrMagnitude;

        if(remainingDistance <= float.Epsilon)
        {
            isWalking = false;
            GetNewPosition();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Box" || collision.transform.tag=="Enemy" || collision.transform.tag == "Player")
        {
            isWalking = false;
            hitWall = true;
            wall = collision.gameObject;
        }
    }

    void Repel()
    {
        if (wall != null)
        {
            Vector2 direction = (transform.position - wall.transform.position).normalized;
            rb.AddForce(direction * 1000);
            

            if (Vector2.Distance(transform.position, wall.transform.position) >= 1.5f)
            {
                rb.Sleep();
                hitWall = false;
                wall = null;
                GetNewPosition();
            }
        }

    }


}
