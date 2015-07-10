using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	private GameObject p;
	public static RaycastHit hit;

	private Vector3 rayCastBack;
	private Vector3 rayCastForward;
	private Vector3 rayCastRight;
	private Vector3 rayCastLeft;
	private Vector3 rayCastDown;

	public static bool isFloating = false;
	public static string moveDir;

	void Start () {
		rayCastBack = transform.TransformDirection(Vector3.back);
		rayCastForward = transform.TransformDirection(Vector3.forward);
		rayCastRight = transform.TransformDirection(Vector3.right);
		rayCastLeft = transform.TransformDirection(Vector3.left);
		rayCastDown = transform.TransformDirection(Vector3.down);
	}

	void Update () {
		p = GameObject.FindGameObjectWithTag("Player") as GameObject;
		this.transform.position = p.transform.position;

		if (Physics.Raycast(this.transform.position, rayCastDown, out hit, 1f, 1)) {
			isFloating = false;
			if (Physics.Raycast(this.transform.position, rayCastBack, out hit, 1f, 1)) {
				moveDir = "d";
			} else if (Physics.Raycast(this.transform.position, rayCastForward, out hit, 1f, 1)) {
				moveDir = "a";
			} else if (Physics.Raycast(this.transform.position, rayCastRight, out hit, 1f, 1)) {
				moveDir = "w";
			} else if (Physics.Raycast(this.transform.position, rayCastLeft, out hit, 1f, 1)) {
				moveDir = "s";
			} else {
				moveDir = "";
			}
		} else {
			moveDir = "";
			isFloating = true;
		}

		Debug.DrawLine(this.transform.position, hit.point, Color.white);
	}
}
