using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{

		private int currentSample = 0;
		private int totalSampleCount = 0;

		private int currentAudioClip;
		private int nextAudioClip = -1;

		private AudioClip[] samples;
		private Dictionary<int, float[]> sampleData = new Dictionary<int, float[]> ();
		private int sampleLength;
		private int sampleIndex = 0;
	
		private int[][] themeIndeces = new int[][]{
		new int[]{0}, //Intro
		new int[]{1,2,3}, //Forest
		new int[]{4,5}, //Forest/Cave
		new int[]{6,7,8}, //Cave
		new int[]{9,10},
		new int[]{11,12,13},
		new int[]{14,15},
		new int[]{16,17,18},
		new int[]{19,20,21,22},
		new int[]{23}

	};
		private bool finished = false;
		private int currentTheme = 0;
		public int nextTheme = 0;
		private int currentSubTheme = 0;
		private int nextSubTheme = 0;

		AudioController[] audioControllers;
		void Awake ()
		{
				Application.runInBackground = true;
				AudioSettings.outputSampleRate = 44100;
				LoadAudioFromResources ();
		}

		void LoadAudioFromResources ()
		{
				samples = Resources.LoadAll<AudioClip> ("Audio");
				if (samples == null || samples.Length == 0)
						return;
				sampleLength = samples [0].samples * 2;
				for (var i = 0; i < samples.Length; i++) {
						sampleData [i] = new float[sampleLength];
						samples [i].GetData (sampleData [i], 0);
				}
		}
	
		float[] PlayFromFloat (int channels, int index)
		{
				if (sampleIndex >= sampleLength) {
						return new float[2]{0,0};
				}
				float[] retVal = new float[channels];
				for (var c = 0; c<channels; c++) {
						retVal [c] = sampleData [index] [sampleIndex];
				}
				sampleIndex = sampleIndex + channels;
				if (sampleIndex >= sampleLength) {
						sampleIndex = 0;
						currentAudioClip = nextAudioClip;
						nextAudioClip = -1;
				}
				return retVal;
		}
	
		void OnAudioFilterRead (float[] data, int channels)
		{
				if (samples == null || samples.Length < 1 || finished)
						return;
				for (var i = 0; i < data.Length; i+=channels) {
						float[] audioFloat = PlayFromFloat (channels, currentAudioClip);
						for (var c = 0; c<channels; c++) {
								data [i + c] += audioFloat [c];
						}
						currentSample = (currentSample + 1) % sampleLength;
			
				}
		}

		void Update ()
		{
				if (nextAudioClip == -1 && sampleIndex > sampleLength * 0.95f) {
						int previousTheme = currentTheme;
						currentTheme = nextTheme;
						if (currentTheme == previousTheme && currentTheme == 0) {
								nextTheme = currentTheme = 1;
						}
						if (currentTheme == themeIndeces.Length - 1 && previousTheme == currentTheme) {
								finished = true;
								return;
						}
						int currentThemeLength = themeIndeces [currentTheme].Length;
						nextSubTheme = UnityEngine.Random.Range (0, currentThemeLength);
						if (currentTheme == previousTheme && nextSubTheme == currentSubTheme && currentThemeLength > 1) {
								nextSubTheme = (nextSubTheme + 1) % currentThemeLength;
								Debug.Log (nextSubTheme);
						}
						currentSubTheme = nextSubTheme;
						nextAudioClip = themeIndeces [currentTheme] [currentSubTheme];
						Debug.Log (nextAudioClip);
						Debug.Log (currentTheme + " - " + nextAudioClip);
				}
				if (Input.GetKeyDown (KeyCode.Space)) {
						nextTheme = currentTheme + 1;
						if (nextTheme >= themeIndeces.Length) {
								nextTheme = 1;
						}
						Debug.Log (nextTheme);
				}
		}
}