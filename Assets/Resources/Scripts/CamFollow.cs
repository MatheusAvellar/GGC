using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {
	
	private GameObject player;
	private Vector3 newPos;
	private Vector3 newRot;
	private bool canRot;
	
	void Start () {
		newRot = new Vector3(30f, 45f, 0f);
	}

	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player") as GameObject;
		newPos = new Vector3 (player.transform.position.x,
		                      player.transform.position.y,
		                      player.transform.position.z);
		this.transform.position = Vector3.Lerp (this.transform.position, newPos, 5f * Time.deltaTime);
		
		
		if (Input.GetKey(KeyCode.Q)) {
			canRot = true;
			newRot = new Vector3(30f, 135f, 0f);
			
		} else if (Input.GetKey(KeyCode.E)) {
			canRot = false;
			newRot = new Vector3(30f, 45f, 0f);
		}

		if (canRot) {
			this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, newRot, 5f * Time.deltaTime);
		} else {
			this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, newRot, 5f * Time.deltaTime);
		}
	}
}
