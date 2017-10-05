using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public float damage = 10;
	public float range = 100f;
	public float fireRate = 15f;
	public float impactForce = 30f;

	public int maxAmmo = 10;
	private int currentAmmo;
	public float reloadTime = 1f;
	private bool isReloading;

	public Camera fpsCam;
	public GameObject muzzleFlash;
	public GameObject impactEffect; 

	public Animator animator;

	private float nextTimeToFire = 0f;
	// Use this for initialization
	void Start () {
		currentAmmo = maxAmmo;
		isReloading = false;
	}

	void OnEnable(){
		isReloading = false;
		animator.SetBool ("Reloading", false);

	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButton (0) && Time.time >= nextTimeToFire && currentAmmo > 0 && !isReloading) {
			nextTimeToFire = Time.time + 1f/fireRate;
			Shoot ();
		}


		if(Input.GetKeyDown(KeyCode.R) && !isReloading){
			StartCoroutine(Reload());
		}
	}

	void FixedUpdate(){


	}
		


	void Shoot(){
		animator.Play ("WeaponShoot");
		RaycastHit hit;

		currentAmmo--;
		//GameObject t_muzzleFlash = Instantiate (muzzleFlash);
		//Destroy (t_muzzleFlash, 0.3f);
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, 
			   out hit, range)) {
			Debug.Log (hit.transform.name);

			Target target = hit.transform.GetComponent<Target> ();
			if (target != null) {
				target.TakeDamage (damage);
			}

			if (hit.rigidbody != null) {
				hit.rigidbody.AddForce (-hit.normal * impactForce);
			}
			GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, 0.2f);
		}
	}

	IEnumerator Reload(){
		isReloading = true;

		animator.SetBool ("Reloading", true);

		//0.25f for animation transitions
		yield return new WaitForSeconds (reloadTime- 0.25f);
		animator.SetBool ("Reloading", false);
		yield return new WaitForSeconds (0.25f);
		currentAmmo = maxAmmo;
		isReloading = false;
	}
}
