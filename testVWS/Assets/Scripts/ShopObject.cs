using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
 * 2017.08.09 정다은 생성 
 * 2017.08.21-22 정다은 수정
 * 2017.08.24 정다은 수정, 데이터 구조 변경 
 */

/*
 * 2017.09.13 이진민 vuforia 위치 변경 함수 추가 
 * 
*/

public class ShopObject : MonoBehaviour {
	//variable
	private string name;
	private UInt32 isSale;
	private UInt32 isEvent;
	private UInt32 isCoupon;
	public string category{ get; set; }
	private Location loc;
	public UInt32 shopId{ get; set; }
    

	[SerializeField]
	private UnityEngine.UI.Text shopName;
	[SerializeField]
	private GameObject[] infoIcon;//sale, event,coupon 순서 


    //10.02 진민 추가
    public GameObject gpsManagerObject;
    private GPSManager gpsManager;
    private double distance;
    public Text distanceText;
//    public Text headingText;

	private double lat;
	private double lon;


    void Awake(){
        gpsManagerObject = GameObject.FindGameObjectWithTag("GPSManager");
        gpsManager = gpsManagerObject.GetComponent<GPSManager>();
    }

    void Start(){
        setUILocation();
    }

    void Update(){
        setUILocation();

    }

    public void initialize( UInt32 shopId, string name, UInt32 isSale, UInt32 isEvent, UInt32 isCoupon,
		string category, Location shopPosition){//초기화 

		setData(shopId,name,isSale,isEvent,isCoupon,category, shopPosition.longitude,shopPosition.latitude);

		//이름 바꾸기 
		shopName.text=this.name;
		//아이콘 보이기
		for (int i = 0; i < infoIcon.Length; i++) {
			infoIcon [i].SetActive (false);
		}
		if (this.isSale!=0)
			infoIcon [0].SetActive (true);
		if (this.isEvent!=0)
			infoIcon [1].SetActive (true);
		if (this.isCoupon!=0)
			infoIcon [2].SetActive (true);

		//위치 및 크기 변경
	}

	public void initialize(UInt32 shopId, string name, UInt32 isSale, UInt32 isEvent, UInt32 isCoupon,
		string category, double longitude, double latitude ){//초기화 
		setData(shopId,name,isSale,isEvent,isCoupon,category, longitude,latitude);
	
		//이름 바꾸기 
		shopName.text=this.name;
		//아이콘 보이기
		for (int i = 0; i < infoIcon.Length; i++) {
			infoIcon [i].SetActive (false);
		}
		if (this.isSale!=0)
			infoIcon [0].SetActive (true);
		if (this.isEvent!=0)
			infoIcon [1].SetActive (true);
		if (this.isCoupon!=0)
			infoIcon [2].SetActive (true);

		//위치 및 크기 변경
	}

	private void setData(UInt32 shopId, string name, UInt32 isSale, UInt32 isEvent, UInt32 isCoupon,
		string category, double longitude, double latitude){
		this.shopId = shopId;
		this.name = string.Copy (name);
		this.isSale = isSale;
		this.isEvent = isEvent;
		this.isCoupon = isCoupon;
		this.category = string.Copy (category);
		this.loc.longitude = longitude;
		this.loc.latitude = latitude;
	}

	//거리에 따라서 크기를 조정 
	public void sizeChange( double distance ){
		
	}

/*
 *17.09.13이진민 함수 추가
 * 
 */

    public void setLocation(){

    }

    public Location getLocation(){
        return this.loc;
    }

    private void setUILocation(){
		lat = gpsManager.lat;
		lon = gpsManager.lon;
		double distance = (gpsManager.distanceBetweenPlaces(gpsManager.lat, gpsManager.lon, this.loc.latitude, this.loc.longitude)) * 1000; //m
		double heading = gpsManager.headingAngle(gpsManager.lat, gpsManager.lon, this.loc.latitude, this.loc.longitude);
        double xLocation;
        distanceText.GetComponent<Text>().text = setDistanceText(distance);
        xLocation = gpsManager.setShopPosition(distance, heading);

        setUIScale(distance);
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(setUIXLocation(distance), (float)xLocation);

    }

    private String setDistanceText(double distance){
        String text = "";


        if (distance < 1) {
            text = "1m 이내";
        } else if (distance >= 1) {
            distance.ToString("N2");
            text = distance.ToString("N1") + "m";

        }
        return text;
    }

    private void setUIScale(double distance){
        //scale 1 기준 
        //5m 이내 1, 10m이내 0.8 20m 이내 0.7 20m밖 0.5
        //        Debug.Log("scale: " + this.GetComponent<RectTransform>().localScale);

        this.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        if (distance < 5){
            this.GetComponent<RectTransform>().localScale = new Vector2(1,1);
        } else if (distance >= 5 && distance < 10) {
            this.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
        } else if (distance >= 10 && distance < 20) {
            this.GetComponent<RectTransform>().localScale = new Vector2(0.7f, 0.7f);
        } else if (distance >= 20) {
            this.GetComponent<RectTransform>().localScale = new Vector2(0.5f, 0.5f);
        }
    }

    private int setUIXLocation(double distance) {
        int xLocation = 0;

        if (distance < 5){
            xLocation = 0;
        } else if (distance >= 5 && distance < 10) {
            xLocation = 90;
        } else if (distance >= 10 && distance < 20) {
            xLocation = 120;
        } else if (distance >= 20) {
            xLocation = 200;
        }

        return xLocation;
    }


}
