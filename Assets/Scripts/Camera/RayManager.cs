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
	public GameObject monster_daemon;
	float monsterInterval;

	// property
	private float gazedTime;
	private bool isDead = false;

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

			if (hit.collider.tag == "Monster") {
				gazedTime += Time.deltaTime;

				// 発射
				if (gazedTime >= 2.2) {
					shotGunScript.Fire (hit.collider.gameObject);
					gazedTime = 0;
				}
			} else {
				gazedTime = 0;
			}

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

		GameObject monster = Instantiate (monster_daemon,
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
