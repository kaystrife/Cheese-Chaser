using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class PointerMove : MonoBehaviour {

    [SerializeField]
    Vector3[] points;
    List<Vector3> linePoints = new List<Vector3>();

    LineRenderer lr;

    Vector3 start, destination;
    Vector3 lastPos = Vector3.zero;

    int index;
    [SerializeField]
    float moveSpeed;

    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    public float threshold = 0.001f;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();

    }

    void Start () {

        start = transform.position;
        index = 0;
        destination = points[index];
    }

    private void Update()
    {
        //reach destination -> next destination
        if(CheckReachedDestination())
        {
            SetNewDestination();

        }else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        }

        if(index == points.Length-1 && CheckReachedDestination())
        {
            StartMove();
        }

        DrawLine();
    }

    void StartMove()
    {
        RemoveLine();
        index = 0;
        transform.position = start;
        destination = points[index];
    }

    void SetNewDestination()
    {
        index++;

        if (index < points.Length)
        {
            destination = points[index];
        }
    }

    bool CheckReachedDestination()
    {
        float remainingDistance = (transform.position - destination).sqrMagnitude;

        if(remainingDistance > float.Epsilon)
        {
            return false;
        }

        return true;
    }

    void DrawLine()
    {
        Vector3 pointerPos = transform.position;
        //pointerPos.z = -9.9f;

        float dist = Vector3.Distance(lastPos, pointerPos);

        if (dist <= threshold)
        {
            return;
        }

        lastPos = pointerPos;

        if (linePoints == null)
        {
            linePoints = new List<Vector3>();
        }

        linePoints.Add(pointerPos);

        UpdateLine();
    }

    void UpdateLine()
    {
        lr.startWidth = startWidth;
        lr.endWidth = endWidth;
        //lr.startColor = Color.yellow;
        //lr.endColor = Color.yellow;
        lr.positionCount = linePoints.Count;

        for (int i = 0; i < lr.positionCount; i++)
        {
            lr.SetPosition(i, linePoints[i]);
        }
    }

    void RemoveLine()
    {
        linePoints.Clear();
        lr.positionCount = 0;
    }
}
