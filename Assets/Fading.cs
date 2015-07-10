using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public Texture2D fadeOut; 
	public float fadeSpeed =1f; 
	public int drawDepth = -1000; 
	private float alpha = 1.0f; 
	private int fadeDirection = -1; 

	// Use this for initialization
	void OnGUI () {

		alpha += fadeDirection * fadeSpeed * Time.deltaTime; 
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0,0, Screen.width, Screen.height), fadeOut);
	
	}
	
	public float FadeBegin (int direction) 
	{
		fadeDirection = direction; 
		return (fadeSpeed); 

	} 

	void OnLevelWasLoaded () 
	{
		FadeBegin (-1);

	}

} 