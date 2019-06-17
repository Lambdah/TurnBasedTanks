using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankDamageOverlay : MonoBehaviour {
    public GameObject m_damageText;
    public Text m_dam_text;
    public Color m_text_color;
    [HideInInspector]public UIFollowTarget target;
	// Use this for initialization
	void Awake () {
        GameObject DamageText = Instantiate(m_damageText);
        target = DamageText.GetComponent<UIFollowTarget>();
        DamageText.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasOverlay").transform);
        m_dam_text = DamageText.GetComponent<Text>();
	}

    private void Start()
    {
        SetDamageText("");
    }

    public void setFollowTarget(Transform trans)
    {
        target.follow = trans;
    }

    public void ShowDamage(int damage)
    {
        string tex = "<color=#" + ColorUtility.ToHtmlStringRGB(m_text_color) + ">" + damage + "</color>";
        SetDamageText(tex);
    }

    private void SetDamageText(string str_dam)
    {
        m_dam_text.text = str_dam;
        StartCoroutine(RevertDamageText());
    }

    IEnumerator RevertDamageText()
    {
        yield return new WaitForSeconds(0.75f);
        m_dam_text.text = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
