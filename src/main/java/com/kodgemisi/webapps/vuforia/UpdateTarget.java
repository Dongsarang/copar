package com.kodgemisi.webapps.vuforia;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.Date;

import org.apache.commons.codec.binary.Base64;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
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
 */

public class UpdateTarget {

	//Server Keys, 현재 우리 프로젝트에만 해당 
	private String accessKey = "a9e8e2bf5ed866ffa198a12743bc5dff0d60f1b8";
	private String secretKey = "b2665507a1e9543e192fdc9c29703e3ad9e67579";
		
	private String targetId = "[ target id ]";
	private String url = "https://vws.vuforia.com";

	//Constructor
	public UpdateTarget(String id){
		this.targetId=id;
	}
	
	//타겟 id에 맞는 거의 타겟을 업데이트, 얘 불러야 해요 
	private void updateTarget() throws URISyntaxException, ClientProtocolException, IOException, JSONException {
		HttpPut putRequest = new HttpPut();
		HttpClient client = new DefaultHttpClient();
		putRequest.setURI(new URI(url + "/targets/" + targetId));
		JSONObject requestBody = new JSONObject();
		
		setRequestBody(requestBody);
		putRequest.setEntity(new StringEntity(requestBody.toString()));
		setHeaders(putRequest); // Must be done after setting the body
		
		HttpResponse response = client.execute(putRequest);
		System.out.println(EntityUtils.toString(response.getEntity()));
	}
	
	private void setRequestBody(JSONObject requestBody) throws IOException, JSONException {
		//requestBody.put("active_flag", true); // Optional
		requestBody.put("application_metadata", Base64.encodeBase64String("Vuforia test metadata".getBytes())); // Optional
	}
	
	private void setHeaders(HttpUriRequest request) {
		SignatureBuilder sb = new SignatureBuilder();
		request.setHeader(new BasicHeader("Date", DateUtils.formatDate(new Date()).replaceFirst("[+]00:00$", "")));
		request.setHeader(new BasicHeader("Content-Type", "application/json"));
		request.setHeader("Authorization", "VWS " + accessKey + ":" + sb.tmsSignature(request, secretKey));
	}
}
