/******************************************
  * uWebKit 
  * (c) 2013 THUNDERBEAST GAMES, LLC
  * sales@uwebkit.com
*******************************************/

using UnityEngine;
using System.Collections;
using UWK;
using System.Collections.Generic;

/// <summary>
/// Basic example of using a WebView on a 3D Unity surface
/// </summary>
 
// IMPORTANT: Please see the WebGUI.cs example for 2D support

public class WebTexture : MonoBehaviour//EZInputHandlerCSharp
{
	
	#region Inspector Fields
	
	//public string URL = "http://www.google.com";
	public int Width = 512;
	public int Height = 512;
	public bool KeyboardEnabled = true;
	public bool MouseEnabled = true;
	public bool Rotate = true;
	public bool AlphaMask = false;
	public UWKView View = null;
	public TextAsset HtmlText;
	public Material WebMaterial;

	protected void OnDestroy(){
		
		UWKCore.DestroyViewsOnLevelLoad = true;
	}
	#endregion
	public void load(string url){
		if (View == null) {
			
			isLoaded = false;
			// Create the WebView, note that smart rects are disabled as they are only supported on 2D surfaces
			View = UWKCore.CreateView ("WebTextureExample", url, Width, Height, true);

			// setup out delegate for when the view has veen created and ready to go
			View.ViewCreated += viewCreated;
		} else {
			Valid = true;
			isLoaded = true;
		}
		
		// listen in for loaded page if we have a URL
		if (url != null && url.Length > 0) {
			
			Debug.Log("????!!!!!!!!!!!!!!!!???");
			View.LoadFinished += loadFinished;
		}
		else if (HtmlText != null)			
			// for TextAsset example, we won't render object until the view is loaded
			gameObject.renderer.enabled = false;
	}
	// Use this for initialization
	void Start ()
	{	
		isLoaded = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		// We'll be valid one the view is created
		if (!Valid)
			return;
		
		// can happen on a texture resize
		if (renderer != null)
		if (renderer.material.mainTexture == null)
			renderer.material.mainTexture = View.MainTexture;

		if (WebMaterial != null) {
			WebMaterial.mainTexture = View.MainTexture;
		}

		if (guiTexture != null)
		if (guiTexture.texture == null)
			guiTexture.texture = View.MainTexture;
		
		
		if (!MouseEnabled)
			return;
		
#if UNITY_IPHONE && !UNITY_EDITOR
		
		if (guiTexture == null && Rotate)
			gameObject.transform.Rotate(0, Time.deltaTime * 2.0f, 0);
		else if (guiTexture != null)
			View.DrawGUI(0, (int) (Screen.height - guiTexture.pixelInset.height), (int) guiTexture.pixelInset.width, (int) guiTexture.pixelInset.height);
		
		return;
#endif
		
		if (Rotate)
			gameObject.transform.Rotate (0, Time.deltaTime * 2.0f, 0);
		/*
		RaycastHit rcast;
			
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out rcast)) {
				
			if (rcast.collider != GetComponent<MeshCollider> ())
				return;
				
			int x = (int)(rcast.textureCoord.x * (float)Width);
			int y = Height - (int)(rcast.textureCoord.y * (float)Height);
			
			View.SetMousePos (x, y);
				
			for (int i = 0; i < 3; i++) {
				if (Input.GetMouseButtonDown (i)) {
					View.OnMouseButtonDown (x, y, i);
				}
					
				if (Input.GetMouseButtonUp (i)) {
						
					View.OnMouseButtonUp (x, y, i);
						
				}
					
			}
			
		}
		*/
	}

	//void On
	/*void OnGUI ()
	{
		if (!Valid)
			return;
		
		if (!KeyboardEnabled)
			return;
		
		View.ProcessKeyboard (Event.current);
		
	}
	*/
	// Delegate for initializing one view has been created
	void viewCreated (UWKView view)
	{
		Valid = true;
		
		view.AlphaMask = AlphaMask;
		
		if (HtmlText != null) {
			view.LoadTextAssetHTML (HtmlText);
			View.LoadFinished += loadFinished;
		}
		
	}
	
	void loadFinished (UWKView view)
	{
		Debug.Log("nonononononononononono");
		//gameObject.renderer.enabled = true;
		
#if UNITY_IPHONE && !UNITY_EDITOR		
		view.captureTexture();
#endif
		isLoaded = true;
	}
	public bool isLoaded = false;
	bool Valid = false;


	/*
	override protected void inputMove(EZMouseEventCSharp evt){
		View.SetMousePos ( Mathf.RoundToInt(evt.position.x*Width), Height - Mathf.RoundToInt(evt.position.y*Height));
	}
	override protected void inputUp(EZMouseEventCSharp evt){
		
		Debug.Log ("==aa==");
		View.OnMouseButtonUp (Mathf.RoundToInt(evt.position.x*Width), Height - Mathf.RoundToInt(evt.position.y*Height), 0);
	}
	override protected void inputDown(EZMouseEventCSharp evt){
		
		View.OnMouseButtonDown (Mathf.RoundToInt(evt.position.x*Width), Height - Mathf.RoundToInt(evt.position.y*Height), 0);
		Debug.Log("b");
		Debug.Log ("========"+ Mathf.RoundToInt(Height -evt.position.y*Height));
	}

*/
	
}
