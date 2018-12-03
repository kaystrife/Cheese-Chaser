using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DrawLineBackUp : MonoBehaviour {


    List<Vector3> points = new List<Vector3>();
    LineRenderer lr;
    Camera thisCamera;
    Vector3 lastPos = Vector3.zero;
    //[SerializeField]
    //private float drawingTime;
    //private float drawingTimeCnt;

    public int drawChance;
    //public Text text;
    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    public float threshold = 0.001f;
    public CharacterWalk player;

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };
    public Action<bool> NoInkLeft = delegate { };

    public static Action<bool> TutorialNoInkLeft = delegate { };


    AccessoryManager am;

    void Awake()
    {
        thisCamera = Camera.main;
        lr = GetComponent<LineRenderer>();

    }

    private void Start()
    {
        am = AccessoryManager.instance;
        drawChance = 3;

        if (am != null)
        {
            foreach (Accessory accessory in am.equippedAccessory)
            {
                drawChance += accessory.drawBonus;
            }
        }

        //text.text = "Draw Chance: " + drawChance;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !player.isWalking)
        {
            Vector3 mousePos = Input.mousePosition;
            //the z pos of the mouse will be at the nearest to the camera so the line will always be on top of everything
            mousePos.z = thisCamera.nearClipPlane;
            Vector3 mouseWorld = thisCamera.ScreenToWorldPoint(mousePos);

            LayerMask drawLayer = 1 << LayerMask.NameToLayer("DrawLayer");

            RaycastHit2D hitInfo = Physics2D.Raycast(mouseWorld, Vector3.forward, Mathf.Infinity, drawLayer);


            if (hitInfo.collider != null)
            {
                //drawingTimeCnt -= Time.deltaTime;

                float dist = Vector3.Distance(lastPos, mouseWorld);

                if (dist <= threshold)
                {
                    return;
                }

                lastPos = mouseWorld;

                if (points == null)
                {
                    points = new List<Vector3>();
                }

                points.Add(mouseWorld);
                UpdateLine();

            }
            else
            {
                //the line will be canceled if player draws outside of the board
                //OnNewPathCreated(points);
                RemoveLine();
                lastPos = Vector3.zero;
                //drawingTimeCnt = drawingTime;
            }

            /*if(drawingTimeCnt<=0)
            {
                OnNewPathCreated(points);
                RemoveLine();
                lastPos = Vector3.zero;
                //drawingTimeCnt = drawingTime;
            }*/

        }

        if (Input.GetMouseButtonUp(0))
        {
            drawChance--;

            if (drawChance < 0)
            {
                if (TutorialLevelManager.instance != null)
                {
                    TutorialNoInk();
                    return;
                }

                drawChance = 0;
                NoInkLeft(true);
                RemoveLine();
                return;
                //TutorialNoInkLeft(true);


            }

            OnNewPathCreated(points);
            RemoveLine();
            lastPos = Vector3.zero;

            //text.text = "Draw Chance: " + drawChance;
            //drawingTimeCnt = drawingTime;
        }

    }


    void UpdateLine()
    {
        lr.startWidth = startWidth;
        lr.endWidth = endWidth;
        lr.startColor = Color.black;
        lr.endColor = Color.black;
        lr.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }

    void RemoveLine()
    {
        points.Clear();
        lr.positionCount = 0;
    }

    void TutorialNoInk()
    {
        if (TutorialLevelManager.instance.tutorialLevel > 0)
        {
            drawChance = 0;
            TutorialNoInkLeft(true);
            RemoveLine();
            return;
        }

        OnNewPathCreated(points);
        RemoveLine();
        lastPos = Vector3.zero;
    }
}
