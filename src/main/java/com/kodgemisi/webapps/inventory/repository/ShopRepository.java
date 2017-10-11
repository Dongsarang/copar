package com.kodgemisi.webapps.inventory.repository;

import org.springframework.data.repository.CrudRepository;
import com.kodgemisi.webapps.inventory.domain.Shop;

/**
 * 2017.09.09 정다은 생성
 *reference: https://spring.io/guides/gs/uploading-files/
 *reference: picture 추가하는 spring project
 */

public interface ShopRepository extends CrudRepository<Shop, Long>{
	
}
