package com.kodgemisi.webapps.inventory.domain;

import org.hibernate.validator.constraints.NotEmpty;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.web.bind.annotation.AuthenticationPrincipal;

import javax.persistence.*;
import javax.validation.constraints.Size;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Set;

/**
 * 2017.07.26 정다은 수정
 * 2017.09.05 정다은 수정, 
 * 2017.09.14 정다은 수정, 필요없는 변수 및 함수 정리
 *reference: https://spring.io/guides/gs/accessing-data-mysql/
 *reference: https://medium.com/kodgemisi/spring-boot-ile-örnek-web-uygulaması-914c94c9099f
 */

@Entity
public class User implements UserDetails {//유저에 대한 정보를 담는 클래스,mysql에서 table로 활용 
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name = "id", nullable = false, updatable = false)
	private int id;

	@NotEmpty
	@Size(min=3, max=20)
	@Column(name = "mail_address", nullable = false, unique = true)
	private String username;

	@NotEmpty
	@Size(min=6, max=20)
	@Column(name = "password", nullable = false)
	private String password;
	
//	@NotEmpty
//	@Column(name = "name", nullable = false)
//	private String name;
//
//	@NotEmpty
//	@Column(name = "lastName", nullable = false)
//	private String lastName;

//	@OneToMany(mappedBy = "user")
//	private Set<Shop> shop;
//	
//	@OneToMany(mappedBy = "user")
//	private Set<Item> items;
//	2017.09.05 추가

	public User() {

	}

	public User(String username, String password) {
		this.username = username;
		this.password = password;
	}

	public int getId() {
		return id;
	}

//	public String getMailAddress() {
//		return mailAddress;
//	}
	
	public String getUsername() {
		return username;
	}

	@Override
	public boolean isAccountNonExpired() {
		return true;
	}

	@Override
	public boolean isAccountNonLocked() {
		return true;
	}

	@Override
	public boolean isCredentialsNonExpired() {
		return true;
	}

	@Override
	public boolean isEnabled() {
		return true;
	}

	@Override
	public Collection<? extends GrantedAuthority> getAuthorities() {
		SimpleGrantedAuthority simpleGrantedAuthority = new SimpleGrantedAuthority("USER");
		List<SimpleGrantedAuthority> list = new ArrayList<SimpleGrantedAuthority>();
		list.add(simpleGrantedAuthority);
		return list;
	}

	public String getPassword() {
		return password;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public void setPassword(String password) {
		this.password = password;
	}

//	public String getName() {
//		return name;
//	}
//
//	public void setName(String name) {
//		this.name = name;
//	}
//
//	public String getLastName() {
//		return lastName;
//	}
//
//	public void setLastName(String lastName) {
//		this.lastName = lastName;
//	}

//	public Set<Item> getItems() {
//		return items;
//	}
//
//	public void setItems(Set<Item> items) {
//		this.items = items;
//	}
	
//	public Set<Shop> getShops() {
//		return shop;
//	}
//
//	public void setShop(Set<Shop> shops) {
//		this.shop = shops;
//	}
//	public Set<Picture> getPictures() {
//		return pictures;
//	}
//
//	public void setPictures(Set<Picture> pictures) {
//		this.pictures=pictures;
//	}

	@Override
	public String toString() {
		return username;
	}
}
