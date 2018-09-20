﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Dijkstra
    {
        public List<Node> listOfNodes = new List<Node>();
        

        public void FillList()
        {
            //Main nodes.
            listOfNodes.Add(new Node("A", 28, 0, 13.5));
            listOfNodes.Add(new Node("B", 28, 0, 8));
            listOfNodes.Add(new Node("C", 17.5, 0, 8));
            listOfNodes.Add(new Node("D", 12.5, 0, 8));
            listOfNodes.Add(new Node("E", 7.5, 0, 8));
            listOfNodes.Add(new Node("F", 7.5, 0, 22.5));
            listOfNodes.Add(new Node("G", 12.5, 0, 22.5));
            listOfNodes.Add(new Node("H", 17.5, 0, 22.5));
            listOfNodes.Add(new Node("I", 28, 0, 22.5));
            listOfNodes.Add(new Node("J", 28, 0, 18));


            //Stellage nodes.
            listOfNodes.Add(new Node("C1", 15, 0, 10));
            listOfNodes.Add(new Node("C2", 15, 0, 11.5));
            listOfNodes.Add(new Node("C3", 15, 0, 13));
            listOfNodes.Add(new Node("C4", 15, 0, 14.5));

            listOfNodes.Add(new Node("D1", 10, 0, 10));
            listOfNodes.Add(new Node("D2", 10, 0, 11.5));
            listOfNodes.Add(new Node("D3", 10, 0, 13));
            listOfNodes.Add(new Node("D4", 10, 0, 14.5));

            listOfNodes.Add(new Node("E1", 5, 0, 10));
            listOfNodes.Add(new Node("E2", 5, 0, 11.5));
            listOfNodes.Add(new Node("E3", 5, 0, 13));
            listOfNodes.Add(new Node("E4", 5, 0, 14.5));


            //Pad tussen de stellage nodes.
            listOfNodes.Add(new Node("C11", 17.5, 0, 10));
            listOfNodes.Add(new Node("C21", 17.5, 0, 11.5));
            listOfNodes.Add(new Node("C31", 17.5, 0, 13));
            listOfNodes.Add(new Node("C41", 17.5, 0, 14.5));

            listOfNodes.Add(new Node("D11", 12.5, 0, 10));
            listOfNodes.Add(new Node("D21", 12.5, 0, 11.5));
            listOfNodes.Add(new Node("D31", 12.5, 0, 13));
            listOfNodes.Add(new Node("D41", 12.5, 0, 14.5));

            listOfNodes.Add(new Node("E11", 7.5, 0, 10));
            listOfNodes.Add(new Node("E21", 7.5, 0, 11.5));
            listOfNodes.Add(new Node("E31", 7.5, 0, 13));
            listOfNodes.Add(new Node("E41", 7.5, 0, 14.5));

        }
    }
}
