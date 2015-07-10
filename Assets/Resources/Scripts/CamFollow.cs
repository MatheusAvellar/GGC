using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {
	
	private GameObject player;
	private Vector3 newPos;

	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player") as GameObject;
		newPos = new Vector3 (player.transform.position.x,
		                      player.transform.position.y,
		                      player.transform.position.z);
		transform.position = Vector3.Lerp (this.transform.position, newPos, 5f * Time.deltaTime);

	}
}
