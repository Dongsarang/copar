package com.kodgemisi.webapps.inventory.domain;

import javax.persistence.*;
/**
 * 2017.09.05 정다은 생성 
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */

//문자열 복
class StringClone {
	  private String dummy;

	  public StringClone(StringClone another) {
	    this.dummy = another.dummy; // 문자열 복사 
	  }
}


@Entity//This tells Hibernate to make a table out of this class
@Table(name="shop") 
public class Shop {//가게 정보 

	//variable
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false, updatable = false)
    private int id;
	
    @Column(name = "address")
    private String address;
    @Column(name = "distribute")
    private String distribute;
    @Column(name = "end_time")
    private String endTime;
    @Column(name = "expl")
    private String explain;
    @Column(name = "holiday")
    private String holiday;
    @Column(name = "image_path")
    private String imagePath;
    @Column(name = "is_coupon")
    private int isCoupon;
    @Column(name = "is_event")
    private int isEvent;
    @Column(name = "is_sale")
    private int isSale;
    @Column(name = "longi")
    private double longitude;
    @Column(name = "lati")
    private double latitude;
    @Column(name = "name")
    private String name;
    @Column(name = "phone")
    private String phone;
    @Column(name = "start_time")
    private String startTime;
    private String targetId;
    @Column(name = "user_id")
    private int userId;
    
    //Constructor
    public Shop(){
    }
    
    public Shop( int userId, String name, String address, double longitude, double latitude, String category, String imagePath,
    		String phone, String startTime, String endTime, String holiday, String explain, int isCoupon, int isSale, int isEvent, String targetId ){

        this.userId=userId;
        
        this.name=name;
        this.address= address;
        this.longitude= longitude;
        this.latitude= latitude;

        this.distribute= category;
        this.imagePath= imagePath;
        this.phone= phone;
     
        this.startTime= startTime;
        this.endTime= endTime;
        this.holiday= holiday;
        this.explain= explain;
        
        this.isCoupon= isCoupon;
        this.isSale= isSale;
        this.isEvent= isEvent;
        
        this.targetId=targetId;
    }
    
    //getter,setter
    public long getId(){
    	return id;
    }
    
    public int getUser(){
    	return userId;
    }
    public void setUser(int user){
    	this.userId=user;
    }
    
    public String getName(){
    	return name;
    }
    public void setName(String name){
    	this.name=name;
    }
    
    public String getAddress(){
    	return address;
    }
    public void setAddress(String address){
    	this.address=address;
    }
    
    public double getLongitude(){
    	return longitude;
    }
    public void setLongitude(double longitude){
    	this.longitude=longitude;
    }
    
    public double getLatitude(){
    	return latitude;
    }
    public void setLatitude(double latitude){
    	this.latitude=latitude;
    }
    
    public String getCategory(){
    	return distribute;
    }
    public void setCategory(String category){
    	this.distribute=category;
    }
    
    public String getImagePath(){
    	return imagePath;
    }
    public void setImagePath(String imagePath){
    	this.imagePath=imagePath;
    }
    
    public String getPhone(){
    	return phone;
    }
    public void setPhone(String phone){
    	this.phone=phone;
    }
    
    public String getStartTime(){
    	return startTime;
    }
    public void setStartTime(String startTime){
    	this.startTime=startTime;
    }
    
    public String getEndTime(){
    	return endTime;
    }
    public void setEndTime(String endTime){
    	this.endTime=endTime;
    }
    
    public String getHoliday(){
    	return holiday;
    }
    public void setHoliday(String holiday){
    	this.holiday=holiday;
    }
    
    public String getExplain(){
    	return explain;
    }
    public void setExplain(String explain){
    	this.explain=explain;
    }
    
    public int getIsEvent(){
    	return isEvent;
    }
    public void setIsEvent(int isEvent){
    	this.isEvent=isEvent;
    }
    
    public int getIsCoupon(){
    	return isCoupon;
    }
    public void setIsCoupon(int isCoupon){
    	this.isCoupon=isCoupon;
    }
    
    public int getIsSale(){
    	return isSale;
    }
    public void setIsSale(int isSale){
    	this.isSale=isSale;
    }
    
    public String getTargetId(){
    	return targetId;
    }
    public void setTargetId(String targetId){
    	this.targetId=targetId;
    }
}
