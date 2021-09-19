using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	[Header("Reference")]
	public GameObject hitEffect_obj;

	[Header("Data Value")]
	public int beamIndex = 0;
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

		switch(collision.gameObject.tag)
		{
			case "Player":
				{
					break;
				}
			case "Enemy":
				{
					EnemyInformationScript enemyScript = collision.gameObject.GetComponent<EnemyInformationScript>();

					if(enemyScript != null)
					{
						if (beamIndex == 0)
						{
							enemyScript.enemyHealth -= 20;
						}
						else
						{
							enemyScript.enemyHealth -= 5;
						}
					}
					break;
				}
			case "Asteroid":
				{
					AsteroidInformationScript asteroidScript = collision.gameObject.GetComponent<AsteroidInformationScript>();

					if (asteroidScript != null)
					{
						if(beamIndex == 1)
						{
							asteroidScript.asteroidHealth -= 20;
						}
						else 
						{
							asteroidScript.asteroidHealth -= 5;
						}
					}
					break;
				}
		}
		Destroy(this.gameObject);
	}
}
