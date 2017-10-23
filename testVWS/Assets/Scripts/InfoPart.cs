using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 2017.08.14 정다은 생성
 * 201708.23 수정
 */

public class InfoPart:MonoBehaviour{//information page의 UI를 담당하는 class
	[SerializeField]
	private Image im;
	[SerializeField]
	private Text[] textUI;

	public void infoInitialize(Shop sh){
		textUI [0].text = sh.name;
		textUI [1].text = sh.address;
		textUI [2].text = sh.startTime;
		textUI [3].text = sh.category;
	}

	public void infoInitialize(ShopEvent sh){
		textUI [0].text = sh.category;
		textUI [1].text = sh.startTime;
		textUI [2].text = sh.explain;
	}
}
