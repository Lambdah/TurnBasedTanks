using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject[] players;
    private Vector3 offset;
    private Vector3 shellOffset;
    private int currPlayer = 0;
    private bool chase = false;
    private bool freeMove = false;
    private GameObject chaseObj;
    private Vector3 currPos;
    private IEnumerator m_camera;
    private float cameraSpeed = 0.5f;
    private Vector3 prevChase;


    public void SetUpCamera(GameObject[] playable_objs)
    {
        players = playable_objs;
        offset = transform.position - players[0].transform.position;
    }

    public void ChangePlayer(int change)
    {
        if (freeMove)
        {
            currPlayer = change % players.Length;
            m_camera = MoveCamera(new Vector3(prevChase.x, 42.0f, prevChase.z), players[currPlayer].transform.position + offset, cameraSpeed, change);
            StartCoroutine(m_camera);
        }
        else
        {
            currPlayer = change % players.Length;
        }
        
    }

    public void ChaseAction(GameObject obj)
    {
        chase = true;
        shellOffset = transform.position - obj.transform.position;
        chaseObj = obj;
    }


    public void StopChase(Vector3 posn)
    {
        transform.position = posn;
        freeMove = true;
        chase = false;
    }

    private void Update()
    {
        // Need this else the camera goes off the screen. Makes sure the camera does not move below 42.0f and starts clipping
        if (transform.position.y < 42.0f)
        {
            transform.position = new Vector3(prevChase.x, 42.0f, prevChase.z);
        }
    }


    private void LateUpdate()
    {
        if (chase)
        {

            if (chaseObj.activeInHierarchy)
            {
                prevChase = chaseObj.transform.position + shellOffset;
                transform.position = new Vector3(prevChase.x, 42.0f, prevChase.z);
            }
        }
        else if (freeMove)
        {
            // Camera can move freely now
        }
        else
        {
            transform.position = players[currPlayer].transform.position + offset;
        }
        
        
    }

    IEnumerator MoveCamera(Vector3 start, Vector3 end, float duration, int player)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(start, end, t / duration);
            yield return 0;
        }
        transform.position = end;
        freeMove = false;
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        // chase = false;
        chaseObj = null;

    }

   
}
