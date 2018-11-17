var exampleSocket;
var object;
var rack;
var group;
var scene;

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
                        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //LEFT
                        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
                        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_top.png"), side: THREE.DoubleSide }), //TOP
                        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
                        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //FRONT
                        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //BACK
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
                    console.log(command)
                    var rackGeo = new THREE.BoxGeometry(1.2, 2, 1.2);
                    var rackMaterial = new THREE.MeshBasicMaterial({ color: 0xFF0000, side: THREE.DoubleSide });
                    var rack = new THREE.Mesh(rackGeo, rackMaterial);
                    group = new THREE.Group();
                    group.add(rack);
                    
                    scene.add(group);
                    worldObjects[command.parameters.guid] = group;
                }
            }

            var object = worldObjects[command.parameters.guid];
            object.position.x = command.parameters.x;
            object.position.y = command.parameters.y;
            object.position.z = command.parameters.z;

            object.rotation.x = command.parameters.rotationX;
            object.rotation.y = command.parameters.rotationY;
            object.rotation.z = command.parameters.rotationZ;
        }
    }
}