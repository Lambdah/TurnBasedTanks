using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject[] players;
    private Vector3 offset;
    private int currPlayer = 0;

    public void SetUpCamera(GameObject[] playable_objs)
    {
        players = playable_objs;
        offset = transform.position - players[0].transform.position;
    }

    public void ChangePlayer(int change)
    {
        currPlayer = change % players.Length;
    }

    private void LateUpdate()
    {
        transform.position = players[currPlayer].transform.position + offset;
    }
}
