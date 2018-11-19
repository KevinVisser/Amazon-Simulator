using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using PathFinding;
using Tasks;

namespace Models {
    /// <summary>
    /// World class
    /// </summary>
    public class World : IObservable<Command>, IUpdatable
    {
        private List<Object> worldObjects = new List<Object>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        /// <summary>
        /// constructor voor de world
        /// </summary>
        public World() {
            //vul de lijst met alle nodes
            Pathfinding.FillList();

            LoadingBay bay = CreateLoadingBay(20, 20, 20);

            //Maak objecten aan
            Pathfinding.listOfNodes[5].SetRack(CreatePalletRack(15, 1.4, 10));
            Pathfinding.listOfNodes[6].SetRack(CreatePalletRack(15, 1.4, 11.5));
            Pathfinding.listOfNodes[7].SetRack(CreatePalletRack(15, 1.4, 13));
            Pathfinding.listOfNodes[8].SetRack(CreatePalletRack(15, 1.4, 14.5));

            Pathfinding.listOfNodes[9].SetRack(CreatePalletRack(10, 1.4, 10));
            Pathfinding.listOfNodes[10].SetRack(CreatePalletRack(10, 1.4, 11.5));
            Pathfinding.listOfNodes[11].SetRack(CreatePalletRack(10, 1.4, 13));
            Pathfinding.listOfNodes[12].SetRack(CreatePalletRack(10, 1.4, 14.5));

            Pathfinding.listOfNodes[13].SetRack(CreatePalletRack(5, 1.4, 10));
            Pathfinding.listOfNodes[14].SetRack(CreatePalletRack(5, 1.4, 11.5));
            Pathfinding.listOfNodes[15].SetRack(CreatePalletRack(5, 1.4, 13));
            Pathfinding.listOfNodes[16].SetRack(CreatePalletRack(5, 1.4, 14.5));


            Truck t1 = CreateTruck(33, 0.23, 60, "1");
            Truck t2 = CreateTruck(35, 0.23, 60, "2");
            Truck t3 = CreateTruck(37, 0.23, 60, "3");

            Robot r1 = CreateRobot(28, 0.15, 13.5, 'C', t1);
            Robot r2 = CreateRobot(28, 0.15, 13.5, 'D', t2);
            Robot r3 = CreateRobot(28, 0.15, 13.5, 'E', t3);


            //Pad voor de Truck
            Pathfinding.PathTruck = Pathfinding.Listnodes("X1", "Y1", Pathfinding.listOfNodes, Pathfinding.PathTruck);

            t1.AddTask(new TruckMove(Pathfinding.StartTruck1));
            t1.AddTask(new TruckMove(Pathfinding.PathTruck));
            Pathfinding.PathTruck = new List<Node>();


            Pathfinding.PathTruck = Pathfinding.Listnodes("X2", "Y2", Pathfinding.listOfNodes, Pathfinding.PathTruck);

            t2.AddTask(new TruckMove(Pathfinding.StartTruck2));
            t2.AddTask(new TruckMove(Pathfinding.PathTruck));
            Pathfinding.PathTruck = new List<Node>();


            Pathfinding.PathTruck = Pathfinding.Listnodes("X3", "Y3", Pathfinding.listOfNodes, Pathfinding.PathTruck);

            t3.AddTask(new TruckMove(Pathfinding.StartTruck3));
            t3.AddTask(new TruckMove(Pathfinding.PathTruck));


            //Pad voor de robot
            Pathfinding.PathRobot = Pathfinding.Listnodes("A", "C4", Pathfinding.listOfNodes, Pathfinding.PathRobot);

            r1.AddTask(new RobotMove(Pathfinding.StartRobot));
            r1.AddTask(new RobotMove(Pathfinding.PathRobot));
            Pathfinding.PathRobot = new List<Node>();


            Pathfinding.PathRobot = Pathfinding.Listnodes("A", "D4", Pathfinding.listOfNodes, Pathfinding.PathRobot);

            r2.AddTask(new RobotMove(Pathfinding.StartRobot));
            r2.AddTask(new RobotMove(Pathfinding.PathRobot));
            Pathfinding.PathRobot = new List<Node>();

            Pathfinding.PathRobot = Pathfinding.Listnodes("A", "E4", Pathfinding.listOfNodes, Pathfinding.PathRobot);

            r3.AddTask(new RobotMove(Pathfinding.StartRobot));
            r3.AddTask(new RobotMove(Pathfinding.PathRobot));
        }

        private Truck CreateTruck(double x, double y, double z, string name)
        {
            Truck truck = new Truck(x, y, z, 0, Math.PI, 0, name);
            worldObjects.Add(truck);
            return truck;
        }

        private LoadingBay CreateLoadingBay(double x, double y, double z)
        {
            LoadingBay bay = new LoadingBay(x, y, z, 0, 0, 0);
            worldObjects.Add(bay);
            return bay;
        }

        private PalletRack CreatePalletRack(double x, double y, double z)
        {
            PalletRack rack = new PalletRack(x, y, z, 0, 0, 0);
            worldObjects.Add(rack);
            return rack;
        }

        private Robot CreateRobot(double x, double y, double z, char name, Truck t) {
            Robot r = new Robot(x, y, z, 0, 0, 0, name, t);
            worldObjects.Add(r);
            return r;
        }

        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer)) {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c) {
            for(int i = 0; i < this.observers.Count; i++) {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs) {
            foreach(Object m3d in worldObjects) {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }

        /// <summary>
        /// Update de objecten
        /// </summary>
        /// <param name="tick">hoe vaak</param>
        /// <returns></returns>
        public bool Update(int tick)
        {
            for(int i = 0; i < worldObjects.Count; i++) {
                Object u = worldObjects[i];

                if(u is IUpdatable) {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

                    if(needsCommand) {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
            }

            return true;
        }
    }

    internal class Unsubscriber<Command> : IDisposable
    {
        private List<IObserver<Command>> _observers;
        private IObserver<Command> _observer;

        internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose() 
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}