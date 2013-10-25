using UnityEngine;
using System.Collections;

public class StatusComponent : MonoBehaviour {
	
	public Powerup ForPowerup;
	public Player ForPlayer;
	protected GUIText SizeText;
	//protected GUITexture DurationFill;
	protected int totalWidth = 24;
	
	public Texture2D DurationBack;
	public Texture2D DurationFill;
	
	protected float durationPercent = 0.0f;
	
	protected Rect size = new Rect(0, 0, 32, 6);
	
	// Use this for initialization
	void Start () {
		SizeText = transform.FindChild("Size").GetComponent<GUIText>();
	}
	
	void OnGUI() {
		int x = (int)((float)Screen.width * transform.localPosition.x);
		int y = (int)((float)Screen.height * transform.localPosition.y);
		size.x = x - 14;
		size.y = Screen.height - y + 20;
		GUI.BeginGroup(size);
		GUI.DrawTexture(new Rect(0, 0, size.width, size.height), DurationBack);
		if (durationPercent > 0.0f) {
			int width = (int)Mathf.Round (durationPercent * (float)totalWidth);
			GUI.DrawTextureWithTexCoords(new Rect(4, 1, totalWidth - width, size.height - 2), DurationFill, new Rect(0, 0, 1.0f - durationPercent, 1));
		}
		GUI.EndGroup();
	}
	
	// Update is called once per frame
	void Update () {
		if (ForPowerup != null && ForPlayer != null) { 
			if (ForPlayer.PowerupStacks.ContainsKey(ForPowerup.Name)) {
				Player.PowerupStack stack = ForPlayer.PowerupStacks[ForPowerup.Name];
				SizeText.text = stack.Size.ToString();
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
				SizeText.text = "0";
				durationPercent = 0.0f;
			}
		}
	}
}
