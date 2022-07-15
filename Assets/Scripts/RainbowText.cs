using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class RainbowText: MonoBehaviour {
	public TMP_Text text;
	
	public float speed;

	void OnValidate() {
		text = GetComponent<TMP_Text>();
	}

	void FixedUpdate() {
		Color.RGBToHSV(text.color, out float h, out float s, out float v);
		text.color = Color.HSVToRGB(h + speed * Time.deltaTime, s, v);
	}
}
