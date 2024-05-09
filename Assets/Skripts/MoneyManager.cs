using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public int money;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    public TextMeshProUGUI textMoney;

    private AudioSource _moneySource;

    private void Awake()
    {
        _moneySource = GetComponent<AudioSource>();

        money = PlayerPrefs.GetInt("Money");
        textMoney.text = money.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_moneySource)
            AudioSource.PlayClipAtPoint(_moneySource.clip, gameObject.transform.position, _moneySource.volume);
        money++;
        textMoney.text = money.ToString();
        SavingDate();
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    void SavingDate()
    {
        PlayerPrefs.SetInt("Money", money);
    }
}
