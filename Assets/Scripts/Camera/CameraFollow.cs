﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject[] players;
    private Vector3 offset;
    private Vector3 shellOffset;
    private int currPlayer = 0;
    private bool chase = false;
    private GameObject chaseObj;
    private Vector3 currPos;
    


    public void SetUpCamera(GameObject[] playable_objs)
    {
        players = playable_objs;
        offset = transform.position - players[0].transform.position;
    }

    public void ChangePlayer(int change)
    {
        currPlayer = change % players.Length;
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
        StartCoroutine("ExecuteAfterTime", 1.0f);
    }

    private void LateUpdate()
    {
        if (chase)
        {
            transform.position = chaseObj.transform.position + shellOffset;
        }
        else
        {
            transform.position = players[currPlayer].transform.position + offset;
        }
        
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        chase = false;
        chaseObj = null;

    }

    IEnumerator ShakeCamera(Vector3 posn)
    {
        yield return null;
    }
}
