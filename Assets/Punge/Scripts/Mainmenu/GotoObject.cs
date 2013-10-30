using UnityEngine;
using System.Collections;

public class GotoObject : MonoBehaviour {
	
	public GameObject Goto;
	
	void OnMouseDown () {
		Tween tween = Camera.main.GetComponent<Tween>();	
		Vector3 endValue = Camera.main.transform.localPosition;
		Vector3 endPos = Goto.transform.localPosition;
		endValue.x = endPos.x;
		endValue.y = endPos.y;
		tween.EndValue = endValue;
		tween.SetActive(true, true);
	}
	
}
