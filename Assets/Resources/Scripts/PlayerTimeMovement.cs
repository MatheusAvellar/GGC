using UnityEngine;
using System.Collections;

public class PlayerTimeMovement : MonoBehaviour {

	private GameObject player;
	private GameObject time;
	private bool canSpawn;
	public static bool isTiming;
	private Color32 color;
	private GameObject safeZone;

	void Start () {
		safeZone = GameObject.FindGameObjectWithTag("Safe");
		color = new Color32(87, 99, 111, 1);
		Camera.main.backgroundColor = color;
		isTiming = true;
		canSpawn = true;
		this.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
		player = Resources.Load ("Prefabs/Player") as GameObject;
		time = Resources.Load ("Prefabs/Time") as GameObject;
	}

	void SpawnTime () {
		if (canSpawn) {
			Instantiate (time, this.transform.position, this.transform.rotation);
		}
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.W)) {
			this.transform.position += new Vector3 (1f, 0f, 0f);
			SpawnTime();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			this.transform.position -= new Vector3 (1f, 0f, 0f);
			SpawnTime();
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			this.transform.position += new Vector3 (0f, 0f, 1f);
			SpawnTime();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			this.transform.position -= new Vector3 (0f, 0f, 1f);
			SpawnTime();
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			this.transform.position -= new Vector3 (0f, 1f, 0f);
			SpawnTime();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			this.transform.position += new Vector3 (0f, 1f, 0f);
			SpawnTime();
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			Instantiate (player, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);
		}
	}


	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Safe") {
			Debug.Log("AH");
			canSpawn = false;	
		}
	}
	
	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Safe") {
			Debug.Log("OH");
			canSpawn = true;	
		}
	}
}
