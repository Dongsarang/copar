package com.kodgemisi.webapps.inventory.controller;

import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.math.BigInteger;
import java.security.SecureRandom;
import java.time.LocalTime;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.servlet.ModelAndView;

import com.kodgemisi.webapps.common.CommandMap;
import com.kodgemisi.webapps.inventory.domain.Shop;
import com.kodgemisi.webapps.inventory.domain.ShopAddForm;
import com.kodgemisi.webapps.inventory.domain.User;
import com.kodgemisi.webapps.inventory.service.ShopService;
import com.kodgemisi.webapps.inventory.service.UserService;
import com.kodgemisi.webapps.vuforia.TargetManager;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.context.SecurityContextHolder;

/**
 * 2017.09.13 정다은 생성 
 * 2017.09.16-20 정다은 수정, 
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */

@Controller//shop 클래스 등록 및 관리를 위한 controller
public class ShopController {//shop 등록 페이지 관리 
	private final ShopService shopService;
	private final UserService userService;
	
	@Autowired
	public ShopController(ShopService shopService, UserService userService){
		this.shopService=shopService;
		this.userService=userService;
	}	
	
	//가게를 등록하는 페이지, 
	@RequestMapping(value="/shops", method = RequestMethod.GET)
	public ModelAndView getShopRegisterPage(HttpServletRequest request){
		return new ModelAndView("shop","shopForm",new ShopAddForm());//template, model, object
	}
	
	@RequestMapping(value="/shops", method = RequestMethod.POST)
	public @ResponseBody String uploadFileHandler(@ModelAttribute("shopForm") ShopAddForm shopForm,
			BindingResult bindingResult, @RequestParam("file") MultipartFile file,HttpServletRequest request) {
		
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
				serverFile = new File(dir.getAbsolutePath() + File.separator + "target.jpg");
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
				
				
				shopForm.setImagePath(newFile.getAbsolutePath());
				//새롭게 이름을 붙임, dosya는 삭제 
				dosya.renameTo(newFile);
				dosya.delete();

				//Vuforia target image upload
				TargetManager t=new TargetManager();
				String targetId = t.TargetUpload(uniq+"target", newFile.getAbsolutePath());
				
				User u=(User)SecurityContextHolder.getContext().getAuthentication().getPrincipal();
				shopService.save(u, targetId, shopForm);
				
				
				System.out.println("------------- file start -------------");
				System.out.println("name : "+shopForm.getName());
				System.out.println("user : "+shopForm.getUsername());
				System.out.println("adr  : "+shopForm.getAddress());
				System.out.println("time : "+shopForm.getStartTime().toString());
				System.out.println("time : "+shopForm.getEndTime().toString());
				System.out.println("cate : "+shopForm.getCategory());
				System.out.println("expl : "+shopForm.getExplain());
				System.out.println("holi  : "+shopForm.getHoliday());
				System.out.println("path : "+shopForm.getImagePath());
				System.out.println("-------------- file end --------------\n");
				
				System.out.println("shop saved");
				return "fileUpload";
			} catch (Exception e) {
				return "You failed to upload " + "target" + " => " + e.getMessage();
			}
		} else {
			System.out.println("You failed to upload " + "target" + " because the file was empty."); 
		}

		return "redirect:/";
	}

}
