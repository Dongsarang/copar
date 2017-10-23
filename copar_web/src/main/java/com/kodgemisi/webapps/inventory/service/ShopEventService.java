package com.kodgemisi.webapps.inventory.service;

import java.util.List;

import com.kodgemisi.webapps.inventory.domain.ShopEvent;

/**
 * 2017.09.10 정다은 생성
 *reference: https://spring.io/guides/gs/uploading-files/
 *reference: picture 추가하는 spring project
 */

public interface ShopEventService {
	public List<ShopEvent> findAll();
	public void save(ShopEvent shopEvent);
	public void delete(long id);
}
