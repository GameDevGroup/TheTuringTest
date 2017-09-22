using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	[SerializeField] private GameObject enemyPrefab;
	private GameObject enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy == null) {
			enemy = Instantiate (enemyPrefab) as GameObject;
			enemy.transform.position = new Vector3 (0, 1.93f, 0);

			float angle = Random.Range (0, 360);
			enemy.transform.Rotate (0, angle, 0);
		}
	}
}
