using UnityEngine;
using System.Data;     //C#의 데이터 테이블 때문에 사용
using MySql.Data;     //MYSQL함수들을 불러오기 위해서 사용
using MySql.Data.MySqlClient;    //클라이언트 기능을사용하기 위해서 사용
using System.Text;//stringbuilder 사용 
using System;

/*
 * 2017.08.06 정다은 생성
 * 2017.08.21-22 정다은 수정 
 * 2017.08.24 정다은 수정, data 구조 변경 
 * reference: http://yourpresence.tistory.com/87
 */

//가게 정보
[Serializable]
public struct Shop{
	//variable
	public UInt32 id;
	public UInt32 userId;

	public string name;
	public string address;
	public double longitude;
	public double latitude;
	public string category;
	public string imagePath;
	public string phone;

//	public DateTime startTime;
//	public DateTime endTime;
	public string startTime;
	public string endTime;
	public string holiday;

	public string explain;
	public UInt32 isCoupon;
	public UInt32 isEvent;
	public UInt32 isSale;

	public Shop(UInt32 id, UInt32 userId,string name,string address,double longitude, double latitude,
		string category, string imagePath, string phone, string startTime, string endTime, string holiday,
		string explain, UInt32 isCoupon, UInt32 isEvent, UInt32 isSale){

		this.id=id;
		this.userId=userId;

		this.name=name;
		this.address= address;
		this.longitude= longitude;
		this.latitude= latitude;
		this.category= category;
		this.imagePath= imagePath;
		this.phone= phone;

		this.startTime= startTime;
		this.endTime= endTime;
		this.holiday= holiday;

		this.explain= explain;
		this.isCoupon= isCoupon;
		this.isEvent= isEvent;
		this.isSale= isSale;
	}

//	public Shop(UInt32 id, UInt32 userId,string name,string address,double longitude, double latitude,
//		string category, string imagePath, string phone, DateTime startTime, DateTime endTime, string holiday,
//		string explain, UInt32 isCoupon, UInt32 isEvent, UInt32 isSale){
//
//		this.id=id;
//		this.userId=userId;
//
//		this.name=name;
//		this.address= address;
//		this.longitude= longitude;
//		this.latitude= latitude;
//		this.category= category;
//		this.imagePath= imagePath;
//		this.phone= phone;
//
//		this.startTime= startTime;
//		this.endTime= endTime;
//		this.holiday= holiday;
//
//		this.explain= explain;
//		this.isCoupon= isCoupon;
//		this.isEvent= isEvent;
//		this.isSale= isSale;
//	}
}

//가게 이벤트 정보 
[Serializable]
public struct ShopEvent{
	public UInt32 id;
	public string category;
	public string imagePath;
	public string explain;

//	public DateTime startTime;
//	public DateTime endTime;
	public string startTime;
	public string endTime;
	public UInt32 shopId;

	public ShopEvent( UInt32 id, string category, string imagePath, string explain, string startTime,
		 string endTime, UInt32 shopId){
		this.id= id;
		this.category= category;
		this.imagePath= imagePath;
		this.explain= explain;
		this.startTime= startTime;
		this.endTime= endTime;
		this.shopId= shopId;
	}

//	public ShopEvent( UInt32 id, string category, string imagePath, string explain, DateTime startTime,
//		DateTime endTime, UInt32 shopId){
//		this.id= id;
//		this.category= category;
//		this.imagePath= imagePath;
//		this.explain= explain;
//		this.startTime= startTime;
//		this.endTime= endTime;
//		this.shopId= shopId;
//	}
}

public class MysqlManager{
	//variable
	MySqlConnection sqlconn = null;
	private string sqlDBip = "127.0.0.1";//192.168.0.5
	private string sqlDBname = "adminPage";
	private string sqlDBid = "springuser";
	private string sqlDBpw = "ThePassword";

	private void sqlConnect()
	{
		//DB정보 입력
		string sqlDatabase = "Server="+ sqlDBip + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw + "";

		//접속 확인하기
		try
		{
			sqlconn =  new MySqlConnection(sqlDatabase);
			sqlconn.Open ();
			Debug.Log("SQL의 접속 상태 : "+sqlconn.State); //접속이 되면 OPEN이라고 나타남
		}
		catch(System.Exception msg) 
		{
			Debug.Log (msg); //기타다른오류가 나타나면 오류에 대한 내용이 나타남
		}
	}

	private void sqldisConnect()
	{
		sqlconn.Close();
		Debug.Log("SQL의 접속 상태 : " + sqlconn.State); //접속이 끊기면 Close가 나타남 
	}

	public void sqlcmdall(string allcmd) //함수를 불러올때 명령어에 대한 String을 인자로 받아옴
	{
		sqlConnect(); //접속

		MySqlCommand dbcmd = new MySqlCommand(allcmd, sqlconn); //명령어를 커맨드에 입력
		dbcmd.ExecuteNonQuery(); //명령어를 SQL에 보냄

		sqldisConnect(); //접속해제
	}

	public DataTable selsql(string sqlcmd)  //리턴 형식을 DataTable로 선언함
	{
		DataTable dt = new DataTable(); //데이터 테이블을 선언함

		sqlConnect();
		MySqlDataAdapter adapter =  new MySqlDataAdapter(sqlcmd, sqlconn);
		adapter.Fill(dt); //데이터 테이블에  채워넣기를함
		sqldisConnect();

		return dt; //데이터 테이블을 리턴함
	}

	private void ShowTable(DataTable table) {
		foreach (DataRow row in table.Rows) {
			foreach (DataColumn col in table.Columns) {
				Debug.Log(row[col]);           
			}

		}
	}

	//모든 가게 정보를 갖고 온다
	public Shop[] getAllShopData(){
		DataTable dt = selsql ("SELECT * from Shop");
		Shop[] sh = new Shop[dt.Rows.Count];

		for (int i = 0; i < dt.Rows.Count; i++) {
			sh [i] = new Shop ((UInt32)dt.Rows [i] [0], (UInt32)dt.Rows [i] [1], (string)dt.Rows [i] [2],
				(string)dt.Rows [i] [3], (double)dt.Rows [i] [4], (double)dt.Rows [i] [5],
				(string)dt.Rows [i] [6], (string)dt.Rows [i] [7], (string)dt.Rows [i] [8], 
				(string)dt.Rows [i] [9], (string)dt.Rows [i] [10], (string)dt.Rows [i] [11],
				(string)dt.Rows [i] [12], (UInt32)dt.Rows [i] [13], (UInt32)dt.Rows [i] [14],
				(UInt32)dt.Rows [i] [15]);
			
		}
		return sh;
	}

	//모든 가게 이벤트 정보를 모두 갖고 온다 
	public ShopEvent[] getAllShopEventData(){
		DataTable dt = selsql ("SELECT * from ShopEvent");
		ShopEvent[] se = new ShopEvent[dt.Rows.Count];

		for (int i = 0; i < dt.Rows.Count; i++) {
			se [i] = new ShopEvent ((UInt32)dt.Rows [i] [0], (string)dt.Rows [i] [1], (string)dt.Rows [i] [2],
				(string)dt.Rows [i] [3], (string)dt.Rows [i] [4], (string)dt.Rows [i] [5],
				(UInt32)dt.Rows [i] [6]);
		}
		return se;
	}

	//가게의 gps 정보들을 주어진 반경 내로 찾는다, 위도 경도 m
	public DataTable searchRadiusShop( double longitude, double latitude, double radius ){
		//위도 경도 0.00001도는 1.1132m
		//20m내를 찾으므로 0.0002의 오차 안에서 조사 
		//double tempRat=ratitude*0.000008983;
		double tempRat=radius*0.00001;

		StringBuilder sb=new StringBuilder();
		sb.Append ("SELECT * from Shop WHERE ");
		sb.Append ("longitude BETWEEN "+(longitude - tempRat)+" AND "+(longitude + tempRat));
		sb.Append("AND latitude BETWEEN "+(latitude-tempRat)+" AND "+(latitude+tempRat));
		DataTable dt=selsql(sb.ToString());
		ShowTable (dt);
		return dt;
	}
}
