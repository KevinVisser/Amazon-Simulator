var exampleSocket;
var object;
var rack;
var group;
var scene;
var truck, bay, truckGroup;

var worldObjects = {};

function InterpretServer(scene)
{
    exampleSocket = new WebSocket("ws://" + window.location.hostname + ":" + window.location.port + "/connect_client");
    exampleSocket.onmessage = function (event)
    {
        var command = parseCommand(event.data);
        if (command.command == "update")
        {
            if (Object.keys(worldObjects).indexOf(command.parameters.guid) < 0)
            {
                if (command.parameters.type == "robot")
                {
                    var geometry = new THREE.BoxGeometry(0.9, 0.3, 0.9);
                    var cubeMaterials = [
                        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //LEFT
                        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
                        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_top.png"), side: THREE.DoubleSide }), //TOP
                        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
                        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //FRONT
                        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //BACK
                    ];
                    var robot = new THREE.Mesh(geometry, cubeMaterials);
                    robot.position.y = 0.15;
                    
                    group = new THREE.Group();
                    group.add(robot);
                    
                    scene.add(group);
                    worldObjects[command.parameters.guid] = group;
                }
                else if(command.parameters.type == "palletRack")
                {
                    var rackGeo = new THREE.BoxGeometry(1.2, 2, 1.2);
                    var rackMaterial = new THREE.MeshPhongMaterial({ color: 0xFF0000, side: THREE.DoubleSide });
                    var rack = new THREE.Mesh(rackGeo, rackMaterial);
                    group = new THREE.Group();
                    group.add(rack);
                    
                    scene.add(group);
                    worldObjects[command.parameters.guid] = group;
                }
                else if(command.parameters.type == "truck")
                {
                    loadOBJModel("models/Truck/", "Truck.obj", "models/Truck/", "Truck.mtl", (mesh) => {
                        console.log("hallo")
                        mesh.scale.set(0.01, 0.01, 0.01);
                        truck = mesh;
                        mesh.position.set(1000, command.parameters.y, command.parameters.z);
                        
                        scene.add(mesh);
                        worldObjects[command.parameters.guid] = mesh;
                    });
                }
                else if(command.parameters.type == "loadingBay")
                {
                    loadOBJModel("models/Warehouse/", "warehouse.obj", "models/Warehouse/", "warehouse.mtl", (mesh) => {
                        mesh.scale.set(1.5, 1.5, 1);
                        mesh.position.set(30, 2.8, 15);
                        mesh.rotation.y = Math.PI / 2;

                        bay = mesh;

                        scene.add(bay);
                        worldObjects[command.parameters.guid] = bay;
                    });
                }
            }
            object = worldObjects[command.parameters.guid];
            object.position.x = command.parameters.x;
            object.position.y = command.parameters.y;
            object.position.z = command.parameters.z;

            object.rotation.x = command.parameters.rotationX;
            object.rotation.y = command.parameters.rotationY;
            object.rotation.z = command.parameters.rotationZ;
        }
    }
}