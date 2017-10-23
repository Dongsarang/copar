package com.kodgemisi.webapps.inventory.service;

import com.kodgemisi.webapps.inventory.domain.User;

import java.util.List;
import java.util.Map;

/**
 * 2017.07.26 정다은 수정
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */

public interface UserService {
    User getUserById(long id);

    User getUserByUsername(String username);

    User addUser(User user);

    Iterable<User> getUsers();

//    Map<String, List<Item>> numberOfItemsByType(long userId);

    List<String> getUsernames();
    
}
