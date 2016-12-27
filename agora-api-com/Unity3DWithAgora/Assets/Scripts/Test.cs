using UnityEngine;
using System;
using System.Collections;
using AgoraRtcEngineControlLib;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Test : MonoBehaviour
{
	public Text BtnText;
	public Text Log;
	AgoraRtcEngineControl m_engine = null;
	bool m_joined = false;
	public Text ChannelName;

	// Use this for initialization
	void Start ()
	{
		AllocConsole ();
		setupRtcEngine ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void setupRtcEngine ()
	{
		try {
			m_engine = new AgoraRtcEngineControl ();
			m_engine.initialize ("your APP ID");
			m_engine.setupLocalVideo (/*videoLocal.Handle*/IntPtr.Zero, AgoraRtcEngineControlLib.RenderMode.RenderMode_Hidden);
			m_engine.enableVideo ();
			//m_engine.onJoinChannelSuccess += new _IAgoraRtcEngineControlEvents_onJoinChannelSuccessEventHandler(this.onJoinChannelSuccess);
			//m_engine.onRejoinChannelSuccess += new _IAgoraRtcEngineControlEvents_onRejoinChannelSuccessEventHandler(this.onRejoinChannelSuccess);
			//m_engine.onFirstRemoteVideoDecoded += new _IAgoraRtcEngineControlEvents_onFirstRemoteVideoDecodedEventHandler(this.onFirstRemoteVideoDecoded);
			//m_engine.onApiCallExecuted += new _IAgoraRtcEngineControlEvents_onApiCallExecutedEventHandler(this.onApiCallExecuted);
			//m_engine.onLeaveChannel += new _IAgoraRtcEngineControlEvents_onLeaveChannelEventHandler(this.onLeaveChannel);
		} catch (Exception e) {
			DebugLog (e);
		}
		//String devices = m_engine.enumerateVideoCaptureDevices();
	}

	private void onJoinChannelSuccess (String channel, uint uid, int elapsed)
	{
		DebugLog (String.Format ("joined channel '{0}' uid {1} elapsed {2}\n", channel, uid, elapsed));
	}

	private void onRejoinChannelSuccess (String channel, uint uid, int elapsed)
	{
		DebugLog (String.Format ("rejoined channel '{0}' uid {1} elapsed {2}\n", channel, uid, elapsed));
	}

	private void onLeaveChannel (uint duration, uint txBytes, uint rxBytes)
	{
		DebugLog (String.Format ("left channel: duration {0} seconds tx/rx bytes {1}/{2}\n", duration, txBytes, rxBytes));
	}

	private void onApiCallExecuted (String api, int result)
	{
		DebugLog (String.Format ("onApiCallExecuted: '{0}' returns {1}\n", api, result));
	}

	private void onFirstRemoteVideoDecoded (uint uid, int width, int height, int elapsed)
	{
		DebugLog (String.Format ("uid: {0} width {1} height {2} elapsed {3}\n", uid, width, height, elapsed));
	}

	public void btnJoinChannel_Click ()
	{
		if (String.IsNullOrEmpty (ChannelName.text)) {
			DebugLog ("Need Channel Name");
			return;
		}
		if (!m_joined) {
			if (m_engine.joinChannel ("", ChannelName.text, "", 0) == 0) {
				m_joined = true;
				BtnText.text = "Leave Channel";
			}
		} else {
			if (m_engine.leaveChannel () == 0) {
				m_joined = false;
				BtnText.text = "Join Channel";
			}
		}
		DebugLog (m_joined);
	}

	void DebugLog (object msg)
	{
		Debug.Log (msg);
		Log.text += msg.ToString () + String.Format ("\n");
	}

	[DllImport ("Kernel32.dll")]
	private static extern bool AllocConsole ();
}
