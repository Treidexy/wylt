using System.Collections;
using UnityEngine;

public class Spawner: MonoBehaviour {
	public GameObject[] prefabs;
	public float[] rates;
	public Vector2 delayRange;

	public float totalRate;

	void OnValidate() {
		totalRate = 0;
		for (int i = 0; i < rates.Length; i++) {
			totalRate += rates[i];
		}
	}

	void Start() {
		StartCoroutine(Spawn());

		IEnumerator Spawn() {
			while (true) {
				yield return new WaitForSeconds(Random.Range(delayRange.x, delayRange.y));
				float v = Random.Range(0, totalRate);
				for (int i = 0; i < rates.Length; i++) {
					if (v <= rates[i]) {
						Instantiate(prefabs[i], transform.position, Quaternion.identity);
						break;
					} else {
						v -= rates[i];
					}
				}
			}
		}
	}
}
