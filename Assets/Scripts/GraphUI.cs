using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] TMP_Text visitedText;
    [SerializeField] TMP_Text pathText;
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
        List<Node> path = graph.StartSearch(searchType);
        PrintNodes(graph.GetVisited(), ",", visitedText);
        PrintNodes(path, "->", pathText);
    }

    void PrintNodes(IEnumerable<Node> nodes, string separator, TMP_Text uiText)
    {
        uiText.text = "";
        uiText.text += "[";

        Node[] nodesArray = nodes.ToArray();

        for (int i = 0; i < nodesArray.Length; i++)
        {
            uiText.text += nodesArray[i].GetCoordinates();

            if (i < nodesArray.Length - 1)
            {
                uiText.text += separator;
            }
        }
        
        uiText.text += "]";
    }
}
