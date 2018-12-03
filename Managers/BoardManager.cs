using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    #region singleton
    public static BoardManager instance = null;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }

        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    //can change according to the size of the game board
    int columns = 8;
    int rows = 10;
    Count blockCount = new Count(10, 13);
    Count cheeseCount = new Count(2, 7);


    public GameObject exit;
    public GameObject[] cheese;
    public GameObject[] floorTiles;
    public GameObject[] blockTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] poison;
    public GameObject[] enemy;
    public GameObject mask;
    //public GameObject[] specialItems;
    public int cheeseNum;

    [SerializeField]
    private float speedUpTime;

    //instantiates will be children of this boardHolder
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    //positions where blocks can be generated
    void InitializeList()
    {
        gridPositions.Clear();
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 2; y < rows - 2; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    //for the setup of outer wall and floor
    void BoardSetUp()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < columns+1; x++)
        {
            for (int y = -1; y < rows +1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                //positions where outer wall should be placed instead of floor
                if(x==-1 || x==columns || y==-1 || y==rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                var clone = (GameObject)Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                clone.transform.SetParent(boardHolder);
            }
        }
    }

    //for random positions of cheese/blocks/special items
    Vector3 RandomPos()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPos = gridPositions[randomIndex];

        //remove one available position for random objects so that they will not be overlapping
        gridPositions.RemoveAt(randomIndex);

        return randomPos;
    }

    int GenObjectsRandomly(GameObject[] tileArray, int min, int max)
    {
        int objectCount = Random.Range(min, max);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPos = RandomPos();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPos, Quaternion.identity);
        }

        return objectCount;
    }

    //will be called by GameManager when it is time to set up the board
    public void SetupScene(int level)
    {
        //set up outer walls and floor
        BoardSetUp();
        //initialize positions for random objects
        InitializeList();
        //gen random objects
        GenObjectsRandomly(blockTiles, blockCount.minimum, blockCount.maximum);
        //gen cheese
        cheeseNum = GenObjectsRandomly(cheese, cheeseCount.minimum, cheeseCount.maximum);


        //gen enemies
        int maxEnemyNum = 2 + (int)(level*0.1f);

        if(maxEnemyNum>4)
        {
            maxEnemyNum = 4;
        }
        GenObjectsRandomly(enemy, 1, maxEnemyNum);
       
        //gen poisons
        if(level>=10)
        {
            int maxPoisonCount = (int)(level*0.1f);

            if (maxPoisonCount > 4)
            {
                maxPoisonCount = 4;
            }

            GenObjectsRandomly(poison, 1, maxPoisonCount+1);
        }

        //gen exit
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0), Quaternion.identity);

    }

}
