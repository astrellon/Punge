﻿using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	
	public float Length = 4.0f;
	public float Speed = 0.2f;
	public KeyCode UpKey = KeyCode.UpArrow;
	public KeyCode DownKey = KeyCode.DownArrow;
	public string UpKeyBinding = "P1Up";
	public string DownKeyBinding = "P1Down";
	public Player Owner { get; set; }
	// Use this for initialization
	void Start () {
		UpdateSize();
	}
	
	public void UpdateSize() {
		Vector3 scale = this.transform.localScale;
		scale.y = Length;
		this.transform.localScale = scale;
	}
	
	public KeyCode UpKeyCode() {
		if (UpKeyBinding.Length > 0 && OptionValues.KeyBindings.ContainsKey(UpKeyBinding)) {
			return OptionValues.KeyBindings[UpKeyBinding];
		}
		return UpKey;
	}
	public KeyCode DownKeyCode() {
		if (DownKeyBinding.Length > 0 && OptionValues.KeyBindings.ContainsKey(DownKeyBinding)) {
			return OptionValues.KeyBindings[DownKeyBinding];
		}
		return DownKey;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(UpKeyCode())) {
			RaycastHit hitInfo;
			Vector3 top = this.transform.position + new Vector3(0.0f, Length * 0.5f, 0.0f);
			Ray ray = new Ray(top, new Vector3(0, 1, 0));
			bool collide = Physics.Raycast(ray, out hitInfo, 100.0f);
			if (collide) {
				if (hitInfo.collider.tag == "Powerup" || hitInfo.distance > 0.1) {
					this.transform.Translate(0.0f, Speed, 0.0f);
				}
			}
		}
		else if (Input.GetKey(DownKeyCode())) {
			RaycastHit hitInfo;
			Vector3 bottom = this.transform.position - new Vector3(0.0f, Length * 0.5f, 0.0f);
			Ray ray = new Ray(bottom, new Vector3(0, -1, 0));
			bool collide = Physics.Raycast(ray, out hitInfo, 100.0f);
			if (collide) {
				if (hitInfo.collider.tag == "Powerup" || hitInfo.distance > 0.1) {
					this.transform.Translate(0.0f, -Speed, 0.0f);
				}
			}
		}
	}
	
	public float CalcPuckBounce(Puck puck, ContactPoint contact) {
		// If the contact normal is almost vertical then don't affect anything.
		if (Mathf.Abs(Vector3.Dot(contact.normal, new Vector3(1, 0, 0))) < 0.05) {
			return 0.0f;
		}
		Vector3 localContact = transform.worldToLocalMatrix.MultiplyPoint(contact.point);
		float amount = -360.0f * localContact.y / Mathf.PI * (contact.normal.x > 0 ? -1.0f : 1.0f);
		return amount;
	}
}
