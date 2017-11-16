using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{

    [SerializeField] private Transform _cardParent;
    [SerializeField] private GameObject _cardPrefab;
    private int _cardCount = -1;
    private Vector3 _firstPos;

    public int rows, columns, colourAmount = 3;
    public float padding = 2;

    private void Start()
    {

    }

    private void Update()
	{
        _cardCount = _cardParent.childCount;
	}

    public void Instantiate()
    {
        if (((rows * columns) % (colourAmount * 2)) != 0) Debug.LogError("CardSpawner : Not enough cards to form colour pairs evenly.");
        else
        {
            GameObject[] card = new GameObject[rows * columns];
            Vector3[] position = new Vector3[rows * columns];
            float offsetX = ((float)columns - 1) / 2, offsetZ = ((float)rows - 1) / 2;

            // Calculate positions for each card
            for (int i = 0, k = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    position[k] = new Vector3((i - offsetX) * padding, 1, (j - offsetZ) * padding);
                    k++;
                }
            }

            // Save lowest position

            _firstPos = position[0];

            // Shuffle positions
            for (int i = 0; i < 100; i++)
            {
                int first = Random.Range(0, position.Length - 1);
                int second = Random.Range(0, position.Length - 1);

                var temp = position[first];
                position[first] = position[second];
                position[second] = temp;
            }

            // Instantiate cards and set colours
            for (int i = 0, j = 1; i < card.Length; i++)
            {
                card[i] = Instantiate(_cardPrefab, position[i], Quaternion.identity);
                CardController cardInfo = card[i].GetComponent<CardController>();
                cardInfo.transform.parent = _cardParent;

                cardInfo.SetMaterial(j);
                if (j < colourAmount) j++;
                else j = 1;
            }
        }
    }

    public int CardCount()
    {
        return _cardCount;
    }

    public Vector3 GetPosition()
    {
        return _firstPos;
    }

}
