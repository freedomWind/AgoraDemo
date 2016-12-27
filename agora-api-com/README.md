#Setup the DEMO
1. Copy headers of Agora SDK into the AgoraRtcEngineControl/lib directory before you build the project.
2. Copy binaries of Agora SDK into the directory where AgoraRtcEngineControl.dll is genereated.
3. After AgoraRtcEngineControl.dll is genereated, run a command shell with Administrator privilege and then run the following command to register the COM component
    regsvr32 AgoraRtcEngineControl.dll
4. 用VS打开unity工程，添加引用AgoraRtcEngineControl.dll，会生成AgoraDemo\agora-api-com\Unity3DWithAgora\obj\Debug\Interop.AgoraRtcEngineControlLib.dll，把它放到AgoraDemo\agora-api-com\Unity3DWithAgora\Assets\Plugins目录下。
5. Specify your own APP ID when initialing Agora Rtc Engine in the function setupRtcEngine() within Test.cs.
