using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputScript : MonoBehaviour
{
	public InputField Field;
	private bool wasFocused;
	public string UUIDinput;
	public GameObject InputObject;

	void Start(){
		DontDestroyOnLoad(InputObject);
	}
	private void Update()
	{
		if (wasFocused && Input.GetKeyDown(KeyCode.Return)) {

			Submit(Field.text);
		}

		wasFocused = Field.isFocused;
	}

	private void Submit(string text)
	{
		Debug.Log("Submit=" + text);
		UUIDinput = text;
		SceneManager.LoadScene("MainScene");
	}
}