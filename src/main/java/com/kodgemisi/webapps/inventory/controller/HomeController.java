package com.kodgemisi.webapps.inventory.controller;

import com.kodgemisi.webapps.inventory.domain.User;
import com.kodgemisi.webapps.inventory.service.UserService;
import com.sun.security.auth.UserPrincipal;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.config.annotation.web.servlet.configuration.EnableWebMvcSecurity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.web.bind.annotation.AuthenticationPrincipal;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by sedat on 26.06.2015.
 * 2017.07.26 정다은 수정
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 *reference: http://millky.com/@origoni/post/1144
 */

@Controller
public class HomeController {
    @RequestMapping("/")
    public ModelAndView getHomePage(@AuthenticationPrincipal User user) {
        return new ModelAndView("main", "user", user);
    }
    
    @RequestMapping("/about")
    public ModelAndView getAboutPage(@AuthenticationPrincipal User user) {
        return new ModelAndView("aboutus", "user", user);
    }
    
    @RequestMapping("/contact")
    public ModelAndView getContactPage(@AuthenticationPrincipal User user) {
        return new ModelAndView("contactus", "user", user);
    }
}
