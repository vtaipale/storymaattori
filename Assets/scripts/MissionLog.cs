﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


//
/// <summary>
/// Mission log.
/// Creates missions and handles them : MISSION SEND IS IN HERE!
/// </summary>
public class MissionLog : MonoBehaviour {

	public Text missionText;
	public List<Mission> missions = new List<Mission>();
	public int currentlyAdded=0;
	public SoldierManager manager;
	public Mission mission;
	public EventController control;
	public Campaing ActualCampaing;

	public RectTransform BG;

	/// <summary>
	/// Generates a new Mission to send soldiers to!
	/// </summary>
	public void AddMission ()		
	{
		if (manager.soldiers.Count > 3) {
			Debug.Log ("Creating New Mission.");

			string target = "";
			string missionSelect = "";
			int Mission_DIFF = Random.Range((control.campaing.Campaing_Difficulty - 20), (control.campaing.Campaing_Difficulty + 20));

			int MissionTypeChance = Random.Range(0, 100); 

			if (MissionTypeChance > 90)	//GUARD/Defence!
			//if (true)	//testing! 
			{
				int targetSelect = Random.Range(0, 100);

				if (targetSelect< 25)
				{
					target = "Bunker #" + Random.Range(101, 999);
				}
				if (targetSelect< 50)
				{
					target = "Town #" + Random.Range(101, 999);
				}
				else if (targetSelect< 75)
				{
					target = "Array #" + Random.Range(101, 999);
				}
				else
				{
					target = "Outpost #" + Random.Range(101, 999);
				}

				missionSelect = "Guard";


			}

			else if (MissionTypeChance > 90)	//PATROL!
			{
				int targetSelect = Random.Range(0, 100);
				
				if (targetSelect< 25)
				{
					target = "Trench #" + Random.Range(101, 999);
				}
				if (targetSelect< 50)
				{
					target = "Cave #" + Random.Range(101, 999);
				}
				else if (targetSelect< 75)
				{
					target = "Complex #" + Random.Range(101, 999);
				}
				else
				{
					target = "Road #" + Random.Range(101, 999);
				}
				
				missionSelect = "Patrol";
				
				
			}
			else if (MissionTypeChance > 80)	//Partyparty!
			{
				int targetSelect = Random.Range(0, 100);
				
				if (targetSelect< 25)
				{
					target = "Spa #" + Random.Range(101, 999);
				}
				if (targetSelect< 50)
				{
					target = "Beach #" + Random.Range(101, 999);
				}
				else if (targetSelect< 75)
				{
					target = "Base #" + Random.Range(101, 999);
				}
				else
				{
					target = "Bar #" + Random.Range(101, 999);
				}

				missionSelect = "Vacation";


			}
			else if (MissionTypeChance < 10)		// ENEMY HQ ASSAULT MISSION! 
//			if (true)
			{
				int targetSelect = Random.Range(0, 100);
				
				if (targetSelect< 25)
				{
					target = "Enemy HQ #" + Random.Range(101, 999);
				}
				else if (targetSelect< 50)
				{
					target = "Hill #" + Random.Range(101, 999);
				}
				else if (targetSelect< 75)
				{
					target = "Fuel Depot #" + Random.Range(101, 999);
				}
				else
				{
					target = "Bunker #" + Random.Range(101, 999);
				}
				
				missionSelect = "Assault";
			}
			else  // Normal mission aka battlemission!
			{

				int targetSelect = Random.Range(0, 100);

				if (targetSelect< 25)
				{
					target = "Town #" + Random.Range(101, 999);
				}
				else if (targetSelect< 50)
				{
					target = "Canyon #" + Random.Range(101, 999);
				}
				else if (targetSelect< 75)
				{
					target = "Complex #" + Random.Range(101, 999);
				}
				else
				{
					target = "Depot #" + Random.Range(101, 999);
				}

				

				int missionRoll = Random.Range(0, 100);

				if (missionRoll< 25)
				{
					missionSelect = "Storm";
				}
				else if (missionRoll< 50)
				{
					missionSelect = "Liberation";
					Mission_DIFF += 5;
				}
				else if (missionRoll< 75)
				{
					missionSelect = "Attack";
				}
				else
				{
					missionSelect = "Raid";
				}
			}




			this.mission = new Mission (target, missionSelect, Mission_DIFF, control.campaing);
			this.mission.MissionName = "Mission " + (control.campaing.missionNumber+1) + "";
		}
	}
	/// <summary>
	/// This is the actual command to send Soldiers to a Mission.
	/// </summary>
	public void AddSquad()		//LADS GO TO BATTLE!BATTLE!
	{
		if (manager.inSquadCurrently == 4) {

			missions.Add (this.mission);
			Debug.Log(missions.IndexOf(mission));

			bool VictoryMatters = false;

//			Debug.Log ("Adding squad...");
//			Debug.Log (missions.IndexOf (mission));
//			Debug.Log (currentlyAdded);
//			Debug.Log ("mission @ " + missions [currentlyAdded]);
			missions [currentlyAdded].AddSquad (manager.GetSquad (manager.squadIds));		// Actual BATTLE
			Debug.Log ("Fighting....");

			if (this.mission.type == "Guard")
			{
				control.Guard (manager.squadIds, mission.difficulty, missions [currentlyAdded]);

			}
			else 
			if (this.mission.type == "Vacation")
			{
				control.Vacate (manager.squadIds, mission.difficulty, missions [currentlyAdded]);

			}
			else if (this.mission.type == "Patrol")
			{
				control.Patrol (manager.squadIds, mission.difficulty, missions [currentlyAdded]);
			}
			else if (this.mission.type == "Assault")
			{
				control.Fight (manager.squadIds, mission.difficulty, missions [currentlyAdded]);	//needs its own, not yet implemented!
				VictoryMatters = true;
			}
			else  {
				control.Fight (manager.squadIds, mission.difficulty, missions [currentlyAdded]);
			}

			Debug.Log ("ADDSQUAD: writing to log...");
			UpdateLog ();

			if (VictoryMatters)
			{
				if (missions [currentlyAdded-1].victory == true)
				{
					ActualCampaing.AssaultMissionReporting (true, missions [currentlyAdded-1].squad);
					Debug.Log ("mission "+ mission.MissionName +"was VICTORY!");
				}
				else
				{
					ActualCampaing.AssaultMissionReporting (false, missions [currentlyAdded-1].squad);
					Debug.Log ("mission "+ mission.MissionName +" was defeat!");
				}
			}
			manager.CheckForNewSoldiers(); // Done only here, checks regardless of deaths 

			ActualCampaing.CheckForNewEvents();

			manager.squadIds = new int[4]{-2,-2,-2,-2};
			manager.inSquadCurrently = 0;

			this.AddMission();		// NEW MISSION IS CREATED
		}
	}
	/// <summary>
	/// Updates log with lots of data!
	/// </summary>
	public void UpdateLog()
	{

		if (missionText.text == "- NO MISSIONS -")
			missionText.text = "";

		string temp=missions [currentlyAdded].ToString();
		missionText.text = missionText.text+temp;



		this.missionText.rectTransform.sizeDelta = new Vector2(359f, (missionText.text.Length*1.1f));

		this.BG.sizeDelta = new Vector2( 0f , (missionText.text.Length*1.1f));

		missionText.text += "\n\n";

		Debug.Log ("All Missions:" + missionText.text);
		Debug.Log ("Current:" + temp);
		
		currentlyAdded++;
	}
	/// <summary>
	/// Exports ALL DATA TO txt file in D:\Storymaattori_HistoryExport_ + CampaingYear + Timestamp
	/// </summary>
	public void EXPORT()
	{
		string returnoitava = "";
		
		returnoitava += control.campaing.alkuteksti;

		returnoitava += "Total Enemies Killed: " + control.campaing.TotalKills + "\n";
		returnoitava += "Total Soldier Deaths: " + control.campaing.TotalDead + "\n";
		returnoitava += "Total Missions: " + control.campaing.missionNumber + "\n";

		returnoitava += "\n\n\n +++MISSIONS+++\n";

		returnoitava += missionText.text;

		returnoitava += "\n\n\n +++ALIVE SOLDIERS+++\n";

		foreach (SoldierController solttu in control.manager.soldiers)
		{
			returnoitava += "\n\n";
			returnoitava += "--" + solttu.soldierID + "\n" + solttu.toString();
		}
		
		returnoitava += "\n\n\n +++DEAD SOLDIERS+++\n";

		foreach (SoldierController solttu in control.manager.dead)
		{
			returnoitava += "\n\n";
			returnoitava += "--" + solttu.soldierID + "\n" + solttu.toString();
		}

		returnoitava = returnoitava.Replace("\n", System.Environment.NewLine);

		System.IO.File.WriteAllText(@"D:\Storymaattori_HistoryExport_" + control.campaing.CampaingYear + "+" + control.campaing.TimeStamp + ".txt", returnoitava);


	}
}
