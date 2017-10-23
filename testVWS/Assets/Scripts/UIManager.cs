using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 2017.08.06 정다은 생성 
/// 2017.08.07-08.13 수정
/// reference: http://www.thegamecontriver.com/2014/10/create-sliding-pause-menu-unity-46-gui.html
/// </summary>
public enum sceneNumber{
	title=1,coupon=2,cam=3,info=4
}

public class UIManager : MonoBehaviour {//ui에 필요한 함수
	private int now = (int)sceneNumber.title;
	[SerializeField]
	private GameObject[] scenes;//main title과 몇개 화면
	private Animator anim;

	private CouponManager cm;
	[SerializeField]
	private GameObject[] couponUI;
	[SerializeField]
	private Text[] couponText;
	[SerializeField]
	public RectTransform content;
	[SerializeField]
	private Text load;
	private bool loadScene = false;

	void Start(){
		content = GameObject.Find ("Content").GetComponent<RectTransform> ();
		cm = this.GetComponent<CouponManager> ();

//		if (scenes == null)//화면 갖고 오기 
//			scenes = GameObject.FindGameObjectsWithTag("UI");
		for (int i = 0; i < scenes.Length; i++) {
			scenes [i].SetActive (false);
		}
		scenes [0].SetActive (true);//main

		Screen.SetResolution (720, 1280, true);//화면 해상도 초기화
	}

	void Update(){
		#if UNITY_EDITOR
		if (Input.GetKey (KeyCode.Escape)) {
			switch (now) {
			case (int)sceneNumber.title:
				now = 0;
//				Application.Quit;
				break;
			case (int)sceneNumber.coupon:
				this.MainCoupon();
				break;
			default:
				break;
			}
		}
		#elif UNITY_ANDROID
		if (Input.GetKey (KeyCode.Escape)) {
		switch (now) {
		case (int)sceneNumber.title:
		now = 0;
		//				Application.Quit;
		break;
		case (int)sceneNumber.coupon:
		now=(int)sceneNumber.title;
		scenes [0].SetActive (true);
		scenes [1].SetActive (false);
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

	//////////////////////
	/// 메인화면  
	public void MainCamera(){//메인화면-카메라
		now=(int)sceneNumber.cam;
		loading ();
		//SceneManager.LoadScene (1);
	}
	public void MainCoupon(){//메인화면-쿠폰
		now=(int)sceneNumber.coupon;
		scenes[1].SetActive(true);
		int length = cm.couponInitialize();
		CouponUIInitialize (length);
		scenes[0].SetActive (false);
	}
//	public void MainSetting(){//메인화면-환경설정
//		if (!anim.GetBool ("openSetting")) {//환경설정이 켜짐 
//			anim.SetBool ("openSetting", true);
//		} else {
//			anim.SetBool ("openSetting", false);
//		}
//	}

	public void loading(){
		StartCoroutine (LoadNewScene ());
	}

	private IEnumerator LoadNewScene() {
		loadScene = true;
		scenes [2].SetActive (true);
		yield return new WaitForSeconds(3);
		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = Application.LoadLevelAsync(1);
		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}
	}

	//////////////////////
	//쿠폰
	private void CouponUIInitialize( int length ){//쿠폰 UI 초기화 
		if (length <= 4) {
			content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 1150);
		} else {
			content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 250*length +215);
		}

		for (int i = 0; i < length; i++) {
			couponUI [i].SetActive (true);
			couponText [i].text = cm.couponList[i].ToString();
		}
	}

	public void CouponBack(){ //뒤로 돌아가기 
		now=(int)sceneNumber.title;
		scenes [0].SetActive (true);
		scenes [1].SetActive (false);
	}
//	public void CouponUse(){//쿠폰 사용 버튼 누르기
//		
//	}
}
