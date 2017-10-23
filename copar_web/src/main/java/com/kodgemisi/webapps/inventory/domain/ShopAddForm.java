package com.kodgemisi.webapps.inventory.domain;

import javax.persistence.*;

import org.springframework.format.annotation.DateTimeFormat;

import java.sql.Time;
import java.time.LocalTime;
/**
 * 2017.09.17 정다은 생성 
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */


public class ShopAddForm {//가게 정보를 추가하기 위한 클래스 
    //mysql entity와는 다르게 정보가 입력되므로 추가함
	
	//variable
    private String name;
    private String address;

    private String category;
    private String imagePath;
 
    private String startTime;
    private String endTime;
    private String holiday;
    private String explain;
    
    private String username;
    
    //Constructor
    public ShopAddForm(){
    }
    
    public ShopAddForm( String username, String name, String address, String category, String imagePath, String startTime, String endTime,
    		String holiday, String explain){
        
        this.name=name;
        this.address= address;

        this.category= category;
        this.imagePath= imagePath;
        
        this.startTime= startTime;
        this.endTime= endTime;
        this.holiday= holiday;
        this.explain= explain;
        
        this.username=username;
    }
    
    public String getUsername() {
        return username;
    }
    public void setUsername(String username) {
        this.username = username;
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
 
    public String getCategory(){
    	return category;
    }
    public void setCategory(String category){
    	this.category=category;
    }
    
    public String getImagePath(){
    	return imagePath;
    }
    public void setImagePath(String imagePath){
    	this.imagePath=imagePath;
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
      
}
