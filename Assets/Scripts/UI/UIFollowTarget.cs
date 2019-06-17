using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTarget : MonoBehaviour {
    public Transform follow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (follow == null)
        {
            return;
        }

        Vector2 sp = Camera.main.WorldToScreenPoint(follow.position);
        this.transform.position = sp;
	}

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            Update();
        }
    }
}
