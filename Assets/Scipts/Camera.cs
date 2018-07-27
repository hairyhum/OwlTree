using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;

    public Vector3 offset;

	// Use this for initialization
	void Start () {
        transform.position = player.transform.position + offset;
	}
	private void LateUpdate()
	{
        transform.position = player.transform.position + offset;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
