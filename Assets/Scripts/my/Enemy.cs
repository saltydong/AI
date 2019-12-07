using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Tank {
	public delegate void recycle(GameObject Tank);
	public static event recycle recycleEvent;
	
	private Vector3 target;

	private bool gameover;

	void Start() {
		setHp(100f);	
		StartCoroutine(shoot());
	}

	void Update () {
		gameover = GameDirector.getInstance().currentSceneController.isGameOver();
		if (!gameover) {
			target = GameDirector.getInstance().currentSceneController.getPlayerPos();		
			if (getHp() <= 0 && recycleEvent != null) {
				recycleEvent(this.gameObject);
				GameDirector.getInstance().currentSceneController.decreaceCurrentEnemyNum();
			} 
			else {
				NavMeshAgent agent = GetComponent<NavMeshAgent>();
				agent.SetDestination(target);
			}
		} 
		else {
			NavMeshAgent agent = GetComponent<NavMeshAgent>();
			agent.velocity = Vector3.zero;
			agent.ResetPath();
		}
		
	}

	IEnumerator shoot() {
		while(!gameover) {
			for (float i = 1; i > 0; i -= Time.deltaTime) {
				yield return 0;	
			}
			if (Vector3.Distance(transform.position, target) < 20) {
				myFactory factory = Singleton<myFactory>.Instance;
				GameObject bullet = factory.getBullet(tankType.Enemy);
				bullet.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z) +
					transform.forward * 1.5f;
				bullet.transform.forward = transform.forward;
				Rigidbody rb = bullet.GetComponent<Rigidbody>();
				rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
			}
		}
	}
}
