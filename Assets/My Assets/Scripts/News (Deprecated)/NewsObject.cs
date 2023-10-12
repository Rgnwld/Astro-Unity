using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsObject
{
    public string writeDate { get; set; }
    public string title { get; set; }
    public string description { get; set; }

    public NewsObject(string writeDate, string title, string description)
    {
        this.writeDate = writeDate;
        this.title = title;
        this.description = description;
    }

    public static implicit operator string(NewsObject news) => $"{news.writeDate},{news.title},{news.description}";

    public static implicit operator NewsObject(string line)
    {
        var data = line.Split(";");
        return new NewsObject(
            data[0],
            data[1],
            data[2]);
    }
}
