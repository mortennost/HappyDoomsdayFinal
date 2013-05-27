using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

namespace AssemblyCSharp
{
	public class GameCenterSingleton
	{
		private GameCenterPlatform gameCenterPlatform;
		
		private static GameCenterSingleton instance;
		public static GameCenterSingleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new GameCenterSingleton();
					instance.Initialize();
				}
				
				return instance;
			}
		}
		
		public bool Authenticated
		{
			get
			{
				if (Social.localUser.authenticated)
				{
					return true;
				}
				else
				{
					Debug.Log("User not Authenticated");
					return false;
				}
			}
		}
		
		public void Initialize()
		{
			if (!Authenticated)
			{
				Social.localUser.Authenticate(ProcessAuthentication);
			}
		}
		
		void ProcessAuthentication(bool success)
		{
			if (success)
			{
				Debug.Log("Authentication successful");
			}
			else
			{
				Debug.Log("Authentication failed");
			}
		}
		
		private GameCenterSingleton()
		{
		}
	}
}

