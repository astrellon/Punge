using UnityEngine;
using System;
using System.Collections;

public class Slider : MonoBehaviour {
	
	public class ChangeEventArgs : EventArgs {
		public float Value;
		public ChangeEventArgs(float value) {
			Value = value;	
		}
	}
	public event EventHandler<ChangeEventArgs> Changed;
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
			float newValue = value;
			if (value < MinValue) {
				newValue = MinValue;
			}
			else if (value > MaxValue) {
				newValue = MaxValue;	
			}
			else if (StepSize > 0.0f) {
				float temp = value / StepSize;
				temp = Mathf.Round (temp) * StepSize;
				newValue = temp;
			}
			if (newValue != sliderValue) {
				sliderValue = newValue;
				if (Changed != null) {
					Changed(this, new ChangeEventArgs(sliderValue));	
				}
			}
		}
	}
	public float StartValue = 0.5f;
	public float MinValue = 0.0f;
	public float MaxValue = 1.0f;
	public float StepSize = 0.1f;
	protected float size = 1.0f;
	protected Transform backQuad;
	public string Label = "Slider";
	
	// Use this for initialization
	void Start () {
		SliderValue = StartValue;
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
	
	public void UpdateThumb() {
		float percent = (SliderValue - MinValue) / (MaxValue - MinValue);
		thumb.transform.localPosition = new Vector3((percent - 0.5f) * size, 0.0f, 0.0f);
		valueText.text = SliderValue.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
