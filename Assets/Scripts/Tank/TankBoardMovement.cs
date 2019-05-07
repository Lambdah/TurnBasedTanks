using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBoardMovement : MonoBehaviour {
    
    
    public int x_location;
    public int y_location;
    public int movement = 4;
    public Node currNode;
    public Node targetNode;
    public List<Node> TankPath;
    public bool wait = false;
    public bool turnFinished = false;
    PathFind path = new PathFind();
    
    Queue<Node> moveableTiles;
    // List<Node> moveableTiles;
    Graph graph;
    // float speed = 1.0f;
    float startTime;
    float nodeDiff;
    Node target;
    MoveScript moveScript;
    TankFire tankFire;
    bool move = false;
    bool generatePath = true;
    bool fire = false;
    

    private void Awake()
    {
        moveScript = GetComponent<MoveScript>();
        tankFire = GetComponent<TankFire>();
        graph = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<Graph>();
    }

    private void Start()
    {
        
    }


    // Notes: Still have to debug the BFS, maybe switch up Dijistrka's to A*.
    // BFS shows less tiles and is a speed slower than Dijistrika's algorithm.
    private void CreatePath()
    {
        // Debug.Log("curr node " + currNode.ToString());
        path.CreatePaths(graph, currNode);
        moveableTiles = path.s;
        // moveableTiles = path.FindTargeteableGrid(graph, currNode, movement);
        // Debug.Log("Moveable tiles");
        foreach (Node k in moveableTiles)
        {
            // GameObject moveable = k.tile.transform.GetChild(1).gameObject;
            // moveable.SetActive(true);

            if (path.NodeCost(k) < movement && k != currNode)
            {
                //Debug.Log(k.ToString());
                GameObject moveable = k.tile.transform.GetChild(1).gameObject;
                moveable.SetActive(true);
            }

        }
    }

    private void DeactivePath()
    {
        foreach(Node k in moveableTiles)
        {
            GameObject moveable = k.tile.transform.GetChild(1).gameObject;
            moveable.SetActive(false);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Node")
        {
            //currNode = other.gameObject.GetComponent<Node>();
            //x_location = currNode.posX;
            //y_location = currNode.posY;
            GameObject terrain = currNode.transform.GetChild(1).gameObject;
            terrain.SetActive(false);
            
        }
    }

    public void TankCurrentNodePosition(Node node)
    {
        x_location = node.posX;
        y_location = node.posY;

        currNode = node;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (wait)
        {
            // Locking
            generatePath = true;
        }
        else
        {
            // Left click with mouse
            if (generatePath)
            {
                CreatePath();
                generatePath = false;
            }
            if (Input.GetMouseButtonUp(0) && generatePath == false) // TankPath != null
            {
                moveScript.TargetMove(TankPath);
                DeactivePath();
                move = true;
            }
            if (move)
            {
                if (!moveScript.Move())
                {
                    move = false;
                    TankPath.Clear();
                    fire = true;   
                }
            }
            if (fire)
            {
                
                if (tankFire.FireTank(0.5f))
                {
                    fire = false;
                    StartCoroutine("ExecuteAfterTime", 0.7f);
                }
                
            }
        }
        


       
    }

    
    public void GridLocationMove(Node node)
    {


        GameObject select = node.tile.transform.GetChild(1).gameObject;
        if (select.activeSelf)
        {
            List<Node> tankPath = path.PathWay(node);
            TankPath = tankPath;
        }
        else
        {
            TankPath = null;
        }

    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        turnFinished = true;

    }





}
