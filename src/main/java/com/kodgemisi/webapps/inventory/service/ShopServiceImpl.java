package com.kodgemisi.webapps.inventory.service;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.google.code.geocoder.*;
import com.google.code.geocoder.model.*;
import com.kodgemisi.webapps.inventory.domain.Shop;
import com.kodgemisi.webapps.inventory.domain.ShopAddForm;
import com.kodgemisi.webapps.inventory.domain.User;
import com.kodgemisi.webapps.inventory.repository.ShopRepository;
/**
 * 2017.09.09 정다은 생성
 * 2017.09.16-17 정다은 수정 
 *reference: https://spring.io/guides/gs/uploading-files/
 *reference: picture 추가하는 spring project
 */

@Service
public class ShopServiceImpl implements ShopService {

	private final ShopRepository shopRepository;
	private final UserService userService;

	//Constructor
	@Autowired
	public ShopServiceImpl(ShopRepository shopRepository, UserService userService){
		this.shopRepository=shopRepository;
		this.userService=userService;
	}

	@Override
	public Iterable<Shop> getShops(){
		return shopRepository.findAll();
	}

	@Override
	public void save(User user, String targetId, ShopAddForm shopAddForm) {
		System.out.println("DDDDDDDD");
		Float[] latLong=geoCoding(shopAddForm.getAddress());
		
		Shop shop= new Shop( user.getId(), shopAddForm.getName(), shopAddForm.getAddress(), latLong[1].doubleValue(),
				latLong[0].doubleValue(), shopAddForm.getCategory(),shopAddForm.getImagePath(), "00-000-0000", 
				shopAddForm.getStartTime().toString(), shopAddForm.getEndTime().toString(), 
				shopAddForm.getHoliday(), shopAddForm.getExplain(), 0, 0, 0,targetId);

		System.out.println(shop.getId()+" "+shop.getAddress()+" "+shop.getCategory()+" "+shop.getEndTime()+" "+
				shop.getHoliday()+" "+shop.getImagePath()+" "+shop.getIsCoupon()+" "+shop.getIsEvent()+" "+shop.getIsSale()
				+" "+shop.getLatitude()+" "+shop.getLongitude()+" "+shop.getName()+" "+shop.getPhone()+" "+shop.getStartTime()+" "+shop.getTargetId()
				+" "+shop.getUser());
		
		shopRepository.save(shop);

	}

	@Override
	public void delete(long id) {
		shopRepository.delete(id);
	}

	//지오코딩을 합니다, 주소->좌표값 
	public Float[] geoCoding(String location) {
		if (location == null)  
			return null;

		Geocoder geocoder = new Geocoder();
		// setAddress : 변환하려는 주소 (경기도 성남시 분당구 등)
		// setLanguate : 인코딩 설정
		GeocoderRequest geocoderRequest = new GeocoderRequestBuilder().setAddress(location).setLanguage("ko").getGeocoderRequest();
		GeocodeResponse geocoderResponse;

		try {
			geocoderResponse = geocoder.geocode(geocoderRequest);
			if (geocoderResponse.getStatus() == GeocoderStatus.OK & !geocoderResponse.getResults().isEmpty()) {

				GeocoderResult geocoderResult=geocoderResponse.getResults().iterator().next();
				LatLng latitudeLongitude = geocoderResult.getGeometry().getLocation();

				Float[] coords = new Float[2];
				coords[0] = latitudeLongitude.getLat().floatValue();
				coords[1] = latitudeLongitude.getLng().floatValue();
				return coords;
			}
		} catch (IOException ex) {
			ex.printStackTrace();
		}
		return null;
	}


}
