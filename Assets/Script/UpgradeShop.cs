using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour {

	public Sprite defaultAmmo;
	public Sprite highDamage;
	public Sprite miniGun;
	public Sprite spread;
	public Sprite shield;
	public Sprite defaultDefense;
	public Sprite health;
	public Sprite gold;
	public Sprite defaultMisc;
	public Sprite speed;
	public Sprite AttSpeed;
    public Image slotAmmo;
	public Image slotDefense;
	public Image slotMisc;
	public MenuScene menu;
	public int index;
	public Text dAmmoText;
	public Text highDText;
	public Text minigunText;
	public Text spreadText;
	public Text dDefenseText;
	public Text shieldText;
	public Text healthText;
	public Text sDefenseText;
	public Text dMiscText;
	public Text goldText;
	public Text speedText;
	public Text sMiscText;
	public Transform texts;


	private void Update()
	{
		switch (SaveManager.Instance.state.activeUp [0]) {
		default:
		case 0:
			slotAmmo.sprite = defaultAmmo;
			break;
		case 1:
			slotAmmo.sprite = highDamage;
			break;
		case 2:
			slotAmmo.sprite = miniGun;
			break;
		case 3:
			slotAmmo.sprite = spread;
			break;

		}

		switch (SaveManager.Instance.state.activeUp [1]) {
		default:
		case 4:
			slotDefense.sprite = defaultDefense;
			break;
		case 5:
			slotDefense.sprite = shield;
			break;
		case 6:
			slotDefense.sprite = health;
			break;
		case 7:
			slotDefense.sprite = defaultDefense;
			break;

		}

		switch (SaveManager.Instance.state.activeUp [2]) {
		default:
		case 8:
			slotMisc.sprite = defaultMisc;

			break;
		case 9:
			slotMisc.sprite = speed;
			break;
		case 10:
			slotMisc.sprite = gold;
			break;
		case 11:
			slotMisc.sprite = AttSpeed;
			break;

		}
	}

	public void DefaultAmmo()
	{
		index = 0;
		menu.OnUpgradeSelect (2,index);
		UpdateText (index);
	}

	public void Damage()
	{
		index = 1;
		menu.OnUpgradeSelect (4,index);
		UpdateText (index);
	}

	public void MiniGun()
	{
		index = 2;
		menu.OnUpgradeSelect (3,index);
		UpdateText (index);
	}

	public void Spread()
	{
		index = 3;
		menu.OnUpgradeSelect (5,index);
		UpdateText (index);
	}

	public void DefaultDefense()
	{
		index = 4;
		menu.OnUpgradeSelect (8,index);
		UpdateText (index);
	}

	public void Shield()
	{
		index = 5;
		menu.OnUpgradeSelect (9,index);
		UpdateText (index);
	}

	public void HealthUp()
	{
		index = 6;
		menu.OnUpgradeSelect (10,index);
		UpdateText (index);
	}

	public void somethingDefense()
	{
		index = 7;
		menu.OnUpgradeSelect (11,index);
		UpdateText (index);
	}

	public void DefaultMisc()
	{
		index = 8;
		menu.OnUpgradeSelect (15,index);
		UpdateText (index);
	}

	public void Speed()
	{
		index = 9;
		menu.OnUpgradeSelect (14,index);
		UpdateText (index);
	}

	public void goldUp()
	{
		index = 10;
		menu.OnUpgradeSelect (16,index);
		UpdateText (index);
	}

	public void somethingMisc()
	{
		index =11;
		menu.OnUpgradeSelect (17,index);
		UpdateText (index);
	}

	private void UpdateText(int index)
	{int i = 0;
	foreach (Transform t in texts)
		{ 
			if(index == i)
				t.GetComponent<Text>().enabled = true;
			else 
				t.GetComponent<Text>().enabled = false;
			i++;
		}
	}
}

