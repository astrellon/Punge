using UnityEngine;
using System.Collections;

public class SliderBack : MonoBehaviour {
	
	private Plane hitPlane;
	private bool mouseDown = false;
	void OnMouseDown() {
		mouseDown = true;
		hitPlane = new Plane(transform.forward, transform.position);
		DoCast ();
	}
	
	public delegate void SliderPercent(float percent);
	public SliderPercent PercentCallback;
	
	private void DoCast() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		float dist = -1.0f;
		if (hitPlane.Raycast(ray, out dist)) {
			Vector3 localPoint = transform.worldToLocalMatrix.MultiplyPoint3x4(ray.GetPoint(dist));
			float percent = localPoint.x + 0.5f;
			if (PercentCallback != null) {
				PercentCallback(percent);	
			}
		}
	}
	
	void Update() {
		if (mouseDown) {
			DoCast();
			if (!Input.GetMouseButton(0)) {
				mouseDown = false;	
			}
		}
	}
}
