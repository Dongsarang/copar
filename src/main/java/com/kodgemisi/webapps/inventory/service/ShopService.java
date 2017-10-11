package com.kodgemisi.webapps.inventory.service;

import java.util.ArrayList;
import java.util.List;

import com.kodgemisi.webapps.inventory.domain.Shop;
import com.kodgemisi.webapps.inventory.domain.ShopAddForm;
import com.kodgemisi.webapps.inventory.domain.User;

/**
 * 2017.09.09 정다은 생성
 * 2017.09.16 정다은 수정 
 *reference: https://spring.io/guides/gs/uploading-files/
 *reference: picture 추가하는 spring project
 */

public interface ShopService {
	public Iterable<Shop> getShops();
	public void save(User user, String targetId, ShopAddForm shopAddForm);
	public void delete(long id);
}
