using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	
	public float Length = 4.0f;
	public float Speed = 0.2f;
	public KeyCode UpKey = KeyCode.UpArrow;
	public KeyCode DownKey = KeyCode.DownArrow;
	// Use this for initialization
	void Start () {
		Vector3 scale = this.transform.localScale;
		scale.y = Length;
		this.transform.localScale = scale;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(UpKey)) {
			RaycastHit hitInfo;
			Vector3 top = this.transform.position + new Vector3(0.0f, Length * 0.5f, 0.0f);
			Ray ray = new Ray(top, new Vector3(0, 1, 0));
			bool collide = Physics.Raycast(ray, out hitInfo, 100.0f);
			if (collide) {
				if (hitInfo.distance > 0.1) {
					this.transform.Translate(0.0f, Speed, 0.0f);
				}
			}
		}
		else if (Input.GetKey(DownKey)) {
			RaycastHit hitInfo;
			Vector3 bottom = this.transform.position - new Vector3(0.0f, Length * 0.5f, 0.0f);
			Ray ray = new Ray(bottom, new Vector3(0, -1, 0));
			bool collide = Physics.Raycast(ray, out hitInfo, 100.0f);
			if (collide) {
				if (hitInfo.distance > 0.1) {
					this.transform.Translate(0.0f, -Speed, 0.0f);
				}
			}
		}
	}
}
