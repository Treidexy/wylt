using UnityEngine;

public class Spear: MonoBehaviour {
	public Rigidbody2D rb;
	public float kb;

	void OnValidate() {
		if (rb == null) {
			rb = GetComponent<Rigidbody2D>();
		}
	}

	void OnCollisionEnter2D(Collision2D hit) {
		if (hit.gameObject.tag == "Player") {
			hit.gameObject.GetComponent<Player>().rb.AddForce(rb.velocity.normalized * kb, ForceMode2D.Impulse);
		}

		Destroy(gameObject);
	}
}
