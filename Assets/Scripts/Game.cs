using UnityEngine;

public class Game: MonoBehaviour {
	public static Game active;

	public Player player;

	void Awake() {
		active = this;
	}
}
