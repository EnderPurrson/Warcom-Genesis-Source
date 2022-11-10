using System.Collections.Generic;
using UnityEngine;

public class UtilUIShopColdWeapons : MonoBehaviour
{
	public TUITexture m_TUITextureDynamic;

	public TUITexture m_TUITextureDynamicLow;

	public WeaponType type;

	public string coldWeaponName;

	private int m_pageCount;

	private TUIScroll m_scroll;

	private List<string> m_iconNameList = new List<string>();

	private List<TUIMeshSprite> m_iconMeshList = new List<TUIMeshSprite>();

	private bool m_loadPic;

	public void Start()
	{
		GameObject gameObject = base.transform.Find("ScrollObject").Find("ItemPrefab").gameObject;
		List<WeaponInfo> coldWeaponByCharacter = DataCenter.Conf().GetColdWeaponByCharacter(coldWeaponName);
		int count = coldWeaponByCharacter.Count;
		for (int i = 0; i < count; i++)
		{
			WeaponInfo weaponInfo = coldWeaponByCharacter[i];
			GameObject gameObject2 = Object.Instantiate(gameObject) as GameObject;
			gameObject2.name = weaponInfo.name;
			gameObject2.transform.parent = gameObject.transform.parent;
			gameObject2.transform.localPosition = new Vector3(-102 + i % 2 * 65, 66 - i / 2 * 65, 0f);
			if (i == 0)
			{
				TUIButtonSelect component = gameObject2.transform.Find("ItemBtn").GetComponent<TUIButtonSelect>();
				component.SetSelected(true);
			}
			TUIMeshSprite component2 = gameObject2.transform.Find("Price").GetComponent<TUIMeshSprite>();
			TUIMeshText component3 = gameObject2.transform.Find("PriceText").GetComponent<TUIMeshText>();
			if (weaponInfo.buyMoney == -1)
			{
				component2.frameName = "StatusBarCrystalImg";
				component2.UpdateMesh();
				component3.text = weaponInfo.buyCrystal.ToString("###,###");
				component3.UpdateMesh();
				gameObject2.transform.Find("Sale").gameObject.SetActiveRecursively(true);
			}
			else
			{
				component2.frameName = "StatusBarMoneyImg";
				component2.UpdateMesh();
				component3.text = weaponInfo.buyMoney.ToString("###,###");
				component3.UpdateMesh();
				gameObject2.transform.Find("Sale").gameObject.SetActiveRecursively(false);
			}
			TUIMeshSprite component4 = gameObject2.transform.Find("Icon").GetComponent<TUIMeshSprite>();
			component4.frameName = weaponInfo.icon;
			m_iconNameList.Add(component4.frameName);
			m_iconMeshList.Add(component4);
			GameObject gameObject3 = gameObject2.transform.Find("Locked").gameObject;
			if (DataCenter.Conf().GetWeaponInfoByName(weaponInfo.name).unlockLevel > DataCenter.Save().GetLv())
			{
				gameObject3.SetActiveRecursively(true);
				component4.color = new Color(0.4f, 0.4f, 0.4f, 1f);
			}
			else
			{
				gameObject3.SetActiveRecursively(false);
			}
			component4.UpdateMesh();
		}
		Object.Destroy(gameObject);
		m_pageCount = (count + 1) / 2;
		m_scroll = base.transform.Find("Scroll").gameObject.GetComponent<TUIScroll>();
		m_scroll.rangeYMin = 0f;
		m_scroll.borderYMin = m_scroll.rangeYMin - 240f;
		m_scroll.rangeYMax = m_pageCount * 60;
		m_scroll.borderYMax = m_scroll.rangeYMax + 240f;
		m_scroll.pageY = new float[m_pageCount];
		for (int j = 2; j < m_pageCount; j++)
		{
			m_scroll.pageY[j] = (m_pageCount - 1 - j) * 60;
		}
	}

	public void LoadTexture()
	{
		for (int i = 0; i < m_iconNameList.Count; i++)
		{
			m_TUITextureDynamic.LoadFrame(m_iconNameList[i]);
		}
		for (int j = 0; j < m_iconMeshList.Count; j++)
		{
			m_iconMeshList[j].UpdateMesh();
		}
	}

	private void Update()
	{
		if (!m_loadPic && base.transform.position == Vector3.zero)
		{
			LoadTexture();
			m_loadPic = true;
		}
	}
}
