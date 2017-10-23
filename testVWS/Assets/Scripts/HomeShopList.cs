using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
 10.15 진민 추가
 ShopObject 처럼 하고 싶었는데 script 특성상 location 바꿔주는게 있어서  ㅠㅠㅠ
     */

public class HomeShopList : MonoBehaviour {
    //variable
    private string name;
    private UInt32 isSale;
    private UInt32 isEvent;
    private UInt32 isCoupon;
    public string category { get; set; }
    private Location loc;
    public UInt32 shopId { get; set; }


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


    void Awake()
    {
        gpsManagerObject = GameObject.FindGameObjectWithTag("GPSManager");
        gpsManager = gpsManagerObject.GetComponent<GPSManager>();
    }


    public void initialize(UInt32 shopId, string name, UInt32 isSale, UInt32 isEvent, UInt32 isCoupon,
        string category, Location shopPosition)
    {//초기화 

        setData(shopId, name, isSale, isEvent, isCoupon, category, shopPosition.longitude, shopPosition.latitude);

        //이름 바꾸기 
        shopName.text = this.name;
        //아이콘 보이기
        for (int i = 0; i < infoIcon.Length; i++)
        {
            infoIcon[i].SetActive(false);
        }
        if (this.isSale != 0)
            infoIcon[0].SetActive(true);
        if (this.isEvent != 0)
            infoIcon[1].SetActive(true);
        if (this.isCoupon != 0)
            infoIcon[2].SetActive(true);

        //위치 및 크기 변경
    }

    public void initialize(UInt32 shopId, string name, UInt32 isSale, UInt32 isEvent, UInt32 isCoupon,
        string category, double longitude, double latitude)
    {//초기화 
        setData(shopId, name, isSale, isEvent, isCoupon, category, longitude, latitude);

        //이름 바꾸기 
        shopName.text = this.name;
        //아이콘 보이기
        for (int i = 0; i < infoIcon.Length; i++)
        {
            infoIcon[i].SetActive(false);
        }
        if (this.isSale != 0)
            infoIcon[0].SetActive(true);
        if (this.isEvent != 0)
            infoIcon[1].SetActive(true);
        if (this.isCoupon != 0)
            infoIcon[2].SetActive(true);

        //위치 및 크기 변경
    }

    private void setData(UInt32 shopId, string name, UInt32 isSale, UInt32 isEvent, UInt32 isCoupon,
        string category, double longitude, double latitude)
    {
        this.shopId = shopId;
        this.name = string.Copy(name);
        this.isSale = isSale;
        this.isEvent = isEvent;
        this.isCoupon = isCoupon;
        this.category = string.Copy(category);
        this.loc.longitude = longitude;
        this.loc.latitude = latitude;
    }
    
}
