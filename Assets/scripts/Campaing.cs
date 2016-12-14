﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Campaing
/// Large and high up in hierarchy: it is used to be deposit of lore & Connectivity between scripts!!!
/// </summary>
public class Campaing : MonoBehaviour {

	public int CampaingYear = 0;

	public int SquadID = 4118;
	public string SquadName = "Reaver Squad";

	public string PlanetName = "Nucheron IV";
	public string FriendName = "Human Empire";
	public string EnemyName = "Maulers of Ghan";

	public bool HumanAttacker = true;

	public string CauseOfWar = "";

	public string PlanetType = "arid";
	public string PlanetAdje = "dry";

	//for notes
	public int missionNumber = 0;
	public int MissionsToReinforcements = 10;		//when next reinforcements come in! Checked by SoldierManager by MissionLog (AddSquad) to see when next come in!
	public int MissionsToCampaingEvent = 10;		//when next fun stuff comes in.
	public int TimeStamp = 0;
	public int TotalKills = 0;
	public int TotalDead = 0;

	public int Campaing_Difficulty = 100; //used to determine current front difficulty. AFFECTS ALL BATTLES

	public Text BeginText;

	public Text WarLog;

	public string alkuteksti = "";

	public SoldierManager Soldiers;
	public ButtonController ButtonCont;
	public ReportController ReportCont;
	public MissionLog MissionTales;

	// Use this for initialization
	void Start () {

		BeginText.text = this.Begin();
		MissionsToReinforcements = Mathf.RoundToInt(Random.Range(3,6));
		MissionsToCampaingEvent = Random.Range(2,4) + Random.Range(2,4);	//AVG 6! - first one comes fast!
		//MissionsToCampaingEvent = Random.Range(3,7) + Random.Range(3,7);	//AVG 10!
		//MissionsToCampaingEvent = Random.Range(3,7) + Random.Range(3,7);	//AVG 10!
	}

	/// <summary>
	/// Gets NAME for the next mission and rolls the clock forward!
	/// Basically -next round- of campaing loop.
	/// 
	/// This is called by the invidinual Mission Types of Eventcontroller! (bit weird)
	/// </summary>
	/// <returns>"M + missionnumber"</returns>
	public string GetNextMission(){

		missionNumber++;
		MissionsToReinforcements--;
		MissionsToCampaingEvent--;
		TimeStamp += Mathf.RoundToInt((Random.Range(4, 8))+ (Random.Range(4, 8)));

		return "M"+ missionNumber;

	}

	// Update is called once per frame
	void Update () {

		WarLog.text = 
				"TimeStamp:" + "\n" + TimeStamp + "\n" +
				"Missions:" + "\n" + missionNumber + "\n" +
				"Reinforcements:" + "\n" + MissionsToReinforcements + "\n" +
				"Total Kills:" + "\n" + TotalKills + "\n" +
				"Total Deaths:" + "\n" + TotalDead+ "\n" +
				"Kills/Deaths:" + "\n";	

		if (TotalDead == 0)
		{
			WarLog.text += "?!?";
		}
		else
		{
			WarLog.text += ""+ TotalKills/TotalDead;
		}
	}
	

	public string Begin(){

		alkuteksti = "In the year ";

	 	CampaingYear =	Random.Range(4100, 4998);
		SquadID = Random.Range(1901, 2299);
		
		alkuteksti += CampaingYear.ToString() + ", the ";
				
		//WHICH ARE ATTACKERS
		int WarAttackerRandomiser = Random.Range(0, 1);

		switch (WarAttackerRandomiser)
		{
		case 0:
			HumanAttacker = true;
			break;
		case 1:
			HumanAttacker = false;
			break;
		}

		if (HumanAttacker)
			alkuteksti +=FriendName;
		else
			alkuteksti += EnemyName;

		//WHY
		int WarReasonRandomiser = Random.Range(0, 2);

		switch (WarReasonRandomiser)
		{
		case 0:
			this.CauseOfWar = "Attack";
			break;
		case 1:
			this.CauseOfWar = "Suprice";
			break;
		default:
			this.CauseOfWar = "Betrayal";
			break;
		}

		if (CauseOfWar == "Suprice"){
			alkuteksti += " supriced the ";
		}
		else if (CauseOfWar == "Betrayal"){
			alkuteksti += " betrayed the ";
		}
		else {
			alkuteksti += " attacked the ";
		}

		if (HumanAttacker)
			alkuteksti += EnemyName + " on the ";
		else
			alkuteksti += FriendName + " on the ";

		alkuteksti += PlanetType + " world of " + PlanetName + ".";

		alkuteksti += "\n The following is the story of " + FriendName + "s squad " + SquadID + " called the " + SquadName + ".";

		Debug.Log (alkuteksti);


		this.CreateWelcomeMessage ();


		return alkuteksti;
	}

	private void CreateWelcomeMessage()
	{
		string 	WelcomeMessage = "Greetings commander!\n";

		WelcomeMessage += "You are trusted with the destiny of "+this.SquadName+"! Watch and guide your troopers in battle and at the motherbase.\n\n";

		WelcomeMessage += "Currently this FRONT is stable but the situation can change rapidly.\n";
		WelcomeMessage += "Click NEXT MISSION to send your soldiers to their first combat!\n\n";

		this.ReportCont.CreateReinforcementsPopUp (Soldiers.soldiers);	// this first so it gets drawn behind!

		this.ReportCont.CreateWelcomePopup(WelcomeMessage);

	}


	public void CheckForNewEvents()
	{
		if (this.MissionsToCampaingEvent <= 0)
		{
			Debug.Log("NewFunCampaingEvent!");
			this.MissionsToCampaingEvent = Random.Range(3,7) + Random.Range(3,7);	//AVG 10!

			int FrontChange = Mathf.RoundToInt( Random.Range(-5,6) + Random.Range(-5,6) );	//avg is slightly positive because Players squad keeps killing enemies.

			if (FrontChange < 0)
			{
				this.ReportCont.CreateFrontChangePopup("Intelligence reports that the "+this.EnemyName+" are withdrawing some soldiers from this FRONT. Great job!");

				MissionTales.missionText.text += "WAR NEWS: ENEMIES ARE WITHDRAWING TROOPS FROM THIS FRONT.\n\n";

				foreach (SoldierController solttu in Soldiers.soldiers)
				{
					solttu.AddEvent("\nWord from HQ told that enemies are withdrawing some soldiers. A relief.\n");
					solttu.ChangeMorale(30);
				}
			}
			else if (FrontChange < 5) 
			{
				this.ReportCont.CreateFrontChangePopup("According to satelites, some new enemy platoons have been sighted moving towards this FRONT.");

				MissionTales.missionText.text += "WAR NEWS: ENEMIES ARE REINFORCING TROOPS OF THIS FRONT.\n\n";

				foreach (SoldierController solttu in Soldiers.soldiers)
				{
					solttu.AddEvent("\nWord from HQ more enemies are coming. Darn.\n");
					solttu.ChangeMorale(Random.Range(-5,5));
				}
			}
			else if (FrontChange < 10) 
			{
				this.ReportCont.CreateFrontChangePopup("Major setback! Enemy regiment inbound! Expect brutal combat in this FRONT soon!");

				MissionTales.missionText.text += "WAR NEWS: ENEMIES ARE BRINGING EXTRA REINFORCEMENTS TO THIS FRONT.\n\n";

				foreach (SoldierController solttu in Soldiers.soldiers)
				{
					solttu.AddEvent("\nWord from HQ told next battles will be hard. Shit.\n");
					solttu.ChangeMorale(-20);
				}
			}
		}
	}
}
