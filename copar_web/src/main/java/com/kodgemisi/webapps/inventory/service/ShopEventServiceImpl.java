package com.kodgemisi.webapps.inventory.service;

import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.kodgemisi.webapps.inventory.domain.ShopEvent;
import com.kodgemisi.webapps.inventory.repository.ShopEventRepository;

/**
 * 2017.09.10 정다은 생성
 *reference: https://spring.io/guides/gs/uploading-files/
 *reference: picture 추가하는 spring project
 */

@Service
public class ShopEventServiceImpl implements ShopEventService {

	private final ShopEventRepository shopEventRepository;
	private final UserService userService;
	
	//Constructor
	@Autowired
	public ShopEventServiceImpl(ShopEventRepository shopEventRepository, UserService userService){
		this.shopEventRepository=shopEventRepository;
		this.userService=userService;
	}
	
	@Override
	public List<ShopEvent> findAll() {
		List<ShopEvent> shopEvents=new ArrayList<>();
		for(ShopEvent shopEvent: shopEventRepository.findAll()){
			shopEvents.add(shopEvent);
		}
		return shopEvents;
	}

	@Override
	public void save(ShopEvent shopEvent) {
		shopEventRepository.save(shopEvent);

	}

	@Override
	public void delete(long id) {
		shopEventRepository.delete(id);
	}

}
