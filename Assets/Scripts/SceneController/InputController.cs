using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private CardController _hitCard;
    private int _clickCount = -1;

	private void Start()
	{
		
	}
	
	private void Update()
	{
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject hitObject = hitInfo.transform.gameObject;
                _hitCard = hitObject.GetComponent<CardController>();
                if ((_hitCard != null) && _hitCard.IsClickable())
                {
                    CountClicks();
                    _hitCard.Select();
                    Debug.Log("You clicked on a card!");
                    Debug.Log("Click number " + GetClicks());
                }
            }
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    private void CountClicks()
    {
        if (_clickCount < 1)
        {
            _clickCount++;
        }
        else
        {
            _clickCount = 0;
        }
    }

    public int GetClicks()
    {
        return _clickCount;
    }

    public CardController GetCard()
    {
        return _hitCard;
    }
}
