using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 2017.08.10 정다은 생성 
 * 2017.08.11 -13 정다은 수정 
 * 2017.08.24 정다은 수정,
 */

public class CouponManager : MonoBehaviour {//현 사용자 받은 쿠폰 관리 
	//public Coupon[] couponList{get;set;}//일단 최대 10개만 둘 예정 
	public List<Coupon> couponList{get;set;}//일단 최대 10개만 둘 예정 
	public JsonReadWrite jrw{get;set;}

	void Start(){
		jrw = this.GetComponent<JsonReadWrite> ();
		couponList = new List<Coupon> ();
		jrw.readcouponData(this);//맨처음에 가져옴
	//	CouponMaking(new Coupon(15, "lalala","laughing",new System.DateTime(2017,3,5,0,0,0),new System.DateTime(2017,9,20,0,0,0)));
	}

	public int couponInitialize(){//쿠폰 목록 초기화
//		DateManage();
		return couponList.Count;
	}

	public void couponDelete( int index ){//쿠폰 삭제 
		couponList.RemoveAt(index);
		jrw.writeCouponData (this);
	}

	public void CouponMaking(Coupon c){//쿠폰 생성, 다운로드 시  
		couponList.Add(c);
		jrw.writeCouponData (this);
	}

	private void DateManage(){//쿠폰 날짜 관리 
		for (int i = 0; i < couponList.Count; i++) {
			if (couponList [i].endTime.CompareTo(System.DateTime.Now) < 0 ) {
				couponDelete (i);
			}
		}
	}
}
