using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {
	
	protected GameObject thumb;
	protected TextMesh valueText;
	
	protected float sliderValue = 0.5f;
	public float SliderValue 
	{
		get
		{
			return sliderValue;
		}
		set
		{
			if (value < MinValue) {
				sliderValue = MinValue;
			}
			else if (value > MaxValue) {
				sliderValue = MaxValue;	
			}
			else {
				sliderValue = value;	
			}
		}
	}
	public float MinValue = 0.0f;
	public float MaxValue = 1.0f;
	protected float size = 1.0f;
	protected Transform backQuad;
	public string Label = "Slider";
	
	// Use this for initialization
	void Start () {
		thumb = transform.FindChild("Thumb").gameObject;
		valueText = transform.FindChild("Value").GetComponent<TextMesh>();
		backQuad = transform.FindChild("Back").transform;
		backQuad.gameObject.AddComponent(typeof(SliderBack));
		backQuad.gameObject.GetComponent<SliderBack>().PercentCallback = PercentCallback;
		size = backQuad.localScale.x - 0.5f;
		
		transform.FindChild("Label").GetComponent<TextMesh>().text = Label;
		UpdateThumb();
	}
	
	void PercentCallback(float percent) {
		SliderValue = percent * (MaxValue - MinValue) + MinValue;
		UpdateThumb();
	}
	
	void UpdateThumb() {
		float percent = (SliderValue - MinValue) / (MaxValue - MinValue);
		thumb.transform.localPosition = new Vector3((percent - 0.5f) * size, 0.0f, 0.0f);
		valueText.text = SliderValue.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
