using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class SpearEnemy: MonoBehaviour {
	public Enemy enemy;
	public GameObject spearPrefab;
	public Transform spearSpawn;
	public float force;
	public float range;
	public float delay;

	public Transform target => Game.active.player.transform;

	void OnValidate() {
		enemy = GetComponent<Enemy>();
		enemy.type = EnemyType.Spear;
	}

	void Start() {
		StartCoroutine(Attack());

		IEnumerator Attack() {
			while (true) {
				if (Vector3.Distance(transform.position, target.position) < range) {
					Shoot();
					yield return new WaitForSeconds(delay);
				} else {
					yield return new WaitForFixedUpdate();
				}
			}
		}
	}

	void Shoot() {
		GameObject spear = Instantiate(spearPrefab, spearSpawn.position, Quaternion.identity);
		Vector2 dir = (new Vector2(target.position.x, target.position.y + (target.transform.position.y - spearSpawn.position.y)) - (Vector2) spearSpawn.position).normalized;
		spear.GetComponent<Rigidbody2D>().AddForce(dir * force);
		spear.transform.rotation = Quaternion.FromToRotation(spearSpawn.position, dir);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
