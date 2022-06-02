using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCardGenerator : MonoBehaviour
{
    private const string MINIGAMES_FOLDER_ROUTE = "Mini-Games";
    [SerializeField] private MiniGameCard _cardPrefab;
    [SerializeField] private Transform _container;
    
    private List<MiniGameScriptableObject> _minigames = new List<MiniGameScriptableObject>();

    private void Start()
    {
        GenerateCards();
    }

    private void GenerateCards()
    {
        _minigames.Clear();
        var tempMinigames = Resources.LoadAll<MiniGameScriptableObject>(MINIGAMES_FOLDER_ROUTE);
        for (int i = 0; i < tempMinigames.Length; i++)
        {
            _minigames.Add(tempMinigames[i]);
            MiniGameCard card = Instantiate(_cardPrefab, _container);
            card.Initialize(tempMinigames[i]);
        }

        Resources.UnloadUnusedAssets();
    }
}
