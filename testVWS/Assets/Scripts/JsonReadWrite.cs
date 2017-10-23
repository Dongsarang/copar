using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;

/*
 * 2017.08.10 정다은 생성
 * 2017.08.11 정다은 수정 
 * 2017.08.22 정다은 수정, 가게를 처음 시작할 때 mysql과 연결해서 정보를 갖고 오게 한다
 * reference: http://pjc0247.tistory.com/32
 * reference: http://debuglog.tistory.com/36
 */

//쿠폰 등 localData를 json 형태로 읽고 저장한다 
public class JsonReadWrite : MonoBehaviour {
	private CouponM[] couponList;
	public String path{ get; set; }
	private MysqlManager mm;

	public Shop[] sh{ get; set; }
	public ShopEvent[] se{ get; set; }

	public Text dataPath;

	void Start(){
		
#if UNITY_EDITOR
		path = Application.dataPath + "/Resources/etc";
#endif
#if UNITY_Android
		path =  "jar:file://" + Application.dataPath + "!/Resources/etc/";
#endif
        
        initialize();

        /*
		sh = shopJsontoStruct ();
		if (sh == null) {
			dataPath.GetComponent<Text> ().text = "is null" + "path: " + path + "data: " + sh[0].id + ", " + sh.Length;
		} else {
            dataPath.GetComponent<Text>().text = "not null " + "path: " + path + "and data: ";
            for (int i=0;i<sh.Length;i++){
                dataPath.GetComponent<Text>().text += sh[i].name;
            }
		}*/
    }

    public void initialize(){
		getCouponList ();

        Debug.Log("DB접속 제외 코드");
        //이진민 09.15 DB접속 제외하기 위해 주석처리
//		shopMysqltoJson ();
//		shopEventSqltoJson ();
//
//		sh=shopJsontoStruct ();
//		se=shopEventJsontoStruct ();
	}

	public void initializeMain(){
		sh=shopJsontoStruct ();
		se=shopEventJsontoStruct ();
	}

	/// //////////////////////////////////////////////////////
	/// 쿠폰 관련 함수 
	/// //////////////////////////////////////////////////////
	public int leng(){
		return couponList.Length;
	}

	public void readcouponData(CouponManager cm){//맨처음에 정보를 가져옴 
		for (int i = 0; i < couponList.Length; i++) {
			cm.couponList.Add(new Coupon (
				couponList [i].id, couponList [i].storeName, couponList [i].explain, 
				couponList [i].startTime, couponList [i].endTime));
		}
	}

	private void getCouponList(){
		TextAsset t = Resources.Load ("etc/couponList",typeof(TextAsset)) as TextAsset;
		if (!t)
			Debug.Log ("CouponList cannot be loaded!");

		//배열인 json 데이터 구조를 읽어들인다.
		string tempData=t.text;
		//string tempData = "{\"Items\":" + t.text + "}";
		couponList = JsonHelper.FromJson<CouponM>(tempData);
		if (couponList == null)// if it cannot change string to object
			Debug.Log("Can't make object");
	}

	public void writeCouponData(CouponManager cm){
		couponList = new CouponM[cm.couponList.Count];
		for (int i = 0; i < cm.couponList.Count; i++) {
			couponList [i] = new CouponM (
				cm.couponList [i].id, cm.couponList [i].storeName, cm.couponList [i].explain,
				cm.couponList [i].startTime.ToString (), cm.couponList [i].endTime.ToString ());
		}
		writeCouponList ();
	}

	private void writeCouponList(){
		String tempData = JsonHelper.ToJson<CouponM> (couponList);
		File.WriteAllText (path + "/couponList.txt", tempData);
	}

	[Serializable]
	public struct CouponM{//쿠폰 데이터 struct, json으로 저장될 예정 
		//variable
		public long id;//id
		public String storeName;//가게 이름
		public String explain;
		public String startTime;//시작 시간 
		public String endTime;//끝 날짜 

		public CouponM(long id, String storeName, String explain, String startTime, String endTime){
			this.id = id;
			this.storeName = String.Copy (storeName);
			this.explain = String.Copy(explain);
			this.startTime = String.Copy (startTime);
			this.endTime = String.Copy (endTime);
		}
	}

	/// //////////////////////////////////////////////////////
	/// 가게 관련 함수 
	/// //////////////////////////////////////////////////////
	private void shopMysqltoJson(){//json파일에 정보를 쓴다. 
		mm=new MysqlManager();//mysql에서 정보를 갖고 온다.
		Shop[] shop= mm.getAllShopData();

		String tempData = JsonHelper.ToJson<Shop> (shop);//json파일에 정보를 쓴다. 
		File.WriteAllText (path + "/shopInformation.txt", tempData);
	}

	public Shop[] shopJsontoStruct(){//json->구조체 배열 
		TextAsset t = Resources.Load ("etc/shopInformation",typeof(TextAsset)) as TextAsset;
		if (!t)
			Debug.Log ("shop list cannot be loaded!");

		//배열인 json 데이터 구조를 읽어들인다.
		string tempData=t.text;
		//string tempData = "{\"Items\":" + t.text + "}";
		Shop[] list = JsonHelper.FromJson<Shop>(tempData);
		if (list == null)// if it cannot change string to object
			Debug.Log("Can't make object");

		return list;
	}

	private void shopEventSqltoJson(){//json파일에 정보를 쓴다. 
		mm=new MysqlManager();//mysql에서 정보를 갖고 온다.
		ShopEvent[] shopEvent= mm.getAllShopEventData();

		String tempData = JsonHelper.ToJson<ShopEvent> (shopEvent);//json파일에 정보를 쓴다. 
		File.WriteAllText (path + "/shopEvent.txt", tempData);
	}

	public ShopEvent[] shopEventJsontoStruct(){//json->구조체 배열 
		TextAsset t = Resources.Load ("etc/shopEvent",typeof(TextAsset)) as TextAsset;
		if (!t)
			Debug.Log ("shop event list cannot be loaded!");

		//배열인 json 데이터 구조를 읽어들인다.
		string tempData=t.text;
		ShopEvent[] list = JsonHelper.FromJson<ShopEvent>(tempData);
		if (list == null)// if it cannot change string to object
			Debug.Log("Can't make object");

		return list;
	}
}
