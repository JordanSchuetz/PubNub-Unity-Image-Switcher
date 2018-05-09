using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PubNubAPI;

public class mouseButton : MonoBehaviour {
	// Use this for initisoualization
	public Button Button1;
	public Button Button2;
	public Button Button3;
	void Start () {
		Button btn1 = Button1.GetComponent<Button>();
		Button btn2 = Button2.GetComponent<Button>();
		Button btn3 = Button3.GetComponent<Button>();
		btn1.onClick.AddListener(delegate{TaskOnClick(1);});
		btn2.onClick.AddListener(delegate{TaskOnClick(2);});
		btn3.onClick.AddListener(delegate{TaskOnClick(3);});
	}
	void TaskOnClick(int btnnumber){
		if (btnnumber == 1) {
			Debug.Log ("Pressed Button 1");
		}
		if (btnnumber == 2) {
			Debug.Log ("Pressed Button 2");
		}
		PublicPubNub.pubnub.Publish ()
			.Channel ("my_channel")
			.Message(btnnumber)
			.Async((result, status) => {
				if (!status.Error) {
					Debug.Log(string.Format("Publish Timetoken: {0}", result.Timetoken));
				} else {
					Debug.Log(status.Error);
					Debug.Log(status.ErrorData.Info);
				}
			});
	}
	
	// Update is called once per frame
	void Update () {

	}
}
