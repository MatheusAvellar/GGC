using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour {
	
	public  bool ismoving = false;
	private float invertPosX;
	private float invertPosY;
	private float invertPosZ;
	private float cubeAngle;
	private float startY = 0;
	private float cubeSpeed;
	private int cubeSize;
	public static string moveDir;
	public static string oldMoveDir;
	public static bool ignoreDir;
	public static bool won = false;
	private Vector3 mainPos;
	private int count;
	private float[] posXAlone = new float[3];
	private string[] moveDirArray = new string[4];
	private string triggerTag;
	public  static Vector3 collCheck;

	private GameObject playerTime;

	private Color32 color;

	IEnumerator DoRoll (Vector3 aPoint, Vector3 aAxis, float aAngle, float aDuration) {

		float tSteps;
		tSteps = Mathf.Ceil (aDuration * 30.0f);
		float tAngle;
		tAngle = aAngle / tSteps;
		Vector3 pos = new Vector3(0, 0, 0);

		for (int i = 1; i <= tSteps; i++) {
			transform.RotateAround (aPoint, aAxis, tAngle);
			yield return new WaitForSeconds(0.00005f);
		}
		
		transform.Find("targetpoint").position = transform.position;
		pos = transform.position;
		pos += new Vector3(0, startY, 0);
		transform.position = pos;

		ismoving = false;
	}

	void Start () {
		color = new Color32(41, 41, 41, 1);
		won = false;
		Camera.main.backgroundColor = color;
		if (PlayerPrefs.GetFloat("x") != 0) {
			this.transform.position = new Vector3(PlayerPrefs.GetFloat("x"),
			                                      PlayerPrefs.GetFloat("y"),
			                                      PlayerPrefs.GetFloat("z"));
		}
		mainPos = new Vector3 (3, 1, 0);
		this.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
		playerTime = Resources.Load ("Prefabs/PlayerTime") as GameObject;
		cubeAngle = 90.0f;
		cubeSpeed = 0.5f;
		cubeSize = 1;
		posXAlone [0] = 0f;
		posXAlone [1] = -1f;
		posXAlone [2] = 1f;
		moveDirArray[0] ="w";
		moveDirArray[1] ="a";
		moveDirArray[2] ="s";
		moveDirArray[3] ="d";
	}

	void Climb () {
		if (moveDir == GroundCheck.moveDir) {
			invertPosY = 1f;
			cubeAngle = 180.0f;
			cubeSpeed = 1f;
		} else {
			invertPosY = -1f;
			cubeAngle = 90.0f;
			cubeSpeed = 0.5f;
		}
	}

    void Update () {

		count ++;
		if (Application.loadedLevel >= 3 && Application.loadedLevel <= 6) {
			if ((Input.GetKey (KeyCode.A))
				&& ismoving == false
				&& !GroundCheck.isFloating
				&& !won) {
				invertPosX = 0f;
				invertPosZ = 1f;
				oldMoveDir = moveDir;
				moveDir = "a";
				Climb ();
				ismoving = true;

				transform.Find ("targetpoint").position +=
				new Vector3 ((float)(cubeSize * invertPosX) / 2,
				             (float)(cubeSize * invertPosY) / 2,
				             (float)(cubeSize * invertPosZ) / 2);

				StartCoroutine (DoRoll (transform.Find ("targetpoint").position, Vector3.right, cubeAngle, cubeSpeed));
			}

			if ((Input.GetKey (KeyCode.D))
				&& ismoving == false
				&& !GroundCheck.isFloating
				&& !won) {
				invertPosX = 0f;
				invertPosZ = -1f;
				oldMoveDir = moveDir;
				moveDir = "d";
				Climb ();
				ismoving = true;

				transform.Find ("targetpoint").position +=
				new Vector3 ((float)(cubeSize * invertPosX) / 2,
				             (float)(cubeSize * invertPosY) / 2,
				             (float)(cubeSize * invertPosZ) / 2);

				StartCoroutine (DoRoll (transform.Find ("targetpoint").position, -Vector3.right, cubeAngle, cubeSpeed));  
			}

			if ((Input.GetKey (KeyCode.S))
				&& ismoving == false
				&& !GroundCheck.isFloating
				&& !won) {
				invertPosX = -1f;
				invertPosZ = 0f;
				oldMoveDir = moveDir;
				moveDir = "s";
				Climb ();
				ismoving = true;

				transform.Find ("targetpoint").position +=
				new Vector3 ((float)(cubeSize * invertPosX) / 2,
				             (float)(cubeSize * invertPosY) / 2,
				             (float)(cubeSize * invertPosZ) / 2);

				StartCoroutine (DoRoll (transform.Find ("targetpoint").position, Vector3.forward, cubeAngle, cubeSpeed));
			}

			if ((Input.GetKey (KeyCode.W))
				&& ismoving == false
				&& !GroundCheck.isFloating
				&& !won) {
				invertPosX = 1f;
				invertPosZ = 0f;
				oldMoveDir = moveDir;
				moveDir = "w";
				Climb ();
				ismoving = true;


				transform.Find ("targetpoint").position +=
				new Vector3 ((float)(cubeSize * invertPosX) / 2,
				             (float)(cubeSize * invertPosY) / 2,
				             (float)(cubeSize * invertPosZ) / 2);

				StartCoroutine (DoRoll (transform.Find ("targetpoint").position, -Vector3.forward, cubeAngle, cubeSpeed));
			}

			if (Input.GetKeyDown (KeyCode.Return)
				&& ismoving == false
				&& !won) {
				Instantiate (playerTime, this.transform.position, this.transform.rotation);
				Destroy (this.gameObject);

				PlayerPrefs.SetFloat ("x", this.transform.position.x);
				PlayerPrefs.SetFloat ("y", this.transform.position.y);
				PlayerPrefs.SetFloat ("z", this.transform.position.z);
				Debug.Log (PlayerPrefs.GetFloat ("x"));
				Debug.Log (PlayerPrefs.GetFloat ("y"));
				Debug.Log (PlayerPrefs.GetFloat ("z"));
			}
		}

		if ((Application.loadedLevel <= 2 || Application.loadedLevel == 7 )&& count >= 50) {
		
			switch(moveDirArray[Random.Range(0,4)])
			{
				case "w":
					invertPosX = 1f;
					invertPosZ = 0f;
					oldMoveDir = moveDir;
					moveDir = "w";
					Climb();
					ismoving = true;
					
					
					transform.Find ("targetpoint").position +=
						new Vector3 ((float)(cubeSize  * invertPosX) / 2,
						             (float)(cubeSize  * invertPosY) / 2,
						             (float)(cubeSize  * invertPosZ) / 2);
					StartCoroutine (DoRoll (transform.Find ("targetpoint").position, -Vector3.forward, cubeAngle, cubeSpeed));
					break;
				case "s":
					invertPosX = -1f;
					invertPosZ = 0f;
					oldMoveDir = moveDir;
					moveDir = "s";
					Climb();
					ismoving = true;
					
					transform.Find ("targetpoint").position +=
						new Vector3 ((float)(cubeSize  * invertPosX) / 2,
						             (float)(cubeSize  * invertPosY) / 2,
						             (float)(cubeSize  * invertPosZ) / 2);
					
					StartCoroutine (DoRoll (transform.Find ("targetpoint").position, Vector3.forward, cubeAngle, cubeSpeed));
					break;
				case "d":
					invertPosX = 0f;
					invertPosZ = -1f;
					oldMoveDir = moveDir;
					moveDir = "d";
					Climb();
					ismoving = true;
					
					transform.Find ("targetpoint").position +=
						new Vector3 ((float)(cubeSize  * invertPosX) / 2,
						             (float)(cubeSize  * invertPosY) / 2,
						             (float)(cubeSize  * invertPosZ) / 2);
					
					StartCoroutine (DoRoll (transform.Find ("targetpoint").position, -Vector3.right, cubeAngle, cubeSpeed));
					break;
				case "a":
					invertPosX = 0f;
					invertPosZ = 1f;
					oldMoveDir = moveDir;
					moveDir = "a";
					Climb();
					ismoving = true;
					
					transform.Find ("targetpoint").position +=
						new Vector3 ((float)(cubeSize  * invertPosX) / 2,
						             (float)(cubeSize  * invertPosY) / 2,
						             (float)(cubeSize  * invertPosZ) / 2);
					
					StartCoroutine (DoRoll (transform.Find ("targetpoint").position, Vector3.right, cubeAngle, cubeSpeed));
					break;
			}
			count = 0;
		}


		if(GroundCheck.isFloating
		   && ismoving == false) {
			this.transform.position += new Vector3(0f, -1f, 0f);
		}

		if (this.transform.position.y < -15.0f) {
			this.transform.position = mainPos;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "WinPosition") {
			won = true;
		}
	}
	
}