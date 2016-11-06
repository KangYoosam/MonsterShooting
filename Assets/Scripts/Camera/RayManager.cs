using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayManager : MonoBehaviour {

	public GameObject dive_camera;
	public GameObject reticle;
	public ShotGunScript shotGunScript;

	private float gazedTime;

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (dive_camera.transform.position, dive_camera.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			reticle.transform.position = hit.point;

			if (hit.collider.tag == "Monster") {
				gazedTime += Time.deltaTime;

				if(gazedTime >= 2.2){
					Destroy(hit.collider.gameObject);

//					ShotGunScript shotGun = GetComponent<ShotGunScript> ();
					shotGunScript.Fire ();

//					ShotGunScript shotGun = GetComponent<Animator> ();
//					shotGun.
//					animator.SetTrigger ("Gazed");
				}
			}
		}
	}
}
