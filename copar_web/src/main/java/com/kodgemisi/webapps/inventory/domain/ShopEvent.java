package com.kodgemisi.webapps.inventory.domain;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;

import org.springframework.format.annotation.DateTimeFormat;

import java.text.SimpleDateFormat;
/**
 * 2017.09.10 정다은 생성 
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */

@Entity
public class ShopEvent {
	//variable
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name = "id", nullable = false, updatable = false)
	private long id;

	@Column(name = "distribute", nullable = false)
	private String distribute;
	@Column(name = "image_path")
	private String imagePath;
	@Column(name = "expl")
	private String explain;

	@ManyToOne
	@JoinColumn(name = "shop_id")
	private Shop shopId;
	//    @Column(name = "start_time", nullable = false)
	//    @DateTimeFormat(pattern = "yyyy-MM-dd")
	//    private SimpleDateFormat startTime;
	//    @Column(name = "end_time", nullable = false)
	//    @DateTimeFormat(pattern = "yyyy-MM-dd")
	//    private SimpleDateFormat endTime;
	@Column(name = "start_time", nullable = false)
	private String startTime;
	@Column(name = "end_time", nullable = false)
	private String endTime;

	@ManyToOne
	@JoinColumn(name = "user_id")
	private User userId;
	
	//Constructor
	public ShopEvent(){

	}

	public ShopEvent( long id, String category, String imagePath, String explain, Shop shopId, SimpleDateFormat startTime, SimpleDateFormat endTime ){
		this.id= id;
		this.distribute= category;
		this.imagePath= imagePath;
		this.explain= explain;
		//	    this.shopId= shopId;
		//	    this.startTime= startTime;
		//	    this.endTime= endTime;
	}

	public long getId(){
		return id;
	}

	public String getDistrubte(){
		return distribute;
	}
	public void setDistribute(String distribute){
		this.distribute=distribute;
	}

	public String getimagePath(){
		return imagePath;
	}
	public void setImagePath(String imagePath){
		this.imagePath=imagePath;
	}

	public String getExplain(){
		return explain;
	}
	public void setExplain(String explain){
		this.explain=explain;
	}

	public Shop getShop(){
		return shopId;
	}
	public void setShop(Shop shop){
		this.shopId=shop;
	}

	//	public SimpleDateFormat getStartTime(){
	//		return startTime;
	//	}
	//	public void setStartTime(SimpleDateFormat startTime){
	//		this.startTime=startTime;
	//	}
	//	
	//	public SimpleDateFormat getEndTime(){
	//		return startTime;
	//	}
	//	public void setEndTime(SimpleDateFormat endTime){
	//		this.endTime=endTime;
	//	}

	public String getStartTime(){
		return startTime;
	}
	public void setStartTime(String startTime){
		this.startTime=startTime;
	}

	public String getEndTime(){
		return startTime;
	}
	public void setEndTime(String endTime){
		this.endTime=endTime;
	}
	
	public User getUser(){
		return userId;
	}
	public void setUser(User user){
		this.userId=user;
	}
}
