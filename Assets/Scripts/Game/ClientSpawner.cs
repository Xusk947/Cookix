using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    /// <summary>
    /// Delay between spawning new clients
    /// </summary>
    [SerializeField]
    private float _clientSpawnDelay = 15f;
    private float _timer;
    /// <summary>
    /// How many clients can spawn per time
    /// </summary>
    [Range(1, 3), SerializeField]
    private int _clientMaxSpawnCount = 1;
    /// <summary>
    /// Prefab of client to Spawn
    /// </summary>
    [SerializeField]
    private ClientController _client;
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0 && ClientController.CLIENTS.Count < 5)
        {
            _timer = _clientSpawnDelay;
            for(int i = 0; i < Random.Range(1, _clientMaxSpawnCount); i++)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        ClientController controller = Instantiate(_client);
        controller.Target = GameManager.Instance.clientEntter.transform.position;
        controller.transform.position = transform.position;
    }

    public static void InstantlySpawn()
    {
        ClientController controller = Instantiate(Content.Instance.Person);
        controller.Target = GameManager.Instance.clientEntter.transform.position;
        controller.transform.position = GameManager.Instance.clientEntter.transform.position;
    }
}
