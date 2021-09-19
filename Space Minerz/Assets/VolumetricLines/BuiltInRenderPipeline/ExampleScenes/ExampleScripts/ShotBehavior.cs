using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	[Header("Reference")]
	public GameObject hitEffect_obj;

	[Header("Data Value")]
	public float speed = 20f;
	public float liveTime = 1f;

	float liveCountDown = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * Time.deltaTime * speed;
		liveCountDown += Time.deltaTime;
		if(liveCountDown > liveTime)
		{
			Destroy(this.gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameObject newHitEffect = Instantiate(hitEffect_obj, collision.contacts[0].point, Quaternion.identity) as GameObject;

		AsteroidInformationScript asteroidScript = collision.gameObject.GetComponent<AsteroidInformationScript>();

		if(asteroidScript != null)
		{
			asteroidScript.asteroidHealth -= 20;
		}

		Destroy(this.gameObject);
	}
}
