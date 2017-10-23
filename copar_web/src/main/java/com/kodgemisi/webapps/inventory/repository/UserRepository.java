package com.kodgemisi.webapps.inventory.repository;

import com.kodgemisi.webapps.inventory.domain.User;
import org.springframework.data.repository.CrudRepository;

/**
 * 2017.07.26 정다은 수정
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */

public interface UserRepository extends CrudRepository<User, Long> {
    User findByUsername(String username);
}
