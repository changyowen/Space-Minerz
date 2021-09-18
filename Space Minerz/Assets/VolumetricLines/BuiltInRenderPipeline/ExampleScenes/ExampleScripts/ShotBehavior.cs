using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	[Header("Reference")]
	public GameObject hitEffect_obj;

	[Header("Data Value")]
	public float speed = 20f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * Time.deltaTime * speed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameObject newHitEffect = Instantiate(hitEffect_obj, collision.contacts[0].point, Quaternion.identity) as GameObject;
		Destroy(this.gameObject);
	}
}
