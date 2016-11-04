using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayManager : MonoBehaviour {

	public GameObject dive_camera;
	public GameObject reticle;
	private float score;

	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (dive_camera.transform.position, dive_camera.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			reticle.transform.position = hit.point;

			if (hit.collider.tag == "Monster") {
				score += Time.deltaTime;

				if(score >= 2.2){
					Destroy(hit.collider.gameObject);
				}
			}
		}
	}
}
