using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


/*
 * 2017.08.08 정다은 생성
 * 2017.08.09 수정, 08.14 수정,  08.22 수정
 */


public class MainUIManager : MonoBehaviour {//Scene main에서 사용되는 UI
	//variable
	[SerializeField]
	private GameObject[] scenes;//main,info 등등 
	private int now=(int)sceneNumber.cam;//현재 상태 
	private Animator anim;
	[SerializeField]
	private Text load;
	private bool loadScene = false;

	[SerializeField]//for main page, shop objects
	private ShopObject[] soManager;//shopObject array
	private InfoManager infoManager;

	[SerializeField]//for information page
	private GameObject[] infoKind;
	[SerializeField]
	private InfoPart[] infoP;

	[SerializeField]
	private RectTransform form;
	private CanvasScaler css;

	private CouponManager cm;

	private List<Shop> dt;

    //이진민 추가
    private int resolutionW = 800;
    private int resolutionH = 1280;
//    private GPSManager gps;
    public GameObject gpsManagerObject;
    private GPSManager gpsManager;

    public Text testText;

    void Awake(){
        gpsManagerObject = GameObject.FindGameObjectWithTag("GPSManager");
        gpsManager = gpsManagerObject.GetComponent<GPSManager>();
//        gps = GameObject.FindGameObjectWithTag("GPSManager").GetComponent<GPSManager>();

    }

    void Start(){
//        gps = GameObject.FindGameObjectWithTag("GPSManager").GetComponent<GPSManager>();
        Debug.Log("MainUIManager gps" + gpsManager.lat + " " + gpsManager.lon);

        for (int i = 0; i < scenes.Length; i++) {
			scenes [i].SetActive (false);
		}
		scenes [0].SetActive (true);//main
		cm=this.GetComponent<CouponManager>();

        css = this.GetComponent<CanvasScaler> ();
		resolutionChange (resolutionW, resolutionH);//해상도 초기화 
		anim=GetComponent<Animator>();

        infoManager = this.GetComponent<InfoManager> ();//shopObject 보여주기 
		shopObjectInitialize();

        if (dt == null){
            testText.GetComponent<Text>().text = "dt is null";
        } else {
            testText.GetComponent<Text>().text = "dt is not null" + "dt count: " + dt.Count + ", ";
            for (int i=0;i<dt.Count;i++){
                testText.GetComponent<Text>().text += dt[i].name + ", ";
            }
        }

    }


    void Update(){
		#if UNITY_EDITOR
		if (Input.GetKey (KeyCode.Escape)) {//esc 버튼을 눌렀을 때 
			switch (now) {
			case (int)sceneNumber.cam:
				now = (int)sceneNumber.title;
				loading();
				break;
			case (int)sceneNumber.info:
				infoBack();
				break;
			default:
				break;
			}
		}
		#elif UNITY_ANDROID
		if (Input.GetKey (KeyCode.Escape)) {//esc 버튼을 눌렀을 때 
		switch (now) {
		case (int)sceneNumber.cam:
		now = (int)sceneNumber.title;
		loading();
		break;
		case (int)sceneNumber.info:
		infoBack();
		break;
		default:
		break;
		}
		}
		#endif

		if (loadScene == true) {
			// ...then pulse the transparency of the loading text to let the player know that the computer is still working.
			load.color = new Color(load.color.r, load.color.g, load.color.b, Mathf.PingPong(Time.time, 1));
		}
	}

	private void resolutionChange( int width, int height ){
		Screen.SetResolution (width, height, true);
		css.referenceResolution = new Vector2 (width, height);
	}

	/// /////////////////
	/// 로딩을 위한 함수 
	/// 
	private void loading(){
		StartCoroutine (LoadNewScene ());
	}

	private IEnumerator LoadNewScene() {
		loadScene = true;
		scenes [2].SetActive (true);
		yield return new WaitForSeconds(1);
		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(0);
		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}
	}

	public void gpsInfo(){

	}

	/// ///////////////////////////////////////////////////////////////////////////////////
	/// 메인화면 + shopObject
	/// ////////////////////////////
	//Categorize button
	public void scrollButton(){
		if (anim.GetBool ("scrollIn")) {
			anim.SetBool ("scrollIn", false);
		} else {
			anim.SetBool ("scrollIn", true);
		}
	}

	public void categoryButton(string args){
		for (int i = 0; i < dt.Count; i++) {
			if ( soManager[i].category.CompareTo (args)==0) {
				if(soManager[i].gameObject.activeSelf) soManager [i].gameObject.SetActive (false);
				else soManager [i].gameObject.SetActive (true);
			}
		}
	}

	public void shopObjectInitialize(){//shop object들 초기화 
                                       //info manager에서 정보를 갖고 온다 
                                       //searchGPSInfo에 사용자의 현재 위치를 넣어준다
//        dt = infoManager.searchGPSInfo(gpsManager.lon, gpsManager.lat);
        dt = infoManager.searchGPSInfo(126.945996, 37.557636);
        Debug.Log("이부분 gps.lon gps.lat 문제로 임의로 gps 정보 넣음 아진짜 왜 에러가 나는지 1도모르겠다 으허엉");


        for (int i = 0; i < soManager.Length; i++) {
			soManager[i].gameObject.SetActive(false);
		}
		for (int i = 0; i < dt.Count; i++) {//각각의 shopObject에게 정보를 넘김 
			soManager[i].gameObject.SetActive(true);
			soManager [i].initialize (dt[i].id, dt [i].name, dt [i].isSale, dt [i].isEvent, dt [i].isCoupon,
				dt [i].category,new Location (dt [i].longitude, dt [i].latitude));
//			soManager [i].sizeChange (System.Math.Sqrt (//거리계산을 통한 사이즈 조정 
//				System.Math.Pow (dt [i].longitude - 30,2) + System.Math.Pow (dt [i].latitude - 100,2)));
		}
	}

	public void shopObjectButton(int index){//shopObject->information
		infosetData(index);
	}

	/// ///////////////////////////////////////////////////////////////////////////////////
	/// 정보화면 
	/// ////////////////////////////
	public void infosetData(int index){//정보 화면을 킨다,data 편집 
		now = (int)sceneNumber.info;
		resolutionChange (720, 1280);

		dt = infoManager.getInformation (soManager [index].shopId);

		infoKind [0].SetActive (true);//shop 정보 
		int length = 1280;
		infoP [0].infoInitialize (dt[0]);

		List<ShopEvent> se;

		if (dt [0].isSale!=0) {//sale 정보 
			infoKind [1].SetActive (true);//켠다

			se=infoManager.getEvent(dt[0].isSale);
			infoP [1].infoInitialize (se[0]);

			length += 1150;
			infosetTextBoxSize (1, length);
		} else {
			infoKind [1].SetActive (false);
		}

		if (dt [0].isEvent!=0) {//event 정보 
			infoKind [2].SetActive (true);

			se=infoManager.getEvent(dt[0].isEvent);
			infoP [2].infoInitialize (se[0]);

			length += 1150;
			infosetTextBoxSize (2, length);
		} else {
			infoKind [2].SetActive (false);
		}

		if (dt [0].isCoupon!=0) {//쿠폰정보 
			infoKind [3].SetActive (true);

			se=infoManager.getEvent(dt[0].isCoupon);
			infoP [3].infoInitialize (se[0]);

			length += 1150;
			infosetTextBoxSize (3, length);
		} else {
			infoKind [3].SetActive (false);
		}

		form.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, length);

		scenes[1].SetActive(true);
		scenes[0].SetActive(false);
	}

	public void infosetTextBoxSize( int index, int length ){//textbox 길이 조절 
		if (length == 2430) {
			infoKind [index].GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -1855);//위치 조정 
		} else if (length == 3580) {
			infoKind [index].GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -3005);//위치 조정 
		} else {
			infoKind[index].GetComponent<RectTransform>().anchoredPosition=new Vector2(0,-4155);//위치 조정 
		}
	}

	public void infoBack(){//정보->메인 캠 
		now=(int)sceneNumber.cam;
		scenes[0].SetActive(true);
		scenes[1].SetActive(false);
		resolutionChange (resolutionW, resolutionH);
	}

	public void infoCouponDown(){//정보-쿠폰 다운로드 
		cm.CouponMaking(new Coupon());
	}

}
