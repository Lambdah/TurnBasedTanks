using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // PathFind path = new PathFind();
    public GameObject tank;
    public GameObject boardManager;
    public GameObject soundManager;
    [HideInInspector]public CameraFollow camera;
    [HideInInspector]public int numPlayers = 2;
    [HideInInspector]public int counter = 0;
    [HideInInspector]public TankBoardMovement[] player;
    TankFire[] tankFire;
    Graph g;
    Queue<Node> moveableTiles;
    Node[] startPosn;
    float speed;
    Node currPos;
    


    // Attempting to instantiate the Board and reference the nodes created gives a nullreference error
    public void Start()
    {
        
        g = boardManager.GetComponent<Graph>();
        Vector3 posTank = g.graph[0,0].tile.transform.position;
        GameObject []toInstantiate = new GameObject[numPlayers];
        player = new TankBoardMovement[numPlayers];
        startPosn = new Node[numPlayers];
        tankFire = new TankFire[numPlayers];
        startPosn[0] = g.graph[0, 0];
        startPosn[1] = g.graph[g.row - 1, g.column - 1];
        for (int i=0; i < numPlayers; i++)
        {
            toInstantiate[i] =
                Instantiate(tank, startPosn[i].tile.transform.position, Quaternion.identity) as GameObject;
            player[i] = toInstantiate[i].GetComponent<TankBoardMovement>();
            player[i].TankCurrentNodePosition(startPosn[i]);
            player[i].wait = true;
            tankFire[i] = toInstantiate[i].GetComponent<TankFire>();
            tankFire[i].wait = true;
        }

        // Setting up the shootable targets. Have to switch shootabletargets to be an array and load with all targets.
        // Before I can do this I have to add a button that allows for switching targets
        tankFire[0].shootableTargets = toInstantiate[1].transform;
        tankFire[1].shootableTargets = toInstantiate[0].transform;

        tankFire[0].shootableObject = toInstantiate[1];
        tankFire[1].shootableObject = toInstantiate[0];

        // Camera set up to follow the current player
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        camera.SetUpCamera(toInstantiate);
        camera.ChangePlayer(0);
        counter = 0;
    }

    // Not properly changing players
    public void Update()
    {
        player[counter].wait = false;
        if (player[counter].turnFinished){
            player[counter].wait = true;
            player[counter].turnFinished = false;
            counter = (counter + 1) % numPlayers;
            camera.ChangePlayer(counter);
              
        }
        
        
    }

    

    
 
}