﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUserGUI : MonoBehaviour {
	IUserAction action;
	// Use this for initialization
	void Start () {
		action = GameDirector.getInstance().currentSceneController as IUserAction;	
	}
	
	// Update is called once per frame
	void Update () {
		if (!action.isGameOver()) {
			if (Input.GetKey(KeyCode.W)) {
				action.moveForward();
			}
			
			if (Input.GetKey(KeyCode.S)) {
				action.moveBackWard();
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				action.shoot();	
			}

			if(Input.GetMouseButtonUp(0)) {
				action.shoot();
			}

			float offsetX = Input.GetAxis ("Mouse X") * 2.0f;
			action.turn(offsetX);
		}
	}
}
