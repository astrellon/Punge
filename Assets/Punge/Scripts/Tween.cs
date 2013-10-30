using UnityEngine;
using System.Collections;

public class Tween : MonoBehaviour {
	
	public Vector3 StartValue;
	public Vector3 EndValue;
	public float Duration = 1.0f;
	protected float endTime = 0.0f;
	protected bool active = false;
	
	public void SetActive(bool active, bool autoSetStartValue = false) {
		this.active = active;
		endTime = Time.time + Duration;
		if (autoSetStartValue) {
			StartValue = transform.localPosition;	
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			float t = 1.0f - ((endTime - Time.time) / Duration);
			if (t < 0.0f) {
				t = 0.0f;
			}
			if (t > 1.0f) {
				t = 1.0f;
				active = false;
			}
			Vector3 temp = new Vector3();
			temp.x = Lerp(StartValue.x, EndValue.x, t);
			temp.y = Lerp(StartValue.y, EndValue.y, t);
			temp.z = Lerp(StartValue.z, EndValue.z, t);
			transform.localPosition = temp;
		}
	}
	
	float Lerp(float a, float b, float t) {
		return 	a + (b - a) * t;
	}
}
