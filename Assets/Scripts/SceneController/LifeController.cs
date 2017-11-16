using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [SerializeField] private Text _textLife;
    [SerializeField] private Text _textGameover;
    private int _damage = 0;
    private bool _alive = true;
    public int health = 3;

    private void Start()
    {
        _textLife.text = "";
        _textGameover.text = "";
    }

    private void Update()
    {
        _alive = (_damage < health) ? true : false;
    }

    private IEnumerator DelayTakeDamage()
    {
        yield return new WaitForSeconds(0.25f);
        _damage++;
        SetLifeText();
    }

    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(0.25f);
        _textLife.text = "";
        _textGameover.text = "Game Over";
    }

    public void SetLifeText()
    {
        string temp = "";
        for (int i = 0; i < _damage; i++)
        {
            temp += "X ";
        }
        if (temp.EndsWith(" ")) temp = temp.Substring(0, temp.Length - 1);
        _textLife.text = temp;
    }

    public void TakeDamage()
    {
        StartCoroutine(DelayTakeDamage());
    }

    public void GameOver()
    {
        StartCoroutine(DelayGameOver());
    }

    public bool Alive()
    {
        return _alive;
    }
}
