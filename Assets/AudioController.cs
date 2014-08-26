using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	public static int clipNumber = 0;

	private int currentSample = 0;
	private int totalSampleCount = 0;

	public static int levelNum = 0;
	private int currentAudioClip = 0;
	private int nextAudioClip = 0;

	private AudioClip[] samples;
	private Dictionary<int, float[]> sampleData = new Dictionary<int, float[]>();
	private int sampleLength;
	private int sampleIndex = 0;

	private bool finished = false;

	AudioController[] audioControllers;
	void Awake () {
		Application.runInBackground = true;
		AudioSettings.outputSampleRate = 44100;
		LoadAudioFromResources();
	}

	void LoadAudioFromResources(){
		samples = Resources.LoadAll<AudioClip>("Audio");
		if(samples==null||samples.Length==0)return;
		sampleLength = samples[0].samples*2;
		for(var i = 0; i < samples.Length; i++){
			sampleData[i] = new float[sampleLength];
			samples[i].GetData (sampleData[i], 0);
		}
	}
	
	float[] PlayFromFloat (int channels, int index){
		if (sampleIndex >= sampleLength) {
			return new float[2]{0,0};
		}
		float[] retVal = new float[channels];
		for (var c = 0; c<channels; c++) {
			retVal [c] = sampleData[index] [sampleIndex];
		}
		sampleIndex = sampleIndex + channels;
		if(sampleIndex >= sampleLength){
			sampleIndex = 0;
			currentAudioClip = nextAudioClip;
			nextAudioClip = -1;
		}
		return retVal;
	}
	
	void OnAudioFilterRead (float[] data, int channels){
		if(samples == null || samples.Length < 1 || finished)return;
		for (var i = 0; i < data.Length; i+=channels) {
			float[] audioFloat = PlayFromFloat (channels, currentAudioClip);
			for (var c = 0; c<channels; c++) {
				data [i + c] += audioFloat [c];
			}
			currentSample = (currentSample + 1) % sampleLength;
			
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			AudioController.levelNum++;
		}
		if(sampleIndex > sampleLength*0.95f){
			nextAudioClip = levelNum;
		}
	}
}