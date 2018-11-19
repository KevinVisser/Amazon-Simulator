using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MoreLinq;

namespace PathFinding
{
    /// <summary>
    /// Pathfinding class
    /// </summary>
    public class Pathfinding
    {
        /// <summary>
        /// Lijst met alle nodes
        /// </summary>
        public static List<Node> listOfNodes = new List<Node>();

        /// <summary>
        /// Pad voor de robot
        /// </summary>
        public static List<Node> PathRobot = new List<Node>();
        /// <summary>
        /// Startnode voor de robot
        /// </summary>
        public static List<Node> StartRobot = new List<Node>();

        /// <summary>
        /// Het pad voor de truck
        /// </summary>
        public static List<Node> PathTruck = new List<Node>();
        /// <summary>
        /// Startnode voor de truck 1
        /// </summary>
        public static List<Node> StartTruck1 = new List<Node>();
        /// <summary>
        /// Startnode voor de truck 2
        /// </summary>
        public static List<Node> StartTruck2 = new List<Node>();
        /// <summary>
        /// Startnode voor de truck 3
        /// </summary>
        public static List<Node> StartTruck3 = new List<Node>();

        /// <summary>
        /// vul de lijsten met alle nodes
        /// </summary>
        public static void FillList()
        {
            StartRobot.Add(new Node("A", 28, 0, 13.5, "B"));
            StartTruck1.Add(new Node("X1", 33, 0.23, 60, "Y1"));
            StartTruck2.Add(new Node("X2", 35, 0.23, 60, "Y2"));
            StartTruck3.Add(new Node("X3", 37, 0.23, 60, "Y3"));

            listOfNodes.Add(new Node("A", 28, 0, 13.5, "B"));
            listOfNodes.Add(new Node("B", 28, 0, 8, "C"));
            listOfNodes.Add(new Node("C", 17.5, 0, 8, "C1", "D"));
            listOfNodes.Add(new Node("D", 12.5, 0, 8, "D1", "E"));
            listOfNodes.Add(new Node("E", 7.5, 0, 8, "E1"));

            listOfNodes.Add(new Node("C1", 15, 0, 10, "C2"));
            listOfNodes.Add(new Node("C2", 15, 0, 11.5, "C3"));
            listOfNodes.Add(new Node("C3", 15, 0, 13, "C4"));
            listOfNodes.Add(new Node("C4", 15, 0, 14.5, "H"));

            listOfNodes.Add(new Node("D1", 10, 0, 10, "D2"));
            listOfNodes.Add(new Node("D2", 10, 0, 11.5, "D3"));
            listOfNodes.Add(new Node("D3", 10, 0, 13, "D4"));
            listOfNodes.Add(new Node("D4", 10, 0, 14.5, "G"));

            listOfNodes.Add(new Node("E1", 5, 0, 10, "E2"));
            listOfNodes.Add(new Node("E2", 5, 0, 11.5, "E3"));
            listOfNodes.Add(new Node("E3", 5, 0, 13, "E4"));
            listOfNodes.Add(new Node("E4", 5, 0, 14.5, "F"));

            listOfNodes.Add(new Node("F", 5, 0, 22.5, "E4", "G"));
            listOfNodes.Add(new Node("G", 10, 0, 22.5, "D4", "H"));
            listOfNodes.Add(new Node("H", 15, 0, 22.5, "C4", "I"));
            listOfNodes.Add(new Node("I", 28, 0, 22.5, "J"));
            listOfNodes.Add(new Node("J", 28, 0, 17, "A"));

            listOfNodes.Add(new Node("X1", 33, 0.23, 60, "Y1"));
            listOfNodes.Add(new Node("Y1", 33, 0.23, 15, "X1", "Z1"));
            listOfNodes.Add(new Node("Z1", 33, 0.23, -30, "Y1"));

            listOfNodes.Add(new Node("X2", 35, 0.23, 60, "Y2"));
            listOfNodes.Add(new Node("Y2", 35, 0.23, 15, "X2", "Z2"));
            listOfNodes.Add(new Node("Z2", 35, 0.23, -30, "Y2"));

            listOfNodes.Add(new Node("X3", 37, 0.23, 60, "Y3"));
            listOfNodes.Add(new Node("Y3", 37, 0.23, 15, "X3", "Z3"));
            listOfNodes.Add(new Node("Z3", 37, 0.23, -30, "Y3"));
        }
        /// <summary>
        /// geeft de lijst met nodes terug die de robot/truck moet bewandelen
        /// </summary>
        /// <param name="start">startnode van object</param>
        /// <param name="end">eindnode van object</param>
        /// <param name="nodes">de lijst moet alle nodes</param>
        /// <param name="path">in welke lijst je het wil stoppen</param>
        /// <returns></returns>
        public static List<Node> Listnodes(string start, string end, List<Node> nodes, List<Node> path)
        {
            path.Clear();
            Node CurrentNode;
            string currentNode;
            //RECURSIEFANBOYs
            for (int i = 0; i < nodes.Count(); i++)
            {
                currentNode = nodes[i].GetName();
                CurrentNode = nodes[i];

                //Termination condition
                if (start == end)
                {
                    break;
                }

                //Sla de stellage nodes over die je niet nodig hebt --> end node == E4? sla dan alles met c1-4 en d1-4 over.
                if (currentNode.Length > 1 && currentNode[0] != start[0] && start != "A")
                {
                    continue;
                }

                //als de currentNode een neighbor heeft met de endnode word de current node toegevoegd aan de list en called hij recursief de functie weer aan met die neighbor als eindpunt.
                if (nodes[i].GetNeighbors().Contains(end))
                {
                    path.Add(CurrentNode);
                    Listnodes(start, nodes[i].GetName(), listOfNodes, path);
                    break;
                }
            }

            foreach (Node node in nodes)
            {
                if (node.GetName() == end)
                {
                    path.Add(node);
                }
            }
            //Sorteer de lijst op alfabetische volgorde en haal alle duplicates eruit
            path = path.OrderBy(x => x.GetName()).Distinct().ToList();
            return path;
        }
    }
}
