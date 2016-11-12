using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class RayManager : MonoBehaviour {

	// Player
	public GameObject diveCamera;
	public GameObject reticle;
	public GameObject blood;

	// Gun
	public ShotGunScript shotGunScript;

	// Monster
	public GameObject Daemon;
	float monsterInterval;

	// property
	private float gazedTime;
	private bool isDead = false;
	private float size;

	Transform[] positions;

	void Start () {
		GameObject positionsWrapper = GameObject.Find ("Cave/EmergePositions");

		// 子要素をすべて取得（自身のオブジェクトは除く）
		positions = positionsWrapper.transform.GetComponentsInChildren<Transform> ()
			.Where( c => positionsWrapper.gameObject != c.gameObject )
			.ToArray();
	}

	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (diveCamera.transform.position, diveCamera.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			reticle.transform.position = hit.point;

			size = 0.2f * hit.distance;

			if (hit.collider.tag == "Monster") {
				gazedTime += Time.deltaTime;

				size = size * gazedTime;

				// 発射
				if (gazedTime >= 1.7) {
					shotGunScript.Fire (hit.collider.gameObject);
					gazedTime = 0;
				}
			} else {
				gazedTime = 0;
			}

			reticle.transform.localScale = new Vector2 (size, size);

			// 血飛沫を表示
			if (isDead) {
				blood.SetActive (true);
			}
		}

		// モンスターを生成
		monsterInterval += Time.deltaTime;
		if (monsterInterval >= 1f) {
			GenerateMonster ();
		}
	}

	// モンスターを生成
	void GenerateMonster() {
		monsterInterval = 0.0f;

		Quaternion q = Quaternion.Euler(0, 0, 0);
		int number = Random.Range (0, positions.Length);

		GameObject monster = Instantiate (Daemon,
			positions[number].transform.position,
			q
		) as GameObject;

		monster.transform.LookAt (diveCamera.transform);

		// MoveToCameraScriptのtargetプロパティにmonsterをアタッチ。
		MonsterScript monsterScript = monster.GetComponent<MonsterScript> ();
		monsterScript.target = diveCamera;
	}

	public void PlayerDead () {
		isDead = true;
	}
}
