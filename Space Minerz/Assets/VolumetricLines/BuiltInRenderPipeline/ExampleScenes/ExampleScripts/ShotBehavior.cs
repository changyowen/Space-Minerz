using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	public float speed = 20f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += /*transform.rotation * */transform.forward * Time.deltaTime * speed;
	
	}
}
