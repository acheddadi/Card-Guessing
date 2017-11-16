using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatcher : MonoBehaviour
{
    private InputController _input;
    private CardController[] _pair = new CardController[2];
    private LifeController _lifeBar;
    private int _lastClick;

	private void Start()
	{
        _input = GetComponent<InputController>();
        _lifeBar = GetComponent<LifeController>();
	}
	
	private void Update()
	{
        int click = _input.GetClicks();
		if (click != _lastClick)
        {
            _lastClick = click;
            CardController card = _input.GetCard();
            switch (click)
            {
                case 0:
                    _pair[click] = card;
                    break;
                case 1:
                    _pair[click] = card;
                    CheckMatch();
                    break;
            }
        }
	}

    private void CheckMatch()
    {
        if (_pair[0].GetMaterial() == _pair[1].GetMaterial())
        {
            Debug.Log("It's a match!");
            StartCoroutine(_pair[0].GoodGuess());
            StartCoroutine(_pair[1].GoodGuess());
        }

        else
        {
            Debug.Log("Wrong guess!");
            StartCoroutine(_pair[0].WrongGuess());
            StartCoroutine(_pair[1].WrongGuess());
            _lifeBar.TakeDamage();
        }
        _pair = new CardController[2];
    }
}
