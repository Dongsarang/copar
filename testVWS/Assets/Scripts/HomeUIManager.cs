using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 2017.10.07 이진민 생성
//home scene의 shopObject 관리를 위한 script

public class HomeUIManager : MonoBehaviour {

    [SerializeField]//for main page, shop objects
    private HomeShopList[] soManager;//shopObject array
    private InfoManager infoManager;

    private List<Shop> dt;

    private GPSManager gps;


    // Use this for initialization
    void Start () {
        infoManager = this.GetComponent<InfoManager>();//shopObject 보여주기 
        shopObjectInitialize();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //MainUIManager script 인용
    private void shopObjectInitialize(){//shop object들 초기화 
        dt = infoManager.searchGPSInfo(126.945996, 37.557636);
        Debug.Log("이부분 gps.lon gps.lat 문제로 임의로 gps 정보 넣음 아진짜 왜 에러가 나는지 1도모르겠다 으허엉");

        for (int i = 0; i < soManager.Length; i++){
            soManager[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < dt.Count; i++){//각각의 shopObject에게 정보를 넘김 
            soManager[i].gameObject.SetActive(true);
            soManager[i].initialize(dt[i].id, dt[i].name, dt[i].isSale, dt[i].isEvent, dt[i].isCoupon,
                dt[i].category, new Location(dt[i].longitude, dt[i].latitude));

            Debug.Log("shop name: " + soManager[i].shopId);
        }
    }
}
