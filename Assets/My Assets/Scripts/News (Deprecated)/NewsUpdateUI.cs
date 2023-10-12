using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NewsUpdateUI : MonoBehaviour
{
    public List<NewsObject> newsObjects = new List<NewsObject>();
    public GameObject newsPrefab;
    public GameObject contentReference;

    private void Start()
    {
        var data = File.ReadLines("Assets/news.csv");
        foreach (var line in data)
        {
            newsObjects.Add(line);
        }

        ApplyNews();
    }

    private void ApplyNews()
    {
        foreach (var line in newsObjects)
        {
            NewsUIItem newItem = Instantiate(newsPrefab, contentReference.transform).GetComponent<NewsUIItem>();
            newItem.title.text = line.title;
            newItem.description.text = line.description;

        }
    }
}


