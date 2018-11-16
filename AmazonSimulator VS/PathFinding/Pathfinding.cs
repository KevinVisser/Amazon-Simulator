using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MoreLinq;

namespace PathFinding
{
    public class Pathfinding
    {
        public static List<Node> listOfNodes = new List<Node>();
        public static List<Node> Path = new List<Node>();
        public static List<Node> Start = new List<Node>();

        public static void FillList()
        {
            listOfNodes.Add(new Node("A", 28, 0, 13.5, "B"));
            Start.Add(new Node("A", 28, 0, 13.5, "B"));
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

            listOfNodes.Add(new Node("F", 7.5, 0, 22.5, "E4", "G"));
            listOfNodes.Add(new Node("G", 12.5, 0, 22.5, "D4", "H"));
            listOfNodes.Add(new Node("H", 17.5, 0, 22.5, "C4", "I"));
            listOfNodes.Add(new Node("I", 28, 0, 22.5, "J"));
            listOfNodes.Add(new Node("J", 28, 0, 18, "A"));
        }

        public static void Listnodes(string start, string end, List<Node> nodes)
        {
            Node CurrentNode;
            string currentNode;
            //RECURSIEFANBOYs
            for (int i = 0; i < nodes.Count(); i++)
            {
                currentNode = nodes[i]._nodeName;
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
                if (nodes[i].Neighbors.Contains(end))
                {
                    Path.Add(CurrentNode);
                    Listnodes(start, nodes[i]._nodeName, listOfNodes);
                    break;
                }
            }

            foreach (Node node in nodes)
            {
                if (node._nodeName == end)
                {
                    Path.Add(node);
                }
            }
            //Sorteer de lijst op alfabetische volgorde en haal alle duplicates eruit
            Path = Path.OrderBy(x => x._nodeName).Distinct().ToList();
        }

        public static void CheckForDupes(List<Node> list)
        {
            foreach (Node node in list)
            {
                Console.WriteLine(node._nodeName);
            }
            Console.WriteLine();
        }
    }
}
