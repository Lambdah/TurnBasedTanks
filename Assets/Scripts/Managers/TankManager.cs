using System;
using UnityEngine;

[Serializable]
public class TankManager
{
                
    public Transform m_SpawnPoint;
    [HideInInspector]public Color m_PlayerColor;
    [HideInInspector] public int m_playerNumber;             
    // [HideInInspector] public string m_coloredPlayerText;
    [HideInInspector] public GameObject m_instance;          
    [HideInInspector] public int m_wins;


    private TankDamageOverlay m_tankDamage;
    private TankBoardMovement m_movement;
    private TankFire m_fire;
    // private GameObject m_CanvasGameObject;

    public void Setup()
    {
        m_movement = m_instance.GetComponent<TankBoardMovement>();
        m_fire = m_instance.GetComponent<TankFire>();
        m_tankDamage = m_instance.GetComponent<TankDamageOverlay>();
        m_tankDamage.setFollowTarget(m_instance.transform);
        // m_CanvasGameObject = m_instance.GetComponentInChildren<Canvas>().gameObject;
        m_movement.wait = true;
        m_fire.wait = true;
        // m_coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_playerNumber + "</color>";
        MeshRenderer[] renderers = m_instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }

        wait();
    }
    
    public void spawnPointNode(Node spawn)
    {
        m_movement.TankCurrentNodePosition(spawn);
    }

    // Probably want to switch this to an array in the future
    public void setShootableTargets(Transform target)
    {
        m_fire.shootableTargets = target;
    }

   public void startTurn()
   {
        m_movement.wait = false;
   }

   public void wait()
   {
        m_movement.wait = true;
        m_movement.turnFinished = false;
   }

   public void move(Node node)
   {
        m_movement.GridLocationMove(node);
        m_movement.targetNode = node;
        m_movement.currNode = node;
   }

    public bool turnFinished()
    {
        return m_movement.turnFinished;
    }


    public void Reset()
    {
        m_instance.transform.position = m_SpawnPoint.position;
        m_instance.transform.rotation = m_SpawnPoint.rotation;

        m_instance.SetActive(false);
        m_instance.SetActive(true);
    }
}
