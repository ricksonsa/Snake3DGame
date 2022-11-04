using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayControllerScript : MonoBehaviour
{
    public static GameplayControllerScript instance;

    public GameObject FruitPickUp, BombPickUp;

    private Text _scoreText;
    private int _scoreCount;

    private float _minX = -4.25f, _maxX = 4.25f, _minY = -2.26f, _maxY = 2.26f;
    private float _zPos = 5.8f;

    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        _scoreText = GameObject.Find("Score").GetComponent<Text>();
        Invoke(nameof(StartSpawning), 0.5f);
    }

    private void MakeInstance()
    {
        if (instance == null) instance = this;
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnPickups());
    }

    void StopSpawning()
    {
        CancelInvoke(nameof(StartSpawning));
    }

    IEnumerator SpawnPickups()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 3f));

        if(Random.Range(0, 10) >= 2)
        {
            Instantiate(FruitPickUp, new Vector3(Random.Range(_minX, _maxX),
                                                Random.Range(_minY, _maxY), _zPos), 
                                                Quaternion.identity);
        }
        else
        {
            Instantiate(BombPickUp, new Vector3(Random.Range(_minX, _maxX),
                                               Random.Range(_minY, _maxY), _zPos),
                                               Quaternion.identity);
        }

        Invoke(nameof(StartSpawning), 0f);
    }

    public void IncreaseScore()
    {
        _scoreCount++;
        _scoreText.text = $"Score: {_scoreCount}";
    }
}
