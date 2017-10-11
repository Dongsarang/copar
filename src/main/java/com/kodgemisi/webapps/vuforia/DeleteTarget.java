package com.kodgemisi.webapps.vuforia;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.Date;

import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpDelete;
import org.apache.http.client.methods.HttpPut;
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
 * 타겟 삭제시 주의사항
 * Note: a target that has status of ìProcessingî cannot be deleted.
 * Note: a target must be inactive in order to be deleted.
 * To deactivate a target, set its active_flag to false using the PUT method as demonstrated below.
 * You can also confirm the activation status of the target in the Target Manager at http://developer.vuforia.com.
 */

public class DeleteTarget implements TargetStatusListener {//현재 올려진 타겟을 삭제, 위의 주의사항 읽

	//Server Keys, 현재 우리 프로젝트에만 해당 
	private String accessKey = "a9e8e2bf5ed866ffa198a12743bc5dff0d60f1b8";
	private String secretKey = "b2665507a1e9543e192fdc9c29703e3ad9e67579";
	
	private String targetId = "[ target id ]";
	private String url = "https://vws.vuforia.com";
	
	private TargetStatusPoller targetStatusPoller;
	
	private final float pollingIntervalMinutes = 60;//poll at 1-hour interval

	//Constructor
	public DeleteTarget(String targetId){
		this.targetId=targetId;
	}
	
	private void deleteTarget() throws URISyntaxException, ClientProtocolException, IOException {
		HttpDelete deleteRequest = new HttpDelete();
		HttpClient client = new DefaultHttpClient();
		deleteRequest.setURI(new URI(url + "/targets/" + targetId));
		setHeaders(deleteRequest);
		
		HttpResponse response = client.execute(deleteRequest);
		System.out.println("Delete Response " + EntityUtils.toString(response.getEntity()));
	}
	
	private void setHeaders(HttpUriRequest request) {
		SignatureBuilder sb = new SignatureBuilder();
		request.setHeader(new BasicHeader("Date", DateUtils.formatDate(new Date()).replaceFirst("[+]00:00$", "")));
		request.setHeader("Authorization", "VWS " + accessKey + ":" + sb.tmsSignature(request, secretKey));
	}
	
	//받은 인자값으로 타겟의 active_flag를 바꾼다.sets the targets active_flag to the Boolean value of the argument
	private void updateTargetActivation(Boolean b) throws URISyntaxException, ClientProtocolException, IOException, JSONException {
		HttpPut putRequest = new HttpPut();
		HttpClient client = new DefaultHttpClient();
		putRequest.setURI(new URI(url + "/targets/" + targetId));
		
		JSONObject requestBody = new JSONObject();
		requestBody.put("active_flag", b );// add a JSON field for the active_flag
		
		putRequest.setEntity(new StringEntity(requestBody.toString()));
		// Set the Headers for this Put request
		// Must be done after setting the body
		SignatureBuilder sb = new SignatureBuilder();
		putRequest.setHeader(new BasicHeader("Date", DateUtils.formatDate(new Date()).replaceFirst("[+]00:00$", "")));
		putRequest.setHeader(new BasicHeader("Content-Type", "application/json"));
		putRequest.setHeader("Authorization", "VWS " + accessKey + ":" + sb.tmsSignature(putRequest, secretKey));
		
		HttpResponse response = client.execute(putRequest);
		System.out.println("Update Response "+EntityUtils.toString(response.getEntity()));
	}
	
	// 타겟의 active flag를 false로 전환하고 타겟을 삭제하는 프로세스 진행, 얘 불러야해용!!!
	public void deactivateThenDeleteTarget() {		
		try {
			updateTargetActivation( false );
		} catch ( URISyntaxException | IOException | JSONException e) {
			e.printStackTrace();
			return;
		}
	
		// Poll the target status until the active_flag is confirmed to be set to false
		// The TargetState will be passed to the OnTargetStatusUpdate callback 
		targetStatusPoller = new TargetStatusPoller(pollingIntervalMinutes, targetId, accessKey, secretKey, this );
		targetStatusPoller.startPolling();
	}
	
	// Called with each update of the target status received by the TargetStatusPoller
	@Override
	public void OnTargetStatusUpdate(TargetState target_state) {
		if (target_state.hasState) {
		
			if (target_state.getActiveFlag() == false) {
				
				targetStatusPoller.stopPolling();
				
				try {
					System.out.println(".. deleting target ..");
					
					deleteTarget();
					
				} catch ( URISyntaxException | IOException e) {
					e.printStackTrace();
				}
			}
		}
	}
}
