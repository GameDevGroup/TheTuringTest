using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {


	public Vector3 aimDownSight;
	private Vector3 aimHipSight;
	private Vector3 endPos;
	private bool aiming;
	public float aimSpeed;


	// Use this for initialization
	void Start () {
		//0, 0, 0
		//ak
		//aimDownSight = new Vector3 (0.2f, 0.075f, -0.1f);
		//m4 (0, 0.035, -0.1)
		//pistol (-0.1, 0.1, -0.1)
		//sniper (0.05, 0.05, -0.1)
		init();
	}

	public void init() {
		aimHipSight = transform.localPosition;

		aiming = false;
		endPos = aimHipSight;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.GetComponentInParent<WeaponSwitch>()) {
			bool previousAim = aiming;
			if (Input.GetMouseButtonDown (1)) {
				aiming = !aiming;
				if (aiming) {
					endPos = aimDownSight;
				} else {
					endPos = aimHipSight;
				}
			}
			
			transform.localPosition = Vector3.Slerp (transform.localPosition, endPos, Time.deltaTime * aimSpeed);
		}
	}
}
