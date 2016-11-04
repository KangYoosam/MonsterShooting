using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayManager : MonoBehaviour {

	public GameObject dive_camera;
	public GameObject reticle;

	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (dive_camera.transform.position, dive_camera.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			reticle.transform.position = hit.point;

			print ("looking for enemies...");

			if (hit.collider.tag == "Monster") {
				Destroy(hit.collider.gameObject);
			}


		}
	}
}
