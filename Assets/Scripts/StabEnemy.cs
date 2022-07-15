using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class StabEnemy: MonoBehaviour {
	public Enemy enemy;
	public float force;
	public float range;
	public float delay;

	public Player player => Game.active.player;
	public Transform target => player.transform;

	void OnValidate() {
		enemy = GetComponent<Enemy>();
		enemy.type = EnemyType.Stab;
	}

	void Start() {
		StartCoroutine(AttackLoop());

		IEnumerator AttackLoop() {
			while (true) {
				if (Vector3.Distance(transform.position, target.position) < range) {
					Attack();
					yield return new WaitForSeconds(delay);
				} else {
					yield return new WaitForFixedUpdate();
				}
			}
		}
	}

	void Attack() {
		player.rb.AddForce((target.position - transform.position).normalized * force);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
