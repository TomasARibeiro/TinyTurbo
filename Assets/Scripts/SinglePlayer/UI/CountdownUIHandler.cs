using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownUIHandler : MonoBehaviour
{
    public TextMeshProUGUI CountDownText;

	private void Awake()
	{
        CountDownText.text = "";
	}

	private void OnEnable()
	{
        GameManager.E_CountDownBegin += HandleCountDownBegin;
	}

	private void OnDisable()
	{
		GameManager.E_CountDownBegin -= HandleCountDownBegin;
	}

	IEnumerator CountDownCO()
    {
        yield return new WaitForSeconds(0.3f);
        int counter = 3;

        while (true)
        {
            if (counter != 0)
                CountDownText.text = counter.ToString();
            else
            {
                CountDownText.text = "GO!";
                GameManager.Instance.OnRaceStart();
                break;
            }

            counter--;
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(0.5f);

        CountDownText.text = "";
        gameObject.SetActive(false);
    }

    private void HandleCountDownBegin()
    {
        StartCoroutine(CountDownCO());
    }
}
