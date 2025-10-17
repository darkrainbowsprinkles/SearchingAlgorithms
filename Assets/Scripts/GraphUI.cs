using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphUI : MonoBehaviour
{
    [SerializeField] NodeUI nodePrefab;
    [SerializeField] Transform nodesContainer;
    [SerializeField] Button startSearchButton;
    [SerializeField] Button clearObstaclesButton;
    [SerializeField] TMP_Dropdown searchTypeDropdown;
    Graph graph;

    void Awake()
    {
        graph = FindObjectOfType<Graph>();
    }

    void OnEnable()
    {
        startSearchButton.onClick.AddListener(StartSearch);
        clearObstaclesButton.onClick.AddListener(graph.ClearObstacles);
    }

    void OnDisable()
    {
        startSearchButton.onClick.RemoveListener(StartSearch);
        clearObstaclesButton.onClick.RemoveListener(graph.ClearObstacles);
    }

    void Start()
    {
        foreach (Node node in graph.GetNodes())
        {
            NodeUI nodeInstance = Instantiate(nodePrefab, nodesContainer);
            nodeInstance.SetNode(node);
        }

        string[] searchTypes = Enum.GetNames(typeof(SearchType));

        searchTypeDropdown.ClearOptions();
        searchTypeDropdown.AddOptions(searchTypes.ToList());
    }

    void StartSearch()
    {
        int selectedIndex = searchTypeDropdown.value;
        SearchType searchType = (SearchType)selectedIndex;
        graph.StartSearch(searchType);
    }
}
