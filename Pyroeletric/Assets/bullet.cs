using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    float time = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= 1.5f) Destroy(gameObject);
        transform.position = Vector3.Lerp(transform.position, transform.TransformPoint(Vector3.up), 20f * Time.deltaTime);
        
        
    }
}
