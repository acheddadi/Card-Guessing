using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01 : MonoBehaviour
{
    [SerializeField] private GameObject _spawnerPrefab;
    [SerializeField] private CameraController _camControl;
    [SerializeField] private LifeController _lifeControl;
    private CardSpawner _currentSpawn;
    private int _phase = 0;
    private bool _spawning = true;
    private bool _loading = true;

	private void Start()
	{
        StartCoroutine(WaitForLoad());
	}
	
	private void Update()
	{
        if (!_loading)
        {
            if (_spawning)
            {
                GameObject spawnerObj = Instantiate(_spawnerPrefab, transform.position, Quaternion.identity);
                _currentSpawn = spawnerObj.GetComponent<CardSpawner>();

                switch (_phase)
                {
                    case 0:
                        _currentSpawn.rows = 2; _currentSpawn.columns = 4; _currentSpawn.colourAmount = 2;
                        break;
                    case 1:
                        _currentSpawn.rows = 3; _currentSpawn.columns = 4; _currentSpawn.colourAmount = 2;
                        break;
                    case 2:
                        _currentSpawn.rows = 4; _currentSpawn.columns = 4; _currentSpawn.colourAmount = 2;
                        break;
                    case 3:
                        _currentSpawn.rows = 5; _currentSpawn.columns = 4; _currentSpawn.colourAmount = 2;
                        break;
                    case 4:
                        _currentSpawn.rows = 4; _currentSpawn.columns = 6;
                        break;
                }

                _currentSpawn.Instantiate();
                _camControl.SetObjectPosition(_currentSpawn.GetPosition());
                _camControl.SetFov();
                _phase++;
                _spawning = false;
            }

            if ((_currentSpawn.CardCount() == 0) && (_currentSpawn != null))
            {
                Destroy(_currentSpawn.gameObject);
                _spawning = true;
            }

            if ((_currentSpawn.CardCount() == 0) && (_currentSpawn != null) && (_phase == 5)) EndLevel();

            if (!_lifeControl.Alive()) EndLevel();
        }
	}

    private IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(1);
        _loading = false;
    }

    public void EndLevel()
    {
        if (_currentSpawn.CardCount() > 0)
        {
            CardController[] temp = _currentSpawn.GetComponentsInChildren<CardController>();
            for (int i = 0; i < temp.Length; i++)
            {
                StartCoroutine(temp[i].GoodGuess());
            }
        }
        _loading = true;
        _lifeControl.GameOver();
    }
}
