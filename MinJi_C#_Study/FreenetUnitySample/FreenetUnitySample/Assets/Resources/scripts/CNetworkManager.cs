using UnityEngine;
using System;
using System.Collections;
using FreeNet;
using FreeNetUnity;
using VirusWarGameServer;

public class CNetworkManager : MonoBehaviour {

	CFreeNetUnityService gameserver;
	string received_msg;

	void Awake()
	{
		this.received_msg = "";

		// 네트워크 통신을 위해 CFreeNetUnityService객체를 추가합니다.
		this.gameserver = gameObject.AddComponent<CFreeNetUnityService>();

		// 상태 변화(접속, 끊김등)를 통보 받을 델리게이트 설정.
		this.gameserver.appcallback_on_status_changed += on_status_changed;

		// 패킷 수신 델리게이트 설정.
		this.gameserver.appcallback_on_message += on_message;
	}

	// Use this for initialization
	void Start()
	{
		connect();
	}

	void connect()
	{
		this.gameserver.connect("127.0.0.1", 7979);
	}

	/// <summary>
	/// 네트워크 상태 변경시 호출될 콜백 매소드.
	/// </summary>
	/// <param name="server_token"></param>
	void on_status_changed(NETWORK_EVENT status)
	{
		switch (status)
		{
				// 접속 성공.
			case NETWORK_EVENT.connected:
				{
					CLogManager.log("on connected");
					this.received_msg += "on connected\n";

					CPacket msg = CPacket.create((short)PROTOCOL.CHAT_MSG_REQ);
					msg.push("Hello!!!");
					this.gameserver.send(msg);
				}
				break;

				// 연결 끊김.
			case NETWORK_EVENT.disconnected:
				CLogManager.log("disconnected");
				this.received_msg += "disconnected\n";
				break;
		}
	}

	void on_message(CPacket msg)
	{
		// 제일 먼저 프로토콜 아이디를 꺼내온다.
		PROTOCOL protocol_id = (PROTOCOL)msg.pop_protocol_id();

		// 프로토콜에 따른 분기 처리.
		switch (protocol_id)
		{
			case PROTOCOL.CHAT_MSG_ACK:
				{
					string text = msg.pop_string();
					GameObject.Find("GameMain").GetComponent<CGameMain>().on_receive_chat_msg(text);
				}
				break;
		}
	}

	public void send(CPacket msg)
	{
		this.gameserver.send(msg);
	}
}
