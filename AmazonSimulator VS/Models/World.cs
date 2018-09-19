using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        Point point = new Point();
        private List<Object> worldObjects = new List<Object>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        
        public World() {
            Robot r = CreateRobot(0,0,0);
            //LoadingBay bay = CreateLoadingBay(10, 5, 10);
            Truck truck = CreateTruck(20, 10, 10);
            r.Move(4.6, 0, 13);
        }

        private Truck CreateTruck(double x, double y, double z)
        {
            Truck truck = new Truck(x, y, z, 0, 0, 0);
            worldObjects.Add(truck);
            return truck;
        }

        private LoadingBay CreateLoadingBay(double x, double y, double z)
        {
            LoadingBay bay = new LoadingBay(x, y, z, 0, 0, 0);
            worldObjects.Add(bay);
            return bay;
        }

        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot(x,y,z,0,0,0);
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