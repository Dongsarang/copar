package com.kodgemisi.webapps.inventory;

/**
 * 2017.07.26 정다은 수정
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */

import com.kodgemisi.webapps.inventory.domain.User;
import com.kodgemisi.webapps.inventory.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.security.config.annotation.web.servlet.configuration.EnableWebMvcSecurity;
import org.springframework.web.servlet.ViewResolver;
import org.thymeleaf.TemplateEngine;
import org.thymeleaf.extras.java8time.dialect.Java8TimeDialect;
//import org.thymeleaf.extras.java8time.dialect.Java8TimeDialect;
import org.thymeleaf.spring4.SpringTemplateEngine;
import org.thymeleaf.spring4.view.ThymeleafViewResolver;
import org.thymeleaf.templateresolver.ClassLoaderTemplateResolver;
import org.thymeleaf.templateresolver.ITemplateResolver;

import java.util.HashSet;
import java.util.Set;

@SpringBootApplication
public class Application implements CommandLineRunner {
    @Autowired
    UserRepository userRepository;

    public static void main(String[] args) {
        SpringApplication.run(Application.class, args);
    }

//    @Bean
//    public Java8TimeDialect java8TimeDialect() {
//        return new Java8TimeDialect();
//    }
//    
    private TemplateEngine templateEngine(ITemplateResolver templateResolver) {
        SpringTemplateEngine engine = new SpringTemplateEngine();
//        engine.addDialect(new Java8TimeDialect());
        engine.setTemplateResolver(templateResolver);
        return engine;
    }
    
    @Bean
    public ViewResolver viewResolver() {
      ClassLoaderTemplateResolver templateResolver = new ClassLoaderTemplateResolver();
      templateResolver.setTemplateMode("XHTML");
      templateResolver.setPrefix("templates/");
      templateResolver.setSuffix(".html");
      
      SpringTemplateEngine engine = new SpringTemplateEngine();

      engine.setTemplateResolver(templateResolver);

      ThymeleafViewResolver viewResolver = new ThymeleafViewResolver();
      viewResolver.setTemplateEngine(engine);
      return viewResolver;
    }
    
    public void run(String... strings) throws Exception {
    	//item을 추가하는 내용이라서 mysql에 이미 추가된 상태였으므로 주석 처리 
//      Item item1 = new Item("123S", "Bilgisayar");
//      Item item2 = new Item("358G", "Bilgisayar");
//      Item item3 =  new Item("158A", "Bilgisayar");
//      Item item4 = new Item("935C", "Telefon");
//
//      Set set1 = new HashSet<Item>();
//      set1.add(item1);
//      set1.add(item3);
//      set1.add(item4);
//
//      Set set2 = new HashSet<Item>();
//      set2.add(item2);
//
//      User user1 = new User("sedo", "123456");
//      user1.setName("Sedat");
//      user1.setLastName("Gökcen");
//      user1.setItems(set1);
//
//      User user2 = new User("hool", "hoo123");
//      user2.setName("Hool");
//      user2.setLastName("Gökcen");
//      user2.setItems(set2);
//
//      item1.setUser(user1);
//      item3.setUser(user1);
//      item4.setUser(user1);
//
//      item2.setUser(user2);
//
//      userRepository.save(user1);
//      userRepository.save(user2);
//
//      itemRepository.save(item1);
//      itemRepository.save(item2);
//      itemRepository.save(item3);
//      itemRepository.save(item4);
    }
}
