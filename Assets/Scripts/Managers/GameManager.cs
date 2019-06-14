using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // PathFind path = new PathFind();
    public GameObject tankPrefab;
    public GameObject boardManager;
    public GameObject soundManager;
    public int numPlayers = 2;
    // Colors are set up in the inspector
    public Color[] m_playerColor;
    public TankManager[] m_tanks;
    [HideInInspector]public CameraFollow camera;
    [HideInInspector]public int counter = 0;
    [HideInInspector]public Node[] startPosn;
    BoardManager bm;
    Graph g;
    

    public void SpawnBoard()
    {
        bm = boardManager.GetComponent<BoardManager>();
        bm.SetupBoard();
    }

    public void SpawnTanks()
    {

        m_tanks = new TankManager[numPlayers];
        g = boardManager.GetComponent<Graph>();
        startPosn = new Node[numPlayers];
        startPosn[0] = g.graph[1, 1];
        startPosn[1] = g.graph[g.row - 2, g.column - 2];

        for (int i = 0; i < numPlayers; i++)
        {
            m_tanks[i] = new TankManager();
            m_tanks[i].m_instance = 
                Instantiate(tankPrefab, startPosn[i].tile.transform.position, Quaternion.identity) as GameObject;
            m_tanks[i].m_PlayerColor = m_playerColor[i];
            m_tanks[i].Setup();
            m_tanks[i].m_SpawnPoint = m_tanks[i].m_instance.transform;
            m_tanks[i].spawnPointNode(startPosn[i]);
            m_tanks[i].m_playerNumber = i;
            
        }

        // Setting up the targets
        for (int i = 0; i < numPlayers; i++)
        {
            for (int j = 0; j < numPlayers; j++)
            {
                if ((j + 1) != (i + 1)) m_tanks[i].setShootableTargets(m_tanks[j].m_instance.transform);
            }
        }

        counter = 0;
    }
    
    public void setUpCamera()
    {
        GameObject[] cameraFollowObjects = new GameObject[m_tanks.Length];
        for (int i = 0; i < m_tanks.Length; i++)
        {
            cameraFollowObjects[i] = m_tanks[i].m_instance;
        }
        // Camera set up to follow the current player
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        camera.SetUpCamera(cameraFollowObjects);
        camera.ChangePlayer(counter);
    }
        
    
    public void Start()
    {
        SpawnBoard();
        SpawnTanks();
        setUpCamera();
        
    }

    public void Update()
    {
        m_tanks[counter].startTurn();
        if (m_tanks[counter].turnFinished())
        {
            m_tanks[counter].wait();
            counter = (counter + 1) % numPlayers;
            camera.ChangePlayer(counter);
        }
        
    }

    

    
 
}