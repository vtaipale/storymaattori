using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//VT. Handles head icons showing up.

public class SoldierHeadImage : MonoBehaviour {

	public SoldierController target;

	public char Sex = 'm';
	public int ImageNumber = 0;

	// There is BG but it is static
	public Image HeadImage;
	public Image HairImage;
	public Image RoboVisionImage;
	public Image ArmorImage;
	public Image ScratchesImage;

	public Sprite Helm_Open;
	public Sprite Helm_Closed;
	public Sprite Helm_Noob;
		
	public Sprite RoboEye;
	public Sprite RoboVision;

	public Sprite Scratch1;
	public Sprite Scratch2;

	// MALE HEADS
	public Sprite M1;
	public Sprite M2;
	public Sprite M3;
	public Sprite M4;
	public Sprite M5;

	// FEMALE HEADS
	public Sprite F1;
	public Sprite F2;
	public Sprite F3;
	public Sprite F4;
	public Sprite F5;
	
	// MALE HAIRs
	public Sprite MH1;
	public Sprite MH2;
	public Sprite MH3;
	public Sprite MH4;
	public Sprite MH5;

	// FEMALE HAIRs
	public Sprite FH1;
	public Sprite FH2;
	public Sprite FH3;
	public Sprite FH4;
	public Sprite FH5;

	// HAIR REPLACING BARCODES
	public Sprite BAR1;
	public Sprite BAR2;
	public Sprite BAR3;
	public Sprite BAR4;
	public Sprite BAR5;

	public void Set(SoldierController solttu)
	{
		this.target = solttu;
		this.Sex = solttu.sex;
		this.ImageNumber = solttu.pictureID;
		this.checkPicture();

	}


	// Use this for initialization
	void Start () {
		ArmorImage.overrideSprite = Helm_Closed;
		this.checkPicture();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void checkPicture()
	{
		if (ImageNumber == 0 | target.HasAttribute("newbie"))
		{
			ArmorImage.overrideSprite = Helm_Closed;
		}

		else 
		{
			ArmorImage.overrideSprite = Helm_Open;

			if (Sex == 'm')
			{
				if (ImageNumber == 1)
				{
					HeadImage.overrideSprite = M1;
				}
				else if (ImageNumber == 2)
				{
					HeadImage.overrideSprite = M2;
				}
				else if (ImageNumber == 3)
				{
					HeadImage.overrideSprite = M3;
				}
				else if (ImageNumber == 4)
				{
					HeadImage.overrideSprite = M4;
				}
				else if (ImageNumber == 5)
				{
					HeadImage.overrideSprite = M5;
				}
				
			}
			else if (Sex == 'f')
			{
				if (ImageNumber == 1)
				{
					HeadImage.overrideSprite = F1;
				}
				else if (ImageNumber == 2)
				{
					HeadImage.overrideSprite = F2;
				}
				else if (ImageNumber == 3)
				{
					HeadImage.overrideSprite = F3;
				}
				else if (ImageNumber == 4)
				{
					HeadImage.overrideSprite = F4;
				}
				else if (ImageNumber == 5)
				{
					HeadImage.overrideSprite = F5;
				}
				
			}
					//HAIR
			if (Sex == 'm')
			{
				if (target.hairID == 1)
				{
					HairImage.overrideSprite = MH1;
				}
				else if (target.hairID == 2)
				{
					HairImage.overrideSprite = MH2;
				}
				else if (target.hairID == 3)
				{
					HairImage.overrideSprite = MH3;
				}
				else if (target.hairID == 4)
				{
					HairImage.overrideSprite = MH4;
				}
				else if (target.hairID == 5)
				{
					HairImage.overrideSprite = MH5;
				}
				else if (target.hairID == 6)
				{
					HairImage.overrideSprite = BAR1;
				}
				else if (target.hairID == 7)
				{
					HairImage.overrideSprite = BAR2;
				}
				else if (target.hairID == 8)
				{
					HairImage.overrideSprite = BAR3;
				}
				else if (target.hairID == 9)
				{
					HairImage.overrideSprite = BAR4;
				}
				else if (target.hairID == 10)
				{
					HairImage.overrideSprite = BAR5;
				}
				
			}
			else if (Sex == 'f')
			{
				if (target.hairID == 1)
				{
					HairImage.overrideSprite = FH1;
				}
				else if (target.hairID == 2)
				{
					HairImage.overrideSprite = FH2;
				}
				else if (target.hairID == 3)
				{
					HairImage.overrideSprite = FH3;
				}
				else if (target.hairID == 4)
				{
					HairImage.overrideSprite = FH4;
				}
				else if (target.hairID == 5)
				{
					HairImage.overrideSprite = FH5;
				}
				else if (target.hairID == 6)
				{
					HairImage.overrideSprite = BAR1;
				}
				else if (target.hairID == 7)
				{
					HairImage.overrideSprite = BAR2;
				}
				else if (target.hairID == 8)
				{
					HairImage.overrideSprite = BAR3;
				}
				else if (target.hairID == 9)
				{
					HairImage.overrideSprite = BAR4;
				}
				else if (target.hairID == 10)
				{
					HairImage.overrideSprite = BAR5;
				}
				
			}

			if (target.HasAttribute("robo-eye"))
			{
				RoboVisionImage.enabled = true;
				RoboVisionImage.overrideSprite = RoboEye;
			}
			else if (target.HasAttribute("robo-vision"))
			{
				RoboVisionImage.enabled = true;
				RoboVisionImage.overrideSprite = RoboVision;
			}
			else 
			{
				RoboVisionImage.enabled = false;
			}
		}

		if (target.HasAttribute("newbie"))	// Armor of newbies shine a lot!!
		{
			ScratchesImage.enabled = true;
			ScratchesImage.overrideSprite = Helm_Noob;
		}
		else if (target.rank > 0)
		{
			ScratchesImage.enabled = true;
			ScratchesImage.overrideSprite = Scratch1;
		}
		else if (target.rank > 3)
		{
			ScratchesImage.enabled = true;
			ScratchesImage.overrideSprite = Scratch2;
		}
		else 
		{
			ScratchesImage.enabled = false;
		}
	}
}
