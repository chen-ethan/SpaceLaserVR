using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTravel : MonoBehaviour {
    public float speed;
    public float range;
	// Use this for initialization
	void Start () {
        Debug.Log("new laser");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
	}
}
