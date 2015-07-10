using UnityEngine;
using System.Collections;

public class GoalDoor : MonoBehaviour {

	public string thisScene;
	public string[] scenes;
	private Vector3 newPos;

	void Start () {
	
	}

	void Update () {
		if (PlayerMovement.won) {
			newPos = new Vector3 (this.transform.position.x,
			                      2.5f,
			                      this.transform.position.z);
			transform.position = Vector3.Lerp (this.transform.position, newPos, 2f * Time.deltaTime);
			StartCoroutine(sceneDelay(2f));
			Debug.Log("Ganhou");
		}
	}
	
	IEnumerator sceneDelay (float _time) {
		yield return new WaitForSeconds(_time);
		int rada = Application.loadedLevel + 1;
		Application.LoadLevel(rada);
	}
}
