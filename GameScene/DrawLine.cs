using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour {

    List<Vector3> points = new List<Vector3>();
    LineRenderer lr;
    Camera thisCamera;
    Vector3 lastPos = Vector3.zero;

    bool canDraw;

    /*[SerializeField]
    private float drawingTime;
    private float drawingTimeCnt;*/

    public int drawChance;
    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    public float threshold = 0.1f;
    public CharacterWalk player;

    public static Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };
    public Action<bool> NoInkLeft = delegate { };
    public static Action<bool> TutorialNoInkLeft = delegate { };

    const string LINE_COLOUR = "LineColour";

    AccessoryManager am;

    void Awake()
    {
        thisCamera = Camera.main;
    }

    private void Start()
    {
        lr = GetComponent<LineRenderer>();

        if (PlayerPrefs.HasKey(LINE_COLOUR))
        {
            GetLineColour();
        }else
        {
            lr.startColor = Color.black;
            lr.endColor = Color.black;
        }

        am = AccessoryManager.instance;
        drawChance = 3;

        if(am != null)
        {
            foreach (Accessory accessory in am.equippedAccessory)
            {
                drawChance += accessory.drawBonus;
            }
        }

        PlayerStatus.OnHurt += RemoveLine;
        canDraw = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 && !player.isWalking)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPos = touch.position;
            //the z pos of the mouse will be at the nearest to the camera so the line will always be on top of everything
            touchPos.z = thisCamera.nearClipPlane;
            Vector3 touchWorld = thisCamera.ScreenToWorldPoint(touchPos);

            LayerMask maskLayer = 1 << LayerMask.NameToLayer("DrawLayer");
            LayerMask playerLayer = 1 << LayerMask.NameToLayer("PlayerLayer");

            LayerMask drawLayer = maskLayer | playerLayer;


            RaycastHit2D hitInfo = Physics2D.Raycast(touchWorld, Vector3.zero, Mathf.Infinity, drawLayer);

            if (hitInfo.collider != null)
            {

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (hitInfo.collider.tag == "Player")
                    {
                        canDraw = true;
                    }

                    if (canDraw)
                    {
                        float dist = Vector3.Distance(lastPos, touchWorld);

                        if (dist <= threshold)
                        {
                            return;
                        }

                        lastPos = touchWorld;

                        if (points == null)
                        {
                            points = new List<Vector3>();
                        }

                        points.Add(touchWorld);
                        UpdateLine();
                    }
            
                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    if(canDraw)
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
                            RemoveLine(true);
                            canDraw = false;
                            return;
                            //TutorialNoInkLeft(true);
                        }

                        OnNewPathCreated(points);
                        RemoveLine(true);
                        lastPos = Vector3.zero;

                        canDraw = false;
                    }

                }

            }
            else //the line will be canceled if player draws outside of the board
            {
                RemoveLine(true);
                lastPos = Vector3.zero;
                canDraw = false;
            }

        }
    }


    void UpdateLine()
    {
        lr.startWidth = startWidth;
        lr.endWidth = endWidth;

        lr.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }

    void RemoveLine(bool b)
    {
        points.Clear();
        UpdateLine();
    }

    void TutorialNoInk()
    {
        if (TutorialLevelManager.instance.tutorialLevel > 0)
        {
            drawChance = 0;
            TutorialNoInkLeft(true);
            RemoveLine(true);
            return;
        }

        OnNewPathCreated(points);
        RemoveLine(true);
        lastPos = Vector3.zero;
    }

    private void OnDestroy()
    {
        PlayerStatus.OnHurt -= RemoveLine;
    }

    void GetLineColour()
    {
        string lineColour = PlayerPrefs.GetString(LINE_COLOUR);
        
        switch(lineColour)
        {
            case "red":
                lr.startColor = new Color(1, 0.3f, 0, 1);
                lr.endColor = new Color(1, 0.3f, 0, 1);
                break;
            case "blue":
                lr.startColor = new Color(0.3f, 0.8f, 1, 1);
                lr.endColor = new Color(0.3f, 0.8f, 1, 1);
                break;
            case "yellow":
                lr.startColor = new Color(1, 1, 0, 1);
                lr.endColor = new Color(1, 1, 0, 1);
                break;
            case "pink":
                lr.startColor = new Color(1, 0.6f, 1, 1);
                lr.endColor = new Color(1, 0.6f, 1, 1);
                break;
            case "green":
                lr.startColor = new Color(0.5f, 1, 0.5f, 1);
                lr.endColor = new Color(0.5f, 1, 0.5f, 1);
                break;
            case "purple":
                lr.startColor = new Color(0.6f, 0.3f, 0, 1);
                lr.endColor = new Color(0.6f, 0.3f, 0, 1);
                break;
            case "white":
                lr.startColor = Color.white;
                lr.endColor = Color.white;
                break;
            case "black":
                lr.startColor = Color.black;
                lr.endColor = Color.black;
                break;
            default:
                lr.startColor = Color.black;
                lr.endColor = Color.black;
                break;

        }
    }
}
