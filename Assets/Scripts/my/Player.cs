using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : Tank {
	public delegate void destroy();
	public static event destroy destroyEvent;

	public Slider m_Slider;                             
    public Image m_FillImage;                           
    public Color m_FullHealthColor = Color.green;       
    public Color m_ZeroHealthColor = Color.red;         

	void Start() {
		setHp(500f);
	}
	void Update () {
		if (getHp() <= 0 ) {
			this.gameObject.SetActive(false);
			if (destroyEvent != null) {
				destroyEvent();
			}
		}
		else {
			SetHealthUI();
		}
	}

	private void SetHealthUI ()
    {
        m_Slider.value = getHp() / 500f * 100f;
		Debug.Log(getHp());
    }
}

