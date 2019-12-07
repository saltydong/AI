using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float explosionRadius = 3f;
	private tankType type;
	void OnCollisionEnter(Collision other) {
		myFactory factory = Singleton<myFactory>.Instance;
		ParticleSystem explosion = factory.getPs();
		explosion.transform.position = transform.position;
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].tag == "tankPlayer" && this.type == tankType.Enemy 
				|| colliders[i].tag == "tankEnemy" && this.type == tankType.Player) {
				float distance = Vector3.Distance(colliders[i].transform.position, transform.position);
				float hurt = 100f / distance;
				float currentHp = colliders[i].GetComponent<Tank>().getHp();
				colliders[i].GetComponent<Tank>().setHp(currentHp - hurt);
			}
		}
		explosion.Play();
		if (this.gameObject.activeSelf) {
			factory.recycleBullet(this.gameObject);
		}
		
	}

		
	public void setTankType(tankType type) {
		this.type = type;
	}
}