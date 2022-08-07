using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetMoneyUI : MonoBehaviour
{
	
	private TextMeshProUGUI moneyText;
	private float fadeOutSpeed = 1f;
	void Start()
	{
		moneyText = GetComponent<TextMeshProUGUI>();
		moneyText.text = $"+{StageState.Instance.earnedMoneyTable[StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength]]}";
	}

	void LateUpdate()
	{

		moneyText.color = Color.Lerp(moneyText.color, new Color(1f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime);
		
		if (moneyText.color.a <= 0.1f)
		{
			Destroy(gameObject);
		}
	}
}
