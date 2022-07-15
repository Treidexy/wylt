using UnityEngine;

public enum EnemyType {
	Spear,
	Stab,
}

public class Enemy: MonoBehaviour {
	public Rigidbody2D rb;
	public EnemyType type;

	public Transform sight;
	public bool inSight;
	public float stoppingDistance;
	public float speed;

	public Transform target => Game.active.player.transform;

	void OnValidate() {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
		if (Mathf.Abs(transform.position.x) < stoppingDistance) {
			transform.position += (Vector3) Random.insideUnitCircle * 0.02f;
			return;
		}

		float x = transform.position.x + speed * Time.deltaTime * -Mathf.Sign(transform.position.x);
		RaycastHit2D hit = Physics2D.BoxCast(new Vector2(x, 5), new Vector2(transform.lossyScale.x, 0.1f), 0, Vector2.down, 15, LayerMask.GetMask("Ground"));
		if (hit.collider != null) {
			transform.position = new Vector3(x, 5 - hit.distance + transform.lossyScale.y / 2);
		} else {
			transform.position = new Vector3(x, transform.position.y);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(-stoppingDistance, 5), new Vector3(-stoppingDistance, -5));
		Gizmos.DrawLine(new Vector3(stoppingDistance, 5), new Vector3(stoppingDistance, -5));
	}
}
