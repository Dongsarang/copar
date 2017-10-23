using System;//for serializable
using System.Collections;//for list and other data structure
/*
 * 2017.08.10 정다은 생성 
 * reference: http://debuglog.tistory.com/36
 * 객체가 필요없는 클래스고 함수만 사용하면 되는 클래스이기 때문에 static class로 선언한다. 
 * 따라서 모든 함수도 static.
 */

public static class JsonHelper{
	//사용할 구조체나 클래스가 여러 종류라는 가정하에 일반화 사용.
	public static T[] FromJson<T>(string json){//json->object
		Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>> (json);
		return wrapper.Items;
	}

	public static string ToJson<T>(T[] array){//object->json
		Wrapper<T> wrapper=new Wrapper<T>();
		wrapper.Items=array;
		return UnityEngine.JsonUtility.ToJson (wrapper);
	}

	public static string ToJson<T>(T[] array, bool prettyPrint){//object->json
		Wrapper<T> wrapper=new Wrapper<T>();
		wrapper.Items=array;
		return UnityEngine.JsonUtility.ToJson (wrapper,prettyPrint);
	}

	[Serializable]//직렬화
	private class Wrapper<T>{//wrapper는 이 클래스에서 밖에 사용을 안 하므로 priate 클래스로 만들었다.
		public T[] Items;
	}
}
