using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private Material _material;
    private Renderer _renderer;
    [SerializeField] private Material[] _materials;
    private bool _clickable = false;

    public float fadeSpeed = 2.0f;
    public float previewDelay = 2.0f;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color =
                new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, 0);
        StartCoroutine(PreviewCard(previewDelay));
    }

    private void Update()
    {

    }

    private IEnumerator PreviewCard(float delay)
    {
        
        yield return StartCoroutine(Opacity("in"));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(Fade("in"));
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(Fade("out"));
        _clickable = true;
    }

    private IEnumerator RemoveCard()
    {
        yield return StartCoroutine(Opacity("out"));
        Destroy(gameObject);
    }

    private IEnumerator Fade(string direction)
    {
        float change = 0.0f;
        while (change < 1.0f)
        {
            change += fadeSpeed * Time.deltaTime;
            switch (direction)
            {
                case "in":
                    _renderer.material.Lerp(_materials[0], _material, change);
                    break;
                case "out":
                    _renderer.material.Lerp(_material, _materials[0], change);
                    break;
                default:
                    Debug.LogError("CardController : Invalid argument passed to Fade() method.");
                    change = 1.0f;
                    break;
            }
            yield return null;
        }
    }

    private IEnumerator Opacity(string direction)
    {
        float change;
        switch (direction)
        {
            case "in":
                change = 0.0f;
                while (change < 1.0f)
                {
                    change += fadeSpeed * Time.deltaTime;
                    _renderer.material.color =
                        new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, change);
                    yield return null;
                }
                break;
            case "out":
                change = 1.0f;
                while (change > 0.0f)
                {
                    change -= fadeSpeed * Time.deltaTime;
                    _renderer.material.color =
                        new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, change);
                    yield return null;
                }
                break;
        }
        
    }

    public void Select()
    {
        _clickable = false;
        StartCoroutine(Fade("in"));
    }

    public void SetMaterial(int material)
    {
        if (material < _materials.Length)
        {
            _material = _materials[material];
        }
        else Debug.Log("CardController : Illegal argument passed to SetMaterial().");
    }

    public Material GetMaterial()
    {
        return _material;
    }

    public IEnumerator GoodGuess()
    {
        yield return new WaitForSeconds(1.5f);
        StopAllCoroutines();
        StartCoroutine(RemoveCard());
    }

    public IEnumerator WrongGuess()
    {
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(Fade("out"));
        _clickable = true;
    }

    public bool IsClickable()
    {
        return _clickable;
    }
}
