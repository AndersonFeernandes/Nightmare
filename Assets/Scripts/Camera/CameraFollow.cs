using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //Transform do jogador
    public Transform target;

    //Tornar a transição mais fluida
    public float smoothing = 5f;

    //Distancia da camera pro jogador
    Vector3 offset;

    
	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;		
	}

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime) ;
    }

}
