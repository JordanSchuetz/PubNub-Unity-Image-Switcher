using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;
using UnityEngine.UI;


public class PublicPubNub : MonoBehaviour {
	public static PubNub pubnub;
	public GameObject CubeObject;
	public Material Pepe;
	public Material Pepe2;
	public Material Pepe3;
	public float speed = 10f;
	public Text UUIDText;
	public Text lastClickText;
	public Text hi; 
	//public GameObject InputObject;
	public string inputResponse;

	// Use this for initialization
	void Start () {
		// Use this for initialization
		PNConfiguration pnConfiguration = new PNConfiguration ();
		pnConfiguration.PublishKey = "pub-c-efb4a8fe-605a-42e8-866a-facc7271a49d";
		pnConfiguration.SubscribeKey = "sub-c-1bb9d4ac-52f4-11e8-85c6-a6b0a876dba1";

		pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
		//pnConfiguration.UUID = Random.Range (0f, 999999f).ToString ();

		inputResponse= GameObject.Find("InputObject").GetComponent<InputScript>().UUIDinput.ToString();
		Debug.Log (inputResponse);
		pnConfiguration.UUID = inputResponse;

		pubnub = new PubNub(pnConfiguration);
		Debug.Log (pnConfiguration.UUID);
		UUIDText.text = "UUID: " + pnConfiguration.UUID;
		
		pubnub.SusbcribeCallback += (sender, e) => { 
			SusbcribeEventEventArgs mea = e as SusbcribeEventEventArgs;
			if (mea.Status != null) {
			}
			if (mea.MessageResult != null) {
				lastClickText.text = "Last Publish Sent by: " + mea.MessageResult.IssuingClientId.ToString();
				if((int)mea.MessageResult.Payload == 1){
					CubeObject.GetComponent<Renderer>().material = Pepe;
				}
				if((int)mea.MessageResult.Payload == 2){
					CubeObject.GetComponent<Renderer>().material = Pepe2;
				}
				if((int)mea.MessageResult.Payload == 3){
					CubeObject.GetComponent<Renderer>().material = Pepe3;
				}
			}
			if (mea.PresenceEventResult != null) {
				//lastClickText.text = mea.PresenceEventResult.UUID.ToString();
				Debug.Log("In Example, SusbcribeCallback in presence" + mea.PresenceEventResult.Channel + mea.PresenceEventResult.Occupancy + mea.PresenceEventResult.Event);
			}
		};
		pubnub.Subscribe ()
			.Channels (new List<string> () {
			"my_channel"
		})
			.WithPresence()
			.Execute();
	}
	
	// Update is called once per frame
	void Update () {
		CubeObject.transform.Rotate(Vector3.up, speed * Time.deltaTime);
		CubeObject.transform.Rotate(0f, 0f, -3f * Time.deltaTime);
	}
}
