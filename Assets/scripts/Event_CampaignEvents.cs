using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_CampaignEvents : MonoBehaviour {

	public Campaing MyCampaing; 

	string [] curses = new string[] {
		"Darn.",
		"Shit.",
		"Not again!",
		"Curses!",
		"Bad times ahead.",
		"",
		"",
		"Time to pack extra ammo.",
		"It is time to check gear.",
		"Vacation sounded fun.",
		"It sounded fun.",
		"Rought times ahead.",
		"Fuck.",
		"Darn!",
		"Bugger." ,
		"Oh dear.",
		"Beer would sound fun."
	};

	string [] luck = new string[] {
		"Yes!",
		"Fun!",
		"Thanks!",
		"Yippee!",
		"Good times ahead.",
		"",
		"",
		"",
		"Beer would sound fun.",
		"Great.",
		"Lucky!",
		"Good job!",
		"Oh dear.",
		"OK!"
	};

	string [] SourceOfInfo = new string[] {
		"Espionage",
		"Our Spies",
		"Our Hackers",
		"Our Infiltrators",
		"Intelligence",
		"Military Intelligence",
		"Satelites",
		"Intelligence reports",
		"News from the front",
		"News from the HQ",
		"News from the FleetCom",
		"Recon",
		"Reconnaissance",
		"Scouts",
		"<Classified>",
		"HQ Rumours"
	};

	string [] TellWhat = new string[] {
		"reports",
		"told us",
		"proclaims",
		"tell us",
		"broadcasts"
	};

	string [] EnemyNames = new string[] {
		"the Enemy",
		"the Enemy",
		"the fiends",
		"the bastards",
		"our foes",
		"our oppressors",
		"our opponents",
		"our adversaries"
	};

	string [] EnemyRetreats = new string[] {
		"retreating from",
		"retreating from",
		"withdrawing soldiers from",
		"withdrawing troops from",
		"moving soldiers away from",
		"not reinforcing of",
		"not caring about",
		"not liking casualties of",
		"afraid of"
	};

	string [] EnemyAttacks = new string[] {
		"attacking in",
		"attacking in",
		"commercing an attack in",
		"reinforcing troops in",
		"sending reinforcements to bases in",
		"digging in at",
		"moving soldiers to",
		"moving soldiers back to"
	};

	string [] BadAlarms = new string[] {
		"Alert!",
		"Alarm!",
		"Major setback!",
		"Danger!",
		"Warning!",
		"Attention"
	};

	string [] EnemyMajorAttacks = new string[] {
		"Enemy regiment inbound",
		"Extra enemies are arriving",
		"Large enemy attack commercing",
		"Elite forces detected",
		"Large troop movement detected",
		"Huge enemy push incoming"
	};

	string [] DangerousFighting = new string[] {
		"brutal fighting",
		"heavy fighting",
		"heavy battles",
		"brutal battles",
		"heavy combat",
		"lethal dangers",
		"heavy casualties",
		"unusual dangers",
		"long battles"
	};


	public Event_CampaignEvents(Campaing CampaingInsert)
	{
		this.MyCampaing = CampaingInsert;

	}



	public void GradeSoldiers(){

		MyCampaing.ReportCont.SoldierListPopUp (MyCampaing.Soldiers.soldiers, "Soldier Check!",true);
		Debug.Log ("Soldier List Printed!");

		foreach (SoldierController solttu in MyCampaing.Soldiers.soldiers) 
		{
			solttu.AddEvent("\n" + solttu.GetFormalName() + " was graded to be " + solttu.QuickGradeSoldier()+ "!\n\n");
			solttu.ChangeMorale(5);
		}

	}





	public void CampaingEvent(string WhatToReport, string WhatToMissionLog){

		MyCampaing.ReportCont.CreateFrontChangePopup(WhatToReport);

		MyCampaing.MissionTales.missionText.text += "WAR NEWS: " + WhatToMissionLog + "\n\n";

	}



	public void CampaingEvent_Good (int change)
	{
		string SourceOfInfoInsert = SourceOfInfo[(Mathf.RoundToInt(Random.value*(SourceOfInfo.GetLength(0)-1)))];

		string TellWhatInsert = TellWhat[(Mathf.RoundToInt(Random.value*(TellWhat.GetLength(0)-1)))];

		string EnemyRetreatInsert = EnemyRetreats[(Mathf.RoundToInt(Random.value*(EnemyRetreats.GetLength(0)-1)))];



		string WhatToReport = SourceOfInfoInsert + "  " + TellWhatInsert + " that " + WhatToCallTheEnemy() +" are " +EnemyRetreatInsert + " this FRONT!!";


		this.CampaingEvent(WhatToReport, "ENEMIES ARE "+ EnemyRetreatInsert.ToUpper()+" THIS FRONT.");


		foreach (SoldierController solttu in MyCampaing.Soldiers.soldiers)
		{
			string luckInsert = luck[(Mathf.RoundToInt(Random.value*(luck.GetLength(0)-1)))];
			if (((solttu.CheckTrait("heroic")||solttu.CheckTrait("young")) && ((Random.Range (0,10) > 5)))|| (solttu.CheckTrait("idiot")) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\nWord from "+SourceOfInfoInsert+" told that we are winning! "+luckInsert+ "\n");
				solttu.ChangeMorale(40);
			}
			else if ((solttu.CheckTrait("coward")||solttu.CheckTrait("young")) && ((Random.Range (0,10) > 4)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\nWord from "+SourceOfInfoInsert+" whispered that we shall have easy time!! "+luckInsert+ "\n");
				solttu.ChangeMorale(40);
			}
			else if (solttu.CheckTrait("veteran") && ((Random.Range (0,10) > 5)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\n According to news, we are doing something well on this battlefield. "+luckInsert+ "\n");
				solttu.ChangeMorale(20);
			}
			else if (solttu.CheckTrait("drunkard") && ((Random.Range (0,10) > 4)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\n"+SourceOfInfoInsert+" Reported good news! Time for a celebratory dring! Cheers!\n");
				solttu.ChangeMorale(40);
			}
			else if ((solttu.CheckTrait("loner")||solttu.CheckTrait("techie")||solttu.CheckTrait("robo-heart")) && ((Random.Range (0,10) > 8)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\n"+SourceOfInfoInsert+" Reported that there is not that many enemies nearby.\n");
				solttu.ChangeMorale(10);
			}

			else {
				

				solttu.AddEvent("\nWord from "+SourceOfInfoInsert+" told that enemies are withdrawing some soldiers. "+luckInsert+ "\n");
				solttu.ChangeMorale(30);
			
			}
		}

	}

	public void CampaingEvent_Neutral (int change)
	{
		
		string SourceOfInfoInsert = SourceOfInfo[(Mathf.RoundToInt(Random.value*(SourceOfInfo.GetLength(0)-1)))];

		string TellWhatInsert = TellWhat[(Mathf.RoundToInt(Random.value*(TellWhat.GetLength(0)-1)))];

		string EnemyAttackInsert = EnemyAttacks[(Mathf.RoundToInt(Random.value*(EnemyAttacks.GetLength(0)-1)))];



		string WhatToReport = "According to " + SourceOfInfoInsert + ", " + WhatToCallTheEnemy() +" are " +EnemyAttackInsert + " this FRONT!!";

		this.CampaingEvent(WhatToReport, "ENEMIES ARE "+ EnemyAttackInsert.ToUpper()+" THIS FRONT.");

		foreach (SoldierController solttu in MyCampaing.Soldiers.soldiers)
		{
			string luckInsert = luck[(Mathf.RoundToInt(Random.value*(luck.GetLength(0)-1)))];
			string CurseInsert = curses[(Mathf.RoundToInt(Random.value*(curses.GetLength(0)-1)))];

			if ((solttu.CheckTrait("heroic") && ((Random.Range (0,10) > 4))) || (solttu.CheckTrait("idiot")) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\nSoldiers speak that more enemies are incoming! "+luckInsert+ "\n");
				solttu.ChangeMorale(10);
			}
			else if ((solttu.CheckTrait("coward")||solttu.CheckTrait("young")) && ((Random.Range (0,10) > 4)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\nWord from "+SourceOfInfoInsert+" "+ TellWhatInsert + " that "+ EnemyAttackInsert +"!! "+CurseInsert+ "\n");
				solttu.ChangeMorale(-30);
			}
			else if (solttu.CheckTrait("veteran") && ((Random.Range (0,10) > 5)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\n Heard that there will be more battles. "+CurseInsert+"\n");
				solttu.ChangeMorale(-5);
			}
			else if (solttu.CheckTrait("drunkard") && ((Random.Range (0,10) > 4)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\n"+SourceOfInfoInsert+" Reported bad news! Time for a confronting drink! Chug!\n");
				solttu.ChangeMorale(-15);
			}
			else if ((solttu.CheckTrait("loner")||solttu.CheckTrait("techie")||solttu.CheckTrait("robo-heart")) && ((Random.Range (0,10) > 8)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\n"+SourceOfInfoInsert+" Reported that will be more enemies coming in.\n");
				solttu.ChangeMorale(-10);
			}

			else {


				solttu.AddEvent("\n Heard that the enemies are "+EnemyAttackInsert+" this front. "+CurseInsert+"\n");
				solttu.ChangeMorale(-10);

			}
		}
	}

	public void CampaingEvent_Bad (int change)
	{
		
		string SourceOfInfoInsert = SourceOfInfo[(Mathf.RoundToInt(Random.value*(SourceOfInfo.GetLength(0)-1)))];

		string TellWhatInsert = TellWhat[(Mathf.RoundToInt(Random.value*(TellWhat.GetLength(0)-1)))];

		string BadAlarmInsert = BadAlarms[(Mathf.RoundToInt(Random.value*(BadAlarms.GetLength(0)-1)))];

		string EnemyMajorAttacksInsert = EnemyMajorAttacks[(Mathf.RoundToInt(Random.value*(EnemyMajorAttacks.GetLength(0)-1)))];

		string DangerousFightingInsert = DangerousFighting[(Mathf.RoundToInt(Random.value*(DangerousFighting.GetLength(0)-1)))]; 


		string WhatToReport = BadAlarmInsert +"! " + SourceOfInfoInsert + "  " + TellWhatInsert + " that " + EnemyMajorAttacksInsert + "! Expect "+DangerousFightingInsert+" in this FRONT!!";


		this.CampaingEvent(WhatToReport, EnemyMajorAttacksInsert.ToUpper());

		foreach (SoldierController solttu in MyCampaing.Soldiers.soldiers)
		{
			solttu.AddHistory ("-BADFIGHTING-");

			string luckInsert = luck[(Mathf.RoundToInt(Random.value*(luck.GetLength(0)-1)))];
			string CurseInsert = curses[(Mathf.RoundToInt(Random.value*(curses.GetLength(0)-1)))];

			if ((solttu.CheckTrait("idiot") && (Random.Range (0,10) > 2)))
			{

				solttu.AddEvent("\nFriends speak that more of enemies are incoming! "+luckInsert+ "\n");
				solttu.ChangeMorale(20);

			}
			else if ((solttu.CheckTrait("heroic") && ((Random.Range (0,10) > 4))) || (solttu.CheckTrait("idiot")) || (Random.Range (0,10) > 9) )
			{

					solttu.AddEvent("\nSoldiers speak that lots of enemies are incoming! "+CurseInsert+ "\n");
				solttu.ChangeMorale(5);
			}
			else if ((solttu.CheckTrait("coward")||solttu.CheckTrait("young")) && ((Random.Range (0,10) > 4)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\n" + EnemyMajorAttacksInsert +"!!! "+CurseInsert+ "\n");
				solttu.ChangeMorale(-50);

				if ((Random.Range (0, 10) > 7))		// yes, crushing damage!
				{

					solttu.AddAttribute ("depressed");
					solttu.AddEvent("This will soon end...\n");
					solttu.ChangeMorale (-20);
				}
			}
			else if (solttu.CheckTrait("veteran") && ((Random.Range (0,10) > 5)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\nWord from "+SourceOfInfoInsert+" "+ TellWhatInsert + " that "+ EnemyMajorAttacksInsert +". Tought times ahead.\n");
				solttu.ChangeMorale(-10);
			}
			else if ((solttu.CheckTrait("drunkard") && (Random.Range (0,10) > 4)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\n"+SourceOfInfoInsert+" Reported extraordinarily bad news! Time for a helpful drinking! ");
				solttu.ChangeMorale(-15);

				Event_Vacation Drinking = new Event_Vacation("", solttu);

				Drinking.Shots(solttu.health, 0);	// hope that this works
			}
			else if ((solttu.CheckTrait("loner")||solttu.CheckTrait("techie")||solttu.CheckTrait("robo-heart")) && ((Random.Range (0,10) > 8)) || (Random.Range (0,10) > 9))
			{

				solttu.AddEvent("\n"+SourceOfInfoInsert+" Reported that will be more enemies coming in.\n");
				solttu.ChangeMorale(-10);
			}

			else {

				solttu.AddEvent("\n Heard that difficult times are ahead.. "+CurseInsert+"\n");
				solttu.ChangeMorale(-20);

			}

		}


	}

	private string WhatToCallTheEnemy()
	{

		string EnemyName = "";

		if (Random.Range (0, 100) > 60)
			return MyCampaing.EnemyName;

		EnemyName = EnemyNames[(Mathf.RoundToInt(Random.value*(EnemyNames.GetLength(0)-1)))];

		return EnemyName;


	}





}
