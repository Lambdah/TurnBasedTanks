using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject[] players;
    public float shakeRandomValue = 10f;
    public float shakyStep = 0.5f;
    public float shakyCamTime = 3f;
    private Vector3 offset;
    private Vector3 shellOffset;
    private int currPlayer = 0;
    private bool chase = false;
    private GameObject chaseObj;
    private Vector3 currPos;
    private float shake;
    


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
        StartCoroutine(ShakeCamera(0.2f, 0.4f));
        StartCoroutine("ExecuteAfterTime", 1.0f);
        // transform.position = posn;
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

    IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 origPos = transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y , origPos.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = origPos;
        
    }
}
