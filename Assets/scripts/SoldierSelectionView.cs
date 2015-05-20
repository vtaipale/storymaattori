﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SoldierSelectionView : MonoBehaviour {

	public MissionLog log;



	void Update () {
		ShowSoldierAmount ();
	}

	public Text textField;
	public SoldierManager manager;



	public void ShowSoldierAmount()
	{
		textField.text =   manager.inSquadCurrently.ToString();
	}

}
