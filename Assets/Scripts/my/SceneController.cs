using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SceneController : MonoBehaviour, IUserAction {
	public GameObject player;
	private int currentEnemyNum = 5;

	private bool gameOver = false;

	private int enemyConut = 5;
	private myFactory factory;
	public Camera myCamera;

	public Text gameText;
	

	void Awake() {
		GameDirector director = GameDirector.getInstance();
		director.currentSceneController = this;
		factory = Singleton<myFactory>.Instance;
		player = factory.getPlayer();
	}
	void Start () {
		for (int i = 0; i < enemyConut; i++) {
			factory.getTank();
		}
		Player.destroyEvent += setGameOver;
	}
	
	void Update () {
		myCamera.transform.position = new Vector3(player.transform.position.x, 15, player.transform.position.z);
		if(currentEnemyNum == 0) {
			gameText.text = "You win!";
		}		
	}

	public Vector3 getPlayerPos() {
		return player.transform.position;
	}

	public bool isGameOver() {
		return gameOver;
	}
	public void setGameOver() {
		gameOver = true;
		myCamera.transform.parent = null;
		gameText.text = "You lose!";
	}

	public void moveForward() {
		player.GetComponent<Rigidbody>().velocity = player.transform.forward * 20;
	}
	public void moveBackWard() {
		player.GetComponent<Rigidbody>().velocity = player.transform.forward * -20;
	}
	public void turn(float offsetX) {
		float x = player.transform.localEulerAngles.y + offsetX * 5;
        float y = player.transform.localEulerAngles.x;
        player.transform.localEulerAngles = new Vector3 (y, x, 0);
	}	

	public void shoot() {
		GameObject bullet = factory.getBullet(tankType.Player);
		bullet.transform.position = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z) +
			player.transform.forward * 1.5f;
		bullet.transform.forward = player.transform.forward;
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
	}

	public void decreaceCurrentEnemyNum() {
		currentEnemyNum--;
	}

}
