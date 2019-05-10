﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int minimum; // minimum amount for Count
        public int maximum; // maximum amount for Count

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }

    }

    public Graph graph;
    public int columns = 8; // columns in game board
    public int rows = 8; // rows in game board
    public GameObject floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;
    

    //Clears grid list and creates a new board
    void InitializeList()
    {
        // Graph script instantiates each node at the origin
        // and assings the neighbors
        graph.ClearGraph();
        graph.CreateGraph();
    }

    //Sets up outer wall and the floor of the game board
    void BoardSetup()
    {
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Node node = graph.graph[i, j];
                node.tile.transform.Translate(i*2, 0, j*2);
                node.tile.SetActive(true);
               
            }
        }
    }

    public void Awake()
    {
        graph = GetComponent<Graph>();
        
    }

    private void Start()
    {
        InitializeList();
        BoardSetup();
    }
   
}