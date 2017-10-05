using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

	public int selectedWeapon = 0;
	public Camera cam;

	// Use this for initialization
	void Start () {
		SelectWeapon ();
	}
	
	// Update is called once per frame
	void Update () {
		int previousSelected = selectedWeapon;

		if (Input.GetKeyDown (KeyCode.F)) {
			RaycastHit reach;

			if (Physics.Raycast (cam.transform.position, cam.transform.forward, out reach, 3)) {
				Debug.Log (reach.transform.name);

				if (reach.transform != null) {
					GameObject gun = reach.collider.gameObject;
					if (gun.tag == "Gun") {
						gun.transform.parent = this.transform;
						gun.transform.position = this.transform.position;
						gun.transform.rotation = this.transform.rotation;

						Rigidbody rb = gun.GetComponent<Rigidbody>();
						rb.isKinematic = true;
						rb.useGravity = false;

						Aiming aim = gun.GetComponent<Aiming> ();
						aim.init ();
						SelectWeapon ();
					}
				}
			}
		}

		if (Input.GetAxis ("Mouse ScrollWheel")>0f) {
			if (selectedWeapon >= transform.childCount - 1) {
				selectedWeapon = 0;
			} else {
				selectedWeapon++;
			}
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0f) {
			if (selectedWeapon <= 0) {
				selectedWeapon = transform.childCount - 1;;
			} else {
				selectedWeapon--;
			}
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			DropWeapon ();
		}

		if(Input.GetKeyDown(KeyCode.Alpha1)){
			selectedWeapon = 0;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2){
			selectedWeapon = 1;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >=3){
			selectedWeapon = 2;
		}
		if(Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >=4){
			selectedWeapon = 3;
		}
		if (previousSelected != selectedWeapon) {
			SelectWeapon ();
		}
	}

	void SelectWeapon(){

		int i = 0;
		foreach (Transform weapon in transform) {

			if (i == selectedWeapon) {
				weapon.gameObject.SetActive (true);
			} else {
				weapon.gameObject.SetActive (false);

			}
			i++;
		}

	}

	void DropWeapon() {

		int i = 0;
		foreach (Transform weapon in transform) {

			if (i == selectedWeapon) {
				weapon.transform.parent = null;

				Rigidbody rb = weapon.GetComponent<Rigidbody>();
				rb.isKinematic = false;
				rb.useGravity = true;
				rb.AddForce( new Vector3 (0, 2, 0));
			}
			i++;
		}
		SelectWeapon ();
	}
}
