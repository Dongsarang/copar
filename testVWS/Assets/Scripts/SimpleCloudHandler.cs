using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
//using System.Data;

public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler  {
	//  클라우드 인식 이벤트 핸들러
	private CloudRecoBehaviour mCloudRecoBehaviour;
	private bool mIsScanning = false;
	private string mTargetMetadata = "";
	// Use this for initialization
	void Start () {
//		MysqlManager mm = new MysqlManager ();
//		DataTable dt= mm.selsql ("select * from Picture");
//
//		ShowTable (dt);

		// register this event handler at the cloud reco behaviour
		mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();

		if (mCloudRecoBehaviour)
		{
			mCloudRecoBehaviour.RegisterEventHandler(this);
		}
	}
	public void OnInitialized() {//클라우드 인식 초기화 
		Debug.Log ("Cloud Reco initialized");
	}
	public void OnInitError(TargetFinder.InitState initError) {//초기화가 잘못된 경우
		Debug.Log ("Cloud Reco init error " + initError.ToString());
	}
	public void OnUpdateError(TargetFinder.UpdateState updateError) {//업데이트 에러
		Debug.Log ("Cloud Reco update error " + updateError.ToString());
	}

	public void OnStateChanged(bool scanning) {//현재 뷰포리아에서 클라우드를 잘 스캔 중인지 표시 
		mIsScanning = scanning;
		if (scanning)
		{
			// clear all known trackables, imageTracker 클래스에서 바뀜
			ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
			tracker.TargetFinder.ClearTrackables(false);
		}
	}

	// Here we handle a cloud target recognition event, 
	public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult) {
		// do something with the target metadata
		mTargetMetadata = targetSearchResult.MetaData;
		// stop the target finder (i.e. stop scanning the cloud)
		mCloudRecoBehaviour.CloudRecoEnabled = false;
	}
		
}
