using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void toMAPNAV(){
        Application.LoadLevel("MapNav_2D_Demo");
    }

    public void toARMain(){
        Application.LoadLevel("main");
    }

    public void toHome(){
        Application.LoadLevel("home");
    }
}
