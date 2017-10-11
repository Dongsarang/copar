package com.kodgemisi.webapps.inventory.controller;

import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.math.BigInteger;
import java.security.SecureRandom;
import java.util.Date;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;

import com.kodgemisi.webapps.inventory.domain.ShopEvent;
import com.kodgemisi.webapps.inventory.domain.User;
import com.kodgemisi.webapps.inventory.service.ShopEventService;
import com.kodgemisi.webapps.inventory.service.UserService;
import com.kodgemisi.webapps.vuforia.TargetManager;

/**
 * 2017.09.13 정다은 수정
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */

@Controller
public class ShopEventController {//shopEvent를 부르는 페이지 
	private final ShopEventService shopEventService;
	private final UserService userService;

	@Autowired
	public ShopEventController(ShopEventService shopEventService, UserService userService){
		this.shopEventService=shopEventService;
		this.userService=userService;
	}

	//shopEvent를 등록하는 페이지를 부른다  
	@RequestMapping(value="/events",method=RequestMethod.GET)//model
	public String eventPage( )
	{
		return "events";
	}

	@RequestMapping(value="/events",method=RequestMethod.POST )//model
	public @ResponseBody String eventPagePost(@ModelAttribute("eventForm") ShopEvent shopEvent,BindingResult bindingResult, 
			@RequestParam("file") MultipartFile file,HttpServletRequest request)
	{
		System.out.println("------------- file start -------------");
		System.out.println("name : "+shopEvent.getDistrubte());
		System.out.println("date : "+shopEvent.getExplain());
		System.out.println("user : "+shopEvent.getimagePath());
		System.out.println("path : "+shopEvent.getEndTime());
		System.out.println("path : "+shopEvent.getStartTime());
		System.out.println("-------------- file end --------------\n");

		File serverFile = null;
		if (!file.isEmpty()) {
			try {

				byte[] bytes = file.getBytes();

				// Creating the directory to store file
				String rootPath = "/usr/local/Cellar/mysql/imageSave";
				File dir = new File(rootPath);
				if (!dir.exists())
					dir.mkdirs();

				// Create the file on server
				serverFile = new File(dir.getAbsolutePath() + File.separator +"event.jpg");
				String str=serverFile.getAbsolutePath();
				BufferedOutputStream stream = new BufferedOutputStream(new FileOutputStream(serverFile));
				stream.write(bytes);
				stream.close();

				File dosya = new File(str);
				SecureRandom random = new SecureRandom();
				String uniq = new BigInteger(130,random).toString();
				String absolutePath = dosya.getAbsolutePath();
				String filePath = absolutePath.substring(0,absolutePath.lastIndexOf(File.separator));
				File newFile = new File(absolutePath+uniq+dosya.getName());	
				//새롭게 이름을 붙임, dosya는 삭제 
				dosya.renameTo(newFile);
				dosya.delete();

				shopEvent.setImagePath(newFile.getAbsolutePath());//저장 경로 저장 
				User u=(User)SecurityContextHolder.getContext().getAuthentication().getPrincipal();
				shopEvent.setUser(u);
//				shopEvent.setShop(u.getShops().);
				//	int shop_id//shop Id를 구한다,  

				System.out.println("------------- file start -------------");
				System.out.println("name : "+shopEvent.getDistrubte());
				System.out.println("date : "+shopEvent.getExplain());
				System.out.println("user : "+shopEvent.getimagePath());
				System.out.println("user : "+shopEvent.getUser());
				System.out.println("path : "+shopEvent.getEndTime());
				System.out.println("path : "+shopEvent.getStartTime());
				System.out.println("-------------- file end --------------\n");
				shopEventService.save(shopEvent);

				System.out.println("event saved");
				return "fileUpload";
			} catch (Exception e) {
				return "You failed to upload "  + " => " + e.getMessage();
			}
		} else {
			System.out.println("You failed to upload " +  " because the file was empty."); 
		}

		return "redirect:/";
	}

	//shopEvent 목록을 보여주는 페이지를 부른다 
}
