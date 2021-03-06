﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ReportController : MonoBehaviour {

	public Campaing campaing;

	public int ReportNumber = 127;
	
	public GameObject mainViewCanvas;
	public GameObject NewsView;	//news popup 
	public Transform HereNews;

	public bool ShowNewReports = true;
	public bool ShowDeadReports = true;

	//BOOLEANS TO check does most normal popups appear - Deaths and new dudes !


	public int NextReportNumber()
	{
		this.ReportNumber += UnityEngine.Random.Range(1,8)+ UnityEngine.Random.Range(1,8);

		return ReportNumber;

	}


	public void CreateNewsPopup(string NewsImput, String HeaderInput){

		int ThisNewsNumber = NextReportNumber();

		string HeaderReturn =HeaderInput +": TS:" + campaing.TimeStamp + "/" + ThisNewsNumber + "\n"; 

		string MainTextReturn = NewsImput;

		GameObject NewNews = (GameObject)Instantiate(NewsView, new Vector3(0f,0f,0f),Quaternion.identity);

		NewNews.name = "News: " + campaing.TimeStamp + "/" + ThisNewsNumber;

		NewsPanel OurNewText = NewNews.GetComponentInChildren<NewsPanel>();
		
		if (OurNewText != null)
		{
			OurNewText.UpdatePanel(MainTextReturn, HeaderReturn);
		}
		else
			Debug.Log("NewsPaper does not find its Text!: " + NewsImput);
		
		NewNews.transform.parent = HereNews.transform;
		
		NewNews.transform.localPosition = new Vector3(0f,0f,0f);		// so in correct location
		
	}

	/// <summary>
	/// Basic Popup. Header is REPORT: TS:XXX / YYY.
	/// </summary>
	/// <param name="NewsImput">News imput.</param>
	public void CreateNewsPopup(string NewsImput){

		int ThisNewsNumber = NextReportNumber();

		string HeaderReturn ="Report: TS:" + campaing.TimeStamp + "/" + ThisNewsNumber + "\n"; 

		this.CreateNewsPopup (NewsImput, HeaderReturn);
	}




	public void CreateNewSoldierPopup(SoldierController Recruit)
	{
		if (this.ShowNewReports == true)
		{
			string ToReturn = "--New Soldier--\n" + Recruit.AllNamesNoRANK().ToUpper()+"\n";

			ToReturn += "ID: " + campaing.SquadID + "/" + Recruit.soldierID + "\n";
			ToReturn += "Skill: " + StatToShort(Recruit.skill) +"\n";
			ToReturn += "Health: " + StatToShort(Recruit.health) +"\n";
			ToReturn += "Morale: " + StatToShort(Recruit.morale) +"\n";

			this.CreateNewsPopup(ToReturn);
		}
		
	}

	public void CreateEmergencySoldierPopup(SoldierController Recruit)
	{
		if (this.ShowNewReports == true)
		{
			string ToReturn = "--Emergency Recruit--\n" + Recruit.AllNamesNoRANK().ToUpper()+"\n";
			
			ToReturn += "ID: " + campaing.SquadID + "/" + Recruit.soldierID + "\n";
			ToReturn += "Skill: " + StatToShort(Recruit.skill) +"\n";
			ToReturn += "Health: " + StatToShort(Recruit.health) +"\n";
			ToReturn += "Morale: " + StatToShort(Recruit.morale) +"\n";
			
			this.CreateNewsPopup(ToReturn);
		}
		
	}

	public void CreateReinforcementsPopUp(List<SoldierController> Reinforcements)
	{
		if (Reinforcements.Count == 0) 
		{
			Debug.Log ("trying to reinforce when no soldiers needeed?!");
		}
		else if (Reinforcements.Count == 1)
		{
			bool SavingFormerStatus = this.ShowNewReports;	// save it - when many come at the same time no invidinual news!
			campaing.ReportCont.ShowNewReports = true;

			this.CreateNewSoldierPopup(Reinforcements[0]);
			
			this.ShowNewReports = SavingFormerStatus;	// save it - when many come at the same time no invidinual news!
		}
		else
		{
			this.SoldierListPopUp (Reinforcements, "Reinforcements");
		}
	}


	public void SoldierListPopUp(List<SoldierController> WhoToCheck, String WhatHeader)
	{
		this.SoldierListPopUp (WhoToCheck, WhatHeader, false);
	
	}

	public void SoldierListPopUp(List<SoldierController> WhoToCheck, String WhatHeader, bool ShowRank)
	{
		string ToReturn = "--"+WhatHeader+"--\n";

		foreach (SoldierController solttu in WhoToCheck)
		{
			if (ShowRank)
				ToReturn += solttu.AllNames() + " - "+ solttu.QuickGradeSoldier() +"\n";
			else 
				ToReturn += solttu.AllNamesNoRANK() + " - "+ solttu.QuickGradeSoldier() +"\n";
		}	

		this.CreateNewsPopup(ToReturn);

	}


	
	public void CreateDEADSoldierPopup(SoldierController Corpse)
	{
		if (this.ShowDeadReports == true)
		{
			string ToReturn = "--Casualty--\n" + Corpse.GetRank() +"\n"+ Corpse.AllNamesNoRANK().ToUpper() +"\n" + Corpse.HowDied+"\n";

			ToReturn += "Missions: "+Corpse.missions +" | Kills: " +Corpse.kills+"\n";

			if (Corpse.awards.Count > 0)
				ToReturn += "Medals: "+ Corpse.GetAwardsShort();

			this.CreateNewsPopup(ToReturn);
		}
		
	}

	
	public void CreateWelcomePopup(string NewsImput){




		int ThisNewsNumber = NextReportNumber(); // for the welcome message!

		// SoldierCheckPopUp - first so it gets rendered below!
;
		this.SoldierListPopUp(this.campaing.Soldiers.soldiers,"Your Soldiers");


		// and the actual welcomemessage!
	
		this.CreateNewsPopup(NewsImput,"Welcome");


	}

	public void CreateWarReport(string NewsImput){
		
		string ToReturn = ""; 

		ToReturn += "REPORT FROM HQ:\n";

		ToReturn += NewsImput;
		
		this.CreateNewsPopup(ToReturn,"WAR NEWS");
	}

	public void ToggleShowNewReports()
	{
		if (ShowNewReports == true)
			ShowNewReports = false;
		else
			ShowNewReports = true;
	}
	public void ToggleShowDeadReports()
	{
		if (ShowDeadReports == true)
			ShowDeadReports = false;
		else
			ShowDeadReports = true;

	}

	private string StatToShort(int Stat)
	{
		if (Stat > 110)
			return "++";
		else if (Stat > 105)
			return "+";
		else if (Stat < 90)
			return "--";
		else if (Stat < 95)
			return "-";
		
		return "Avg";
		
	}

}
