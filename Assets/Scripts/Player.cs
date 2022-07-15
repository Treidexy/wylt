using UnityEngine;

public class Player: MonoBehaviour {
	public float termVel;
	public float speed;
	public float jumpForce;

	public Rigidbody2D rb;

	public bool isGrounded;

	void OnValidate() {
		if (rb == null) {
			rb = GetComponent<Rigidbody2D>();
		}
	}

	void FixedUpdate() {
		if (transform.position.y < -5.5f) {
			print("game over.");
		}

		Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0);
		if (isGrounded) {
			if (Input.GetAxisRaw("Vertical") > 0) {
				move.y = jumpForce;
			}

			isGrounded = false;
		}

		rb.AddForce(move);
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, termVel);
	}

	void OnCollisionEnter2D(Collision2D hit) {
		if (hit.gameObject.layer == LayerMask.GetMask("Enemy")) {
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			Enemy enemy = hit.gameObject.GetComponent<Enemy>();
			switch (enemy.type) {
			case EnemyType.Spear:
				Destroy(enemy.gameObject);
				break;
			case EnemyType.Stab:
				break;
			}
		}
	}

	void OnCollisionStay2D(Collision2D hit) {
		isGrounded = true;
	}
}
