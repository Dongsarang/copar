using System;

/*
 * 2017.08.10 정다은 생성 
 * 2017.08.24 정다은 수정, 데이터 구조 수정
 * reference:https://msdn.microsoft.com/ko-kr/library/8kb3ddd4(v=vs.110).aspx
 */
public class Coupon{//쿠폰 데이터 struct, json으로 저장될 예정 
	//variable
	public long id;//id
	public String storeName;//가게 이름
	public String explain;
	public String startTime;//시작 시간 
	public String endTime;//끝 날짜 

	public Coupon(){

	}

//	public Coupon(long id, String storeName, String explain, DateTime startTime, DateTime endTime){
//		this.id = id;
//		this.storeName = String.Copy (storeName);
//		this.explain = explain;
//		this.startTime = startTime;
//		this.endTime = endTime;
//	}

//	public Coupon(long id, String storeName, String explain, String startTime, String endTime){
//		this.id = id;
//		this.storeName = String.Copy (storeName);
//		this.explain = String.Copy (explain);
//		this.startTime = Convert.ToDateTime (startTime);
//		this.endTime = Convert.ToDateTime (endTime);
//	}

	public Coupon(System.UInt32 id, String storeName, String explain, String startTime, String endTime){
		this.id = id;
		this.storeName = String.Copy (storeName);
		this.explain = String.Copy (explain);
		this.startTime = String.Copy (startTime);
		this.endTime = String.Copy (endTime);
	}

	public Coupon(long id, String storeName, String explain, String startTime, String endTime){
		this.id = id;
		this.storeName = String.Copy (storeName);
		this.explain = String.Copy (explain);
		this.startTime = String.Copy (startTime);
		this.endTime = String.Copy (endTime);
	}

	public override string ToString(){
		return (storeName + "\n설   명:"+explain+"\n이벤트 기간:"+startTime+"-"+endTime+"\n");
	}
}
