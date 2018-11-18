using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using PathFinding;
using Tasks;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<Object> worldObjects = new List<Object>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();

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


            Truck t1 = CreateTruck(33, 0.23, 15);
            Truck t2 = CreateTruck(33, 0.23, 10);
            Truck t3 = CreateTruck(33, 0.23, 5);

            Robot r1 = CreateRobot(28, 0.15, 13.5, 'C', t1);
            Robot r2 = CreateRobot(28, 0.15, 13.5, 'D', t2);
            Robot r3 = CreateRobot(28, 0.15, 13.5, 'E', t3);



            //Pad voor de robot
            Pathfinding.Path = Pathfinding.Listnodes("A", "C4", Pathfinding.listOfNodes, Pathfinding.Path);

            r1.AddTask(new RobotMove(Pathfinding.Start));
            r1.AddTask(new RobotMove(Pathfinding.Path));

            Pathfinding.Path = new List<Node>();


            Pathfinding.Path = Pathfinding.Listnodes("A", "D4", Pathfinding.listOfNodes, Pathfinding.Path);

            r2.AddTask(new RobotMove(Pathfinding.Start));
            r2.AddTask(new RobotMove(Pathfinding.Path));

            Pathfinding.Path = new List<Node>();

            Pathfinding.Path = Pathfinding.Listnodes("A", "E4", Pathfinding.listOfNodes, Pathfinding.Path);

            r3.AddTask(new RobotMove(Pathfinding.Start));
            r3.AddTask(new RobotMove(Pathfinding.Path));
        }

        private Truck CreateTruck(double x, double y, double z)
        {
            Truck truck = new Truck(x, y, z, 0, Math.PI / 2, 0);
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