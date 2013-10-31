using UnityEngine;
using System;
using System.Collections;

public class Keychoose : MonoBehaviour {
	
	public class KeychooseEventArgs : EventArgs {
		public KeyCode Code;
		public KeychooseEventArgs(KeyCode code) {
			Code = code;
		}
	}
	
	public event EventHandler<KeychooseEventArgs> Changed;
	
	public string Label = "Label";
	public KeyCode Code = KeyCode.Space;
	public string CodeBindings = "";
	protected bool choosing = false;
	
	protected TextMesh labelObj;
	protected TextMesh valueObj;
	// Use this for initialization
	void Start () {
		labelObj = transform.FindChild("Label").GetComponent<TextMesh>();
		labelObj.text = Label;
		valueObj = transform.FindChild("Code").GetComponent<TextMesh>();
		UpdateCode();
	}
	
	public void UpdateCode() {
		if (CodeBindings.Length > 0) {
			valueObj.text = OptionValues.KeyBindings[CodeBindings].ToString();
		}
		else {
			valueObj.text = Code.ToString();
		}
	}
	
	void OnMouseDown() {
		choosing = !choosing;
		if (choosing) {
			valueObj.text = "Press Key";	
		}
		else {
			UpdateCode();
		}
	}
	
	void OnGUI () {
		if (!choosing) {
			return;
		}
		
		Event e = Event.current;
		if (e != null && e.isKey) {
			if (Code != e.keyCode) {
				Code = e.keyCode;
				if (Changed != null) {
					KeychooseEventArgs args = new KeychooseEventArgs(Code);
					Changed(this, args);
				}
			}
			UpdateCode();
			choosing = false;
		}
	}
}
