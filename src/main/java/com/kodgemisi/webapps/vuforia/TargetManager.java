package com.kodgemisi.webapps.vuforia;

/*
 * 17.08.03 정다은 추가 
 * 17.08.04 수정 
 * vuforia cloud recognization java example 참조
 * Reference: https://developer.vuforia.com/resources/dev-guide/adding-target-cloud-database-api
 */

//vuforia에 타겟을 올리고, 삭제하고, 업데이트하는 것을 관리하는 함수를 전부 모아놓은 클래스 
public class TargetManager {

	//variable
	private PostNewTarget postNewTarget;
	private DeleteTarget deleteTarget;
	private GetAllTargets getAllTargets;
	private GetTarget getTarget;
	private UpdateTarget updateTarget;
	
	//target이 될 이미지를 업로드한다.
	public String TargetUpload( String name, String imagePath){
		postNewTarget=new PostNewTarget(name,imagePath);
		String tempId = postNewTarget.postTargetThenPollStatus();
		return tempId;//targetId를 반환한다. DB에 저장되게 된다 
	}
	
	//target이 될 이미지를 삭제한다.
	public void targetDelete(){
		String id=null;//id를 찾는다,DB에서 targetId를 갖고온다.
		deleteTarget=new DeleteTarget(id);
	}
	//target을 새로운 이미지로 업데이트한다. 
	public void targetUpdate(){
		String id=null;//id를 찾는다,DB에서 targetId를 갖고온다.
		updateTarget=new UpdateTarget(id);
	}
	//target을 전부 불러온다.
	public void findTargetById(){
		String id=null;//id를 찾는다,DB에서 targetId를 갖고온다.
		getTarget=new GetTarget(id);
		getTarget.findTarget();
	}
	//target을 id에 해당하는 하나만 갖고 온다. 
	public void findAllTarget(){
		getAllTargets=new GetAllTargets();
		getAllTargets.findAllTarget();
		
	}
}
