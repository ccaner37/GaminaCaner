using Gamina.Maze.Controllers.Container;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gamina.Maze.Utility.Vibration
{
    public static class Vibrator
    {

#if UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			private static AndroidJavaObject CurrentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			private static AndroidJavaObject AndroidVibrator = CurrentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
			private static AndroidJavaClass VibrationEffectClass;
			private static AndroidJavaObject VibrationEffect;
			private static int DefaultAmplitude;
            private static IntPtr AndroidVibrateMethodRawClass = AndroidJNIHelper.GetMethodID(AndroidVibrator.GetRawClass(), "vibrate", "(J)V", false);
            private static jvalue[] AndroidVibrateMethodRawClassParameters = new jvalue[1];
#else
		private static AndroidJavaClass UnityPlayer;
		private static AndroidJavaObject CurrentActivity;
		private static AndroidJavaObject AndroidVibrator = null;
		private static AndroidJavaClass VibrationEffectClass = null;
		private static AndroidJavaObject VibrationEffect;
		private static int DefaultAmplitude;
		private static IntPtr AndroidVibrateMethodRawClass = IntPtr.Zero;
		private static jvalue[] AndroidVibrateMethodRawClassParameters = null;
#endif

		public static long LightDuration = 20;
        public static int LightAmplitude = 40;

        private static long[] _lightimpactPattern = { 0, LightDuration };
        private static int[] _lightimpactPatternAmplitude = { 0, LightAmplitude };
		private static int _sdkVersion = -1;

		private static bool _isVibrating;

		static Vibrator()
        {
			ContainerTriggerController.OnBallCollected += LightVibrate;
        }

		public static bool Android()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
				return true;
#else
			return false;
#endif
		}

		public static void AndroidVibrate(long[] pattern, int[] amplitudes, int repeat)
		{
			if (!Android()) { return; }
			if ((AndroidSDKVersion() < 26))
			{
				AndroidVibrator.Call("vibrate", pattern, repeat);
			}
			else
			{
				VibrationEffectClassInitialization();
				VibrationEffect = VibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", new object[] { pattern, amplitudes, repeat });
				AndroidVibrator.Call("vibrate", VibrationEffect);
			}
		}

		private static void VibrationEffectClassInitialization()
		{
			if (VibrationEffectClass == null)
			{
				VibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
			}
		}

		public static int AndroidSDKVersion()
		{
			if (_sdkVersion == -1)
			{
				int apiLevel = int.Parse(SystemInfo.operatingSystem.Substring(SystemInfo.operatingSystem.IndexOf("-") + 1, 3));
				_sdkVersion = apiLevel;
				return apiLevel;
			}
			else
			{
				return _sdkVersion;
			}
		}

		public static async void LightVibrate()
        {
			if (_isVibrating) return;
			_isVibrating = true;

			AndroidVibrate(_lightimpactPattern, _lightimpactPatternAmplitude, -1);

			await Task.Delay(500);
			_isVibrating = false;
		}
	}
}
