using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericSingleton<PlayerManager>
{
    [SerializeField] private PlayerController[] playerPrefabs;
    [SerializeField] private int chosenPrefab;

    private PlayerController _player;


    public PlayerController Player => _player;

    protected override void Awake()
    {
        base.Awake();
        _player = CreatePlayer(chosenPrefab);
    }

    private PlayerController CreatePlayer(int index)
    {
        index = Mathf.Min(index, playerPrefabs.Length);
        return Instantiate(playerPrefabs[index]);
    }
}