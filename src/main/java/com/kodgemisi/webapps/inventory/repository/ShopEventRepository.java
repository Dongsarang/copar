package com.kodgemisi.webapps.inventory.repository;

import org.springframework.data.repository.CrudRepository;
import com.kodgemisi.webapps.inventory.domain.ShopEvent;
/**
 * 2017.09.10 정다은 생성 
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */

public interface ShopEventRepository extends CrudRepository<ShopEvent,Long> {

}
