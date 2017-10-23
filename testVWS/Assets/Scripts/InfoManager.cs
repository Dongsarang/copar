using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data; //C#의 데이터 테이블 때문에 사용
using System.Linq;

/*
 * 2017.08.14 정다은 생성
 * 201708.22-23 수정
 */

public struct Location{
	public double longitude;
	public double latitude;

	public Location(double longitude, double latitude){
		this.longitude = longitude;
		this.latitude = latitude;
	}
}

public class InfoManager : MonoBehaviour {
	private JsonReadWrite jrw;
	private Shop[] sh;

	void Start(){
		jrw = this.GetComponent<JsonReadWrite> ();
	}

	//가게의 gps 정보들을 반경 20m내로 찾는다 
	public List<Shop> searchGPSInfo( double longitude, double latitude ){
		double radius=0.00001*1000;//100m
		jrw = this.GetComponent<JsonReadWrite> ();
		jrw.initializeMain ();
		sh = jrw.sh;

		var query = from shop in jrw.sh
		       where ((shop.longitude > (longitude - radius)) && (shop.longitude < (longitude + radius)))
		           && ((shop.latitude > (latitude - radius)) && (shop.latitude < (latitude + radius)))
		       select shop;

		return query.ToList();
	}

	//가게 정보를 반환한다 
	public List<Shop> getInformation(System.UInt32 shopId){
		var query = from shop in jrw.sh
		          where shop.id == shopId
		          select shop;

		return query.ToList ();
	}

	//이벤트 정보를 반환 
	public List<ShopEvent> getEvent(System.UInt32 eventId){
		var query = from shopEvent in jrw.se
				where shopEvent.id == eventId
			select shopEvent;

		return query.ToList ();
	}
}
