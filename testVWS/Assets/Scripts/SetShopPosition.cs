using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShopPosition : MonoBehaviour {
    private GPSManager gpsManager;
    private Location gpsLocation;

    void Awake(){
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
//        Debug.Log("set shop position script: " + gpsLocation.latitude + ", " + gpsLocation.longitude);

    }

    private void getShopObject(){
        //        gpsLocation = this.GetComponents<ShopObject>().SetValue();
        gpsLocation = this.gameObject.GetComponent<ShopObject>().getLocation();
    }
}
