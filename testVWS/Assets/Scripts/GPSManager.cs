using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GPSManager : MonoBehaviour {

    public double lat = 10;
    public double lon = 10;
    private double lastlat = 0, lastlon = 0;

    private double distanceTest;

    //mobile device direction
    public Text textHeading;
    public Text textLocation;

    public double heading;

    List<GameObject> shopList = new List<GameObject>();


    private float panelWidth;
    private float panelHeight;

    private GameObject mainCanvas;
    private RectTransform rt;

    void Awake(){
        mainCanvas = GameObject.FindGameObjectWithTag("mainCanvas");
        rt = mainCanvas.GetComponent<RectTransform>();

        Debug.Log("get rt: " + rt.rect.width + ", " + rt.rect.height);

        if (!Input.location.isEnabledByUser){
            //for testing MainUIManger!
            Debug.Log("set lat and lon for testing MainUIManager GPS");
            lat = 37.557636;
            lon = 126.945996;

			Debug.Log ("call lat and lon  " + lat  +" " + lon);
        }
    }

    IEnumerator Start() {

        textHeading.GetComponent<Text>().text = "heading set: NULL ";
        textLocation.GetComponent<Text>().text = "location set: NULL";

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("location is Enabled by user");
            textHeading.GetComponent<Text>().text = "the location is Enabled by user";
            textLocation.GetComponent<Text>().text = "the location is Enabled by user";



        }
        else
        {
            // Start service before querying location
            Input.location.Start();

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                Debug.Log("Timed out");
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
                yield break;
            }
            else
            {

                textHeading.GetComponent<Text>().text =
                    "Enable?: " + Input.compass.enabled + "\n"
                    + " MagneticHeading: " + Input.compass.magneticHeading + "\n"
                    + " headingAccuracy: " + Input.compass.headingAccuracy + "\n"
                    + " rawVector: " + Input.compass.rawVector + "\n"
                    + " trueHeading: " + Input.compass.trueHeading;

                heading = Input.compass.magneticHeading;

                textLocation.GetComponent<Text>().text =
                    "Lat and lon: " + Input.location.lastData.latitude + ", " + Input.location.lastData.longitude;
				lat = Input.location.lastData.latitude;
				lon = Input.location.lastData.longitude;

            }
        }

        // Stop service if there is no need to query location updates continuously
        //        Input.location.Stop();
    }


    void Update() {
        updateGPS();
        updateHeading();

        //        setShopPosition(0,0);
    }

    private void updateGPS() {
        //      <---------Mobile Device Code----------->
        if (Input.location.isEnabledByUser) {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
            if (lastlat != lat || lastlon != lon) {
                /*
                map.GetComponent<GoogleMap>().centerLocation.latitude = lat;
                map.GetComponent<GoogleMap>().centerLocation.longitude = lon;
                latText.GetComponent<Text>().text = "Lat" + lat.ToString();
                lonText.GetComponent<Text>().text = "Lon" + lon.ToString();
                //spawn.GetComponent<Spawn> ().updateMonstersPosition (lon, lat);
                //Add above after you complete spawn part
                map.GetComponent<GoogleMap>().Refresh();
                */
                //map.GetComponent<MapNav>().refreshMapPosition();

                textLocation.GetComponent<Text>().text =
                        "Lat and lon: " + Input.location.lastData.latitude + ", " + Input.location.lastData.longitude;
            }
            lastlat = lat;
            lastlon = lon;
        } else {
            return;
        }
    }

    private void updateHeading(){
        if (!Input.location.isEnabledByUser){
//			Debug.Log ("heading doesnt work " + heading);
            return;
        } else {
//			Debug.Log ("heading work " + heading);
            textHeading.GetComponent<Text>().text =
                    "Enable?: " + Input.compass.enabled + "\n"
                    + " MagneticHeading: " + Input.compass.magneticHeading + "\n"
                    + " headingAccuracy: " + Input.compass.headingAccuracy + "\n"
                    + " rawVector: " + Input.compass.rawVector + "\n"
                    + " trueHeading: " + Input.compass.trueHeading;

            heading = Input.compass.magneticHeading;
//			heading = 0;

        }
    }


    public double distanceBetweenPlaces(double lat1, double lon1, double lat2, double lon2){
        double radius = 6371;
        double dLat = this.toRadian(lat2 - lat1);
        double dLon = this.toRadian(lon2 - lon1);
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(this.toRadian(lat1)) * Math.Cos(this.toRadian(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
        double d = radius * c;
        return d;
    }

    // Convert to Radians.
    private double toRadian(double val) {
        return (Math.PI / 180) * val;
    }

    //북쪽, 사용자, shop의 상대적 angle
    public double headingAngle(double lat1, double lon1, double lat2, double lon2) {
        double dLon = (lon2 - lon1);

        double y = Math.Sin(dLon) * Math.Cos(lat2);
        double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1)
                * Math.Cos(lat2) * Math.Cos(dLon);

        double brng = Math.Atan2(y, x);

        brng = Mathf.Rad2Deg * brng;

        /*
        brng = DegreeToRadian(brng);
        brng = (brng + 360) % 360;
        brng = 360 - brng; // count degrees counter-clockwise - remove to make clockwise
        */

        return brng;
    }


    private double compareTwoVector(double v0, double v1){
        //+ 는 왼쪽 화면 - 는 오른쪽 화면
        //v0 는 사용자가 바라보는 vector, v1은 북쪽을 기준으로 사용자와 shop의 vector
        double v = v0 - v1;

        if (v > 180){
            v= v - 360;
        } else if (v < -180) {
            v = v + 360;
        }

        return v;
    }

    public double setShopPosition(double distance, double angle){
        //+는 왼쪽 화면 - 는 오른쪽 화면
        //heading - 사용자가 향하는 방향
        //headingAngle(37.38172, 127.1305, 37.38172, 127.1308)) - 북쪽, 사용자, shop의 상대적 angle

        //for test
//        double shopAngleTest = headingAngle(37.38172, 127.1305, 37.38172, 127.1308);
//        double distanceTest = DistanceBetweenPlaces(37.38172, 127.1305, 37.38172, 127.1308);

//        double v1 = compareTwoVector(heading, shopAngleTest);
        double v = compareTwoVector(heading + 90, angle);


        double xLocation = 0;//shop 의 x 좌표값
        float R = 0.1f; //상대값
        float canvasHeight = rt.rect.height;

//        Debug.Log("canvaswidht" + canvasHeight);


        //v : canvasWidth = 90 :  canvasWidht/2 
        // v = 0 이면 canvasWidht/2 
        // v 이 90 or -90 이면 0 , canvasWidht이어야함
        /*
        if (v < 0){ // - 는 오른쪽 화면
            xLocation = distance * angle * R *-1 ; 
        } else {
            xLocation = distance * angle * R;
        }*/
        //v가 -면 오른쪽, +면 왼쪽임 가운데가 0임
        if (v<10 && v>-10) {
            xLocation = 0;
        } else if (v<=-10){ // 오른쪽 바깥, 즉 v는 -, pos: -canvasWidth/2 을 지나야함
            xLocation = (canvasHeight / (2 * 90)) * v;
        } else if(v>=10) { //왼쪽 바깥, 즉 v 는 +, pos:+canvasWidth/2 을 지나야
            xLocation = (canvasHeight / (2 * 90)) * v;
        }

//        Debug.Log("v: " + v);
         
        return xLocation;

    }

    private void findShopObject(){
        GameObject shopObjectTest = GameObject.FindGameObjectWithTag("shopObject").GetComponent<GameObject>();
    }




}
