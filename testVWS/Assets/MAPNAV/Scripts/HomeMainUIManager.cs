using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//home button ui manager
//10.17 이진민 추가
public class HomeMainUIManager : MonoBehaviour {

    public GameObject menuBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clickMenuIcon(){
        if (menuBar.activeInHierarchy ==  true){
            menuBar.SetActive(false);
        } else {
            menuBar.SetActive(true);
        }
    }
}
