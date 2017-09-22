using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAi : MonoBehaviour {

	public float speed = 3.0f;
	public float obstacleRange = 5.0f;

	private bool alive;

	[SerializeField] private GameObject fireballPrefab;
	private GameObject fireball;
	// Use this for initialization
	void Start () {
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (alive) {
			transform.Translate (0, 0, speed * Time.deltaTime);

			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast (ray, 0.75f, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				if(hitObject.GetComponent<CharacterController>()){
					if (fireball == null) {
						fireball = Instantiate (fireballPrefab) as GameObject;
						fireball.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
						fireball.transform.rotation = transform.rotation;
					}
				}
			}
			else if (hit.distance < obstacleRange) {
					float angle = Random.Range (-110, 110);
					transform.Rotate (0, angle, 0);
			}
		}
	}


	public void SetAlive(bool alive){
		this.alive = alive;
	}
}
