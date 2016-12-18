using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

//Handles the many parts of UI.
public class ButtonController : MonoBehaviour {
	
	public GameObject soldierView;
	public GameObject missionView;

	public GameObject deadSoldierView;
		public GameObject soldierSelectorView;

	public GameObject mainView;

	public SoldierManager manager;
	public MissionLog missions;

	public SoldierView DeadSoldierViewer;
	public SoldierView SoldierViewer;

	public AudioSource BOOM;

	public AudioSource NO;

	public AudioSource DebugBling;

	public void Start()
	{
		missions.AddMission();	//we always have one mission!

	}

	/// <summary>
	/// The actual Mission Send Signal to Missions! Works only if with 4 soldiers (currently)
	/// </summary>
	public void CheckSoldierGO_SoldierView()		//This is the GO signal, sends Soldiers to MISSION!
	{

		if (manager.inSquadCurrently == 4) {

			BOOM.Play ();

			missions.AddSquad();	//The actual GO SIGNAL TO MISSIONS!
			
			this.ActivateMissionViewButton();
			DeactivateSoldierSelectorView();
			mainView.SetActive(false);
		}
		else

		{

			NO.Play();
		}


	}

	/// <summary>
	/// Debug mission send: first 4 are sent with single button!
	/// </summary>
	public void DEBUG_Send4FirstSoldiersToBattle()	
	{

		//.soldiers{-2,-2,-2,-2}

		manager.squadIds = new int[4]{0,1,2,3};
		manager.inSquadCurrently = 4;

		// so that there is some noise that tester knows game hasnt frozen much noise. 
		DebugBling.Play ();


		missions.AddSquad();	//The actual GO SIGNAL TO MISSIONS!
			
		this.ActivateMissionViewButton();
		DeactivateSoldierSelectorView();
		mainView.SetActive(false);


		
	}

	/// <summary>
	/// Sends the first 4 soldiers to a missions HowManyTimes number of times.
	/// Quite slow. Note: does not play the correct amount of debugBlings!
	/// </summary>
	public void TEST_SendToMission(int HowManyTimes)
	{
		int Wohuuu = HowManyTimes;
		Debug.Log("TEST SEND TO MISSION - INITIALISATION: Run " + HowManyTimes +" missions.");

		for (int i = 1; i <= HowManyTimes;  i++)
		{
			Debug.Log("TEST SEND TO MISSION - Mission " + i +" out of " + HowManyTimes);
			this.DEBUG_Send4FirstSoldiersToBattle();
		}

	}

	
	public void ActivateSoldierSelectorView()
	{
		soldierSelectorView.SetActive (true);
		mainView.SetActive (false);
		missionView.SetActive (false);
	}
	
	public void DeactivateSoldierSelectorView()
	{
		soldierSelectorView.SetActive (false);
		mainView.SetActive (true);
	}
	
	
	public void ActivateSoldierViewButton(){
		soldierView.SetActive (true);

		SoldierViewer.CheckAliveMessage();
		//soldierView.transform.GetChild(0).transform.FindChild ("SoldierView").SendMessage ("CheckAliveMessage");
		mainView.SetActive (false);
	}
	
	public void ActivateDeadSoldierViewButton(){
		deadSoldierView.SetActive (true);
		DeadSoldierViewer.CheckAliveMessage();
		//deadSoldierView.transform.GetChild(0).transform.FindChild ("SoldierView").SendMessage ("CheckAliveMessage");
		mainView.SetActive (false);
	}
	
	public void ActivateMissionViewButton(){
		missionView.SetActive (true);
		mainView.SetActive (false);
	}
	
	public void DeActivateSoldierViewButton(){
		soldierView.SetActive (false);
		mainView.SetActive (true);
	}
	
	public void DeActivateDeadSoldierViewButton(){
		deadSoldierView.SetActive (false);
		mainView.SetActive (true);
	}
	
	public void DeActivateMissionViewButton(){
		missionView.SetActive (false);
		mainView.SetActive (true);
	}
	
}
