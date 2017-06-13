using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_GuardIdle : MonoBehaviour {

	public List<SoldierController> squad;

	public void GuardBoredom (List<SoldierController> INJECT, Mission MissionImput){
	
		this.squad = INJECT;

		foreach (SoldierController idler in squad) {


			/* 
			 *  Idling
			 *  healing wounds
			 *  fortifying!
			 *  EXTRA FORTIFYING
			 *  talking with dudes
			 *  Shortage of something?
			 */





			idler.AddEvent ("No enemy attack, ");
			idler.ChangeMorale(20+idler.CheckTrait("coward",20));


			int DoingsRoll = (Mathf.RoundToInt(Random.Range(0, 5)));

			bool DidSomething = false;


			switch (DoingsRoll)
			{
			case 0:
				string[] idlings = new string[] {
					"idled",
					"did nothing noteworthy",
					"stared into distance",
					"checked weapons",
					"maintained constant alert"
				};

				string idlingsInsert = idlings [(Mathf.RoundToInt (Random.value * (idlings.GetLength (0) - 1)))];
				idler.AddEvent (idlingsInsert + ".");
				idler.ChangeMorale (5);
				DidSomething = true;
				break;
			case 1:
				idler.AddEvent("Checked the fortifications. ");

				if (idler.HasHistory("-FORTIFIED-") && idler.HasAttribute("techie"))
				{
					idler.AddEvent ("Improved them to an excellent standard!");
					idler.AddHistory ("-HEAVYFORTIFIED-");
					idler.RemoveHistory ("-FORTIFIED-");
					idler.ChangeMorale (20);
				}
				else if (idler.HasHistory("-FORTIFIED-"))
				{
					idler.AddEvent ("They were just fine!!");
					idler.ChangeMorale (5);
				}
				else if (Random.Range(0,100) > (70 + idler.CheckTrait("young",-10) + idler.CheckTrait("techie",+20) + idler.CheckTrait("veteran",+20) + idler.CheckTrait("idiot",-50)))
				{
					idler.AddEvent ("Fixed the cover!");
					idler.AddHistory ("-FORTIFIED-");
					idler.ChangeMorale (10);
				}
				DidSomething = true;
				break;
			case 5:
				idler.AddEvent(" Listened to a propaganda broadcast - no news from our front.\n");
				if (Random.Range(0, 10) + idler.CheckTrait("heroic",2) > 5 )
				{
					idler.AddEvent("  It was inspiring!\n");
					idler.ChangeMorale(15);
				}
				else
				{
					idler.AddEvent("  It was boring..\n");
					idler.ChangeMorale(-10);
				}
				DidSomething = true;
				break;
			default:


				break;
			}

			idler.AddEvent ("\n"); 














		}
	
	
	}




}
