package com.kodgemisi.webapps.vuforia;

import java.io.File;
import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.Date;

import org.apache.commons.codec.binary.Base64;
import org.apache.commons.io.FileUtils;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.impl.cookie.DateUtils;
import org.apache.http.message.BasicHeader;
import org.apache.http.util.EntityUtils;
import org.json.JSONException;
import org.json.JSONObject;

import com.kodgemisi.webapps.vuforia.SignatureBuilder;

/*
 * 17.08.03 정다은 추가 
 * vuforia cloud recognization java example 참조
 * Reference: https://developer.vuforia.com/resources/dev-guide/adding-target-cloud-database-api
 */

public class PostNewTarget implements TargetStatusListener {//새로운 타겟을 올린다.

	//Server Keys, 현재 우리 프로젝트에만 해당 
	private String accessKey = "a9e8e2bf5ed866ffa198a12743bc5dff0d60f1b8";
	private String secretKey = "b2665507a1e9543e192fdc9c29703e3ad9e67579";
	
	private String url = "https://vws.vuforia.com";
	private String targetName = "[ target name ]";//올릴 타겟 이름
	private String imageLocation = "[ file system path ]";//올려야할 이미지 경로

	private TargetStatusPoller targetStatusPoller;
	
	private final float pollingIntervalMinutes = 60;//poll at 1-hour interval
	
	//Constructor
	public PostNewTarget(String targetName,String imageLocation){
		this.targetName=targetName;
		this.imageLocation=imageLocation;
	}
	
	private String postTarget() throws URISyntaxException, ClientProtocolException, IOException, JSONException {
		HttpPost postRequest = new HttpPost();
		HttpClient client = new DefaultHttpClient();
		postRequest.setURI(new URI(url + "/targets"));
		JSONObject requestBody = new JSONObject();
		
		setRequestBody(requestBody);
		postRequest.setEntity(new StringEntity(requestBody.toString()));
		setHeaders(postRequest); // Must be done after setting the body
	//	System.out.println("requestBody      "+ requestBody.toString());
		
		HttpResponse response = client.execute(postRequest);
		String responseBody = EntityUtils.toString(response.getEntity());
		System.out.println("ResponseBody "+responseBody);
		
		JSONObject jobj = new JSONObject(responseBody);
		
		String uniqueTargetId = jobj.has("target_id") ? jobj.getString("target_id") : "";
		System.out.println("\nCreated target with id: " + uniqueTargetId);
		
		return uniqueTargetId;
	}
	
	private void setRequestBody(JSONObject requestBody) throws IOException, JSONException {
		File imageFile = new File(imageLocation);
		if(!imageFile.exists()) {
			System.out.println("File location does not exist!");
			System.exit(1);
		}
		byte[] image = FileUtils.readFileToByteArray(imageFile);
		requestBody.put("name", targetName); // Mandatory
		requestBody.put("width", 320.0); // Mandatory
		requestBody.put("image", Base64.encodeBase64String(image)); // Mandatory
		requestBody.put("active_flag", 1); // Optional
		requestBody.put("application_metadata", Base64.encodeBase64String("Vuforia test metadata".getBytes())); // Optional
	}
	
	private void setHeaders(HttpUriRequest request) {
		SignatureBuilder sb = new SignatureBuilder();
		request.setHeader(new BasicHeader("Date", DateUtils.formatDate(new Date()).replaceFirst("[+]00:00$", "")));
		request.setHeader(new BasicHeader("Content-Type", "application/json"));
		request.setHeader("Authorization", "VWS " + accessKey + ":" + sb.tmsSignature(request, secretKey));
	}
	
	/**
	 * Posts a new target to the Cloud database; 
	 * then starts a periodic polling until 'status' of created target is reported as 'success'.
	 *///클라우드 데이터베이스에 새로운 타겟을 올린다.얘 불러야해용!!
	public String postTargetThenPollStatus() {
		String createdTargetId = "";
		try {
			createdTargetId = postTarget();//타겟을 올리고 id를 받음.
		} catch (URISyntaxException | IOException | JSONException e) {//에러가 생긴 경우
			e.printStackTrace();
			return null;
		}
        
		//id를 받았으면 'status'가 'success'가 될때까지 target을 poll 한다.
		// The TargetState will be passed to the OnTargetStatusUpdate callback 
		if (createdTargetId != null && !createdTargetId.isEmpty()) {
			targetStatusPoller = new TargetStatusPoller(pollingIntervalMinutes, createdTargetId, accessKey, secretKey, this );
			targetStatusPoller.startPolling();
		}
		return createdTargetId;
	}
	
	// Called with each update of the target status received by the TargetStatusPoller
	@Override
	public void OnTargetStatusUpdate(TargetState target_state) {
		if (target_state.hasState) {
			
			String status = target_state.getStatus();//Status를 받는다.
			
			System.out.println("Target status is: " + (status != null ? status : "unknown"));
			
			if (target_state.getActiveFlag() == true && "success".equalsIgnoreCase(status)) {//타겟의 상태가 활성화되있고 성공했다면 polling을 멈춤
				
				targetStatusPoller.stopPolling();
				
				System.out.println("Target is now in 'success' status");
			}
		}
	}
}
