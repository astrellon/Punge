using UnityEngine;
using System.Collections;

public class StatusComponent : MonoBehaviour {
	
	public string ForPowerup;
	public string ForPlayer;
	public Powerup Powerup;
	public Player Player;
	protected GUIText sizeText;
	protected int totalWidth = 24;
	
	protected Texture2D durationBack;
	protected Texture2D durationFill;
	
	protected float durationPercent = 0.0f;
	
	protected Rect size = new Rect(0, 0, 32, 6);
	protected bool inited = false;
	
	// Use this for initialization
	void Start () {
		sizeText = transform.FindChild("Size").GetComponent<GUIText>();
	}
	
	public void UpdateTextures() {
		Debug.Log ("Powerup: " + this.Powerup + ", " + this.ForPlayer + ", " + this.ForPowerup);
		GetComponent<GUITexture>().texture = this.Powerup.StatusBack;
		if (this.Powerup.Duration > 0.0f) {
			durationBack = (Texture2D)Resources.LoadAssetAtPath(@"Assets\Punge\Textures\PowerupDuration.png", typeof(Texture2D));
			durationFill = (Texture2D)Resources.LoadAssetAtPath(@"Assets\Punge\Textures\PowerupDurationFill.png", typeof(Texture2D));
		}
	}
	
	void OnGUI() {
		int x = (int)((float)Screen.width * transform.localPosition.x);
		int y = (int)((float)Screen.height * transform.localPosition.y);
		size.x = x - 14;
		size.y = Screen.height - y + 20;
		GUI.BeginGroup(size);
		if (durationBack != null) {
			GUI.DrawTexture(new Rect(0, 0, size.width, size.height), durationBack);
		}
		if (durationFill != null && durationPercent > 0.0f) {
			int width = (int)Mathf.Round (durationPercent * (float)totalWidth);
			GUI.DrawTextureWithTexCoords(new Rect(4, 1, totalWidth - width, size.height - 2), durationFill, new Rect(0, 0, 1.0f - durationPercent, 1));
		}
		GUI.EndGroup();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.Powerup != null && this.Player != null) { 
			if (this.Player.PowerupStacks.ContainsKey(this.Powerup.Name)) {
				Player.PowerupStack stack = this.Player.PowerupStacks[this.Powerup.Name];
				sizeText.text = stack.Size.ToString();
				float now = Time.time;
				durationPercent = (now - stack.StartTime) / stack.Duration;
				if (durationPercent > 1.0f) {
					durationPercent = 1.0f;
				}
				else if (durationPercent < 0.0f) {
					durationPercent = 0.0f;
				}
			}
			else {
				sizeText.text = "0";
				durationPercent = 0.0f;
			}
		}
		else if (!inited) {
			inited = true;
			GameManager.MainManager.RegisterStatus(this);
			this.UpdateTextures();	
		}
	}
}
