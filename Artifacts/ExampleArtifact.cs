using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using RoR2;

namespace AutoItemPickup
{
	class ExampleArtifact : ArtifactBase
	{
		public static ConfigEntry<int> TimesToPrintMessageOnStart;
		public override string ArtifactName => "Artifact of Example";
		public override string ArtifactLangTokenName => "ARTIFACT_OF_EXAMPLE";
		public override string ArtifactDescription => "When enabled, print a message to the chat at the start of the run.";
		public override Sprite ArtifactEnabledIcon => Sprite.Create(new Rect(0.0f, 0.0f, 5f, 5f), new Vector2(0.5f, 0.5f), 100.0f);
		public override Sprite ArtifactDisabledIcon => Sprite.Create(new Rect(0.0f, 0.0f, 5f, 5f), new Vector2(0.5f, 0.5f), 100.0f);

		public override void Init(ConfigFile config)
		{
			CreateConfig(config);
			CreateLang();
			CreateArtifact();
			Hooks();
		}
		private void CreateConfig(ConfigFile config)
		{
			TimesToPrintMessageOnStart = config.Bind<int>("Artifact: " + ArtifactName, "Times to Print Message in Chat", 5, "How many times should a message be printed to the chat on run start?");
		}
		public override void Hooks()
		{
			Run.onRunStartGlobal += PrintMessageToChat;
		}
		private void PrintMessageToChat(Run run)
		{
			if (NetworkServer.active && ArtifactEnabled)
			{
				Chat.AddMessage("I maen it should work? It's not like there's anything crazy happening here.");
			}
		}
	}
}