using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    public float travelTime;
    public float rotateTime;
    public Transform[] nTargets;
    //public Vector3[] nTargets;


    private int i;
    private Vector3 startPos;
    private Vector3 endPos;
    private IEnumerator m_coroutine;
    private IEnumerator r_coroutine;

    private Vector3 targetAngle;
    private Vector3 currentAngle;

    private Vector3 targetDir;

    private Vector3 newDir;
    private float step;

    private bool rotate;
    private bool move;

    // Use this for initialization
    void Start () {
        /*i = 0;
        startPos = transform.position;
        endPos = nTargets[i].position;

        m_coroutine = MoveObject(startPos, endPos, travelTime);
        r_coroutine = RotateObject(startPos, endPos,rotateTime);

        rotate = true;
        move = false;*/
        clearnTargets();
    }
	
	// Update is called once per frame
	void Update () {
        /*if (rotate)
        {
            StartCoroutine(r_coroutine);
            float dot = Vector3.Dot(transform.forward, (endPos - transform.position).normalized);
            if (dot >= 0.95f)
            {
                
                rotate = false;
                startPos = transform.position;
                endPos = nTargets[i].position;
                m_coroutine = MoveObject(startPos, endPos, travelTime);
                move = true;
            }
        }

        if (move)
        {
            StartCoroutine(m_coroutine);
            if (transform.position == endPos && i < (nTargets.Length - 1))
            {
                i++;
                move = false;
                endPos = nTargets[i].position;
                startPos = transform.position;
                r_coroutine = RotateObject(startPos, endPos, rotateTime);
                rotate = true;
                
            }
        }*/
        
	}
    
    public void clearnTargets()
    {
        nTargets = null;
    }

    public bool isEmptynTargets()
    {
        
        if (nTargets == null)
        {
            return true;
        }
        return false;
    }

    private Transform[] GetNodeTransformation(List<Node> trans)
    {
        Transform[] posTrans = new Transform[trans.Count];
        for (int i=0; i < trans.Count; i++)
        {
            
            posTrans[i] = trans[i].transform;
        }

        return posTrans;
    }

    public void TargetMove(List<Node> targetPos)
    {
        nTargets = GetNodeTransformation(targetPos);
        i = 1;
        startPos = transform.position;
        endPos = nTargets[i].position;
        m_coroutine = MoveObject(startPos, endPos, travelTime);
        r_coroutine = RotateObject(startPos, endPos, rotateTime);

        rotate = true;
        move = false;



    }

    public bool Move()
    {
        
        if (rotate)
        {
            
            StartCoroutine(r_coroutine);
            float dot = Vector3.Dot(transform.forward, (endPos - transform.position).normalized);
         
            if (dot >= 0.95f)
            {

                rotate = false;
                startPos = transform.position;
                endPos = nTargets[i].position;
                m_coroutine = MoveObject(startPos, endPos, travelTime);
                move = true;
            }
        }
        if (move)
        {
            
            StartCoroutine(m_coroutine);
            if (transform.position == endPos && i < (nTargets.Length - 1))
            {
                i++;
                move = false;
                endPos = nTargets[i].position;
                startPos = transform.position;
                r_coroutine = RotateObject(startPos, endPos, rotateTime);
                rotate = true;

            }
        }

        if(nTargets == null)
        {
            //Debug.Log("nTargets is null");
            return true;
        }

        
        if (transform.position == nTargets[nTargets.Length - 1].position)
        {
            nTargets = null;
            return false;
        }
        return true;

    }

    IEnumerator MoveObject(Vector3 start, Vector3 end, float moveSpeed)
    {
        float timer = Time.time;
        while (Time.time < timer + moveSpeed)
        {
            transform.position = Vector3.Lerp(start, end, (Time.time - timer) / moveSpeed);
            yield return null;
        }
        transform.position = end;
    }

    IEnumerator RotateObject(Vector3 start, Vector3 end, float rotateSpeed)
    {
        
        targetDir = endPos - startPos;
        targetDir = new Vector3(targetDir.x, 0f, targetDir.z);
        float timer = Time.time;
        Vector3 newDir;
        float step;
        while (Time.time < timer + rotateSpeed)
        {
            step = Time.deltaTime * rotateSpeed;
            newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            yield return null;
        }
        

    }

}
