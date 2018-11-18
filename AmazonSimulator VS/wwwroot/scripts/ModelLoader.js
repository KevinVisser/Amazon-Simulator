/**
* @param {string} modelPath
* @param {string} modelName
* @param {string} texturePath
* @param {string} textureName
* @param {function(THREE.Mesh): void} onload
* @return {void}
*/

function loadOBJModel(modelPath, modelName, texturePath, textureName, onload){
new THREE.MTLLoader()
    .setPath(texturePath)
    .load(textureName, function(materials){

        materials.preload();

        new THREE.OBJLoader()
            .setPath(modelPath)
            .setMaterials(materials)
            .load(modelName, function(object){
                onload(object);
            }, function () {} , function (e){console.log("Error loading model"); console.log(e); });
    });
}

function LoadModels(scene)
{
    LoadPlane(scene);
    LoadSkybox(scene);
    LoadRoad(scene);
    LoadLeftWall(scene);
    LoadRightWall(scene);
    LoadBackWall(scene);
    LoadFrontLeftWall(scene);
    LoadFrontRightWall(scene);
    LoadFrontMiddleWall(scene);
    LoadRoof(scene);
    LoadLogo(scene);
}

function LoadPlane(scene)
{
    var geometry = new THREE.PlaneGeometry(30, 30);
    var material = new THREE.MeshBasicMaterial({ color: 0x404040, side: THREE.DoubleSide });
    var plane = new THREE.Mesh(geometry, material);
    plane.rotation.x = Math.PI / 2.0;
    plane.position.x = 15;
    plane.position.z = 15;
    scene.add(plane);
}

function LoadSkybox(scene)
{
    var skyboxGeometry = new THREE.BoxGeometry(1000, 1000, 1000);
    var skyboxMaterials = [
        new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/skybox/skybox.png"), side: THREE.DoubleSide}),
        new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/skybox/skybox.png"), side: THREE.DoubleSide}),
        new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/skybox/skybox.png"), side: THREE.DoubleSide}),
        new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/skybox/skybox.png"), side: THREE.DoubleSide}),
        new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/skybox/skybox.png"), side: THREE.DoubleSide}),
        new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/skybox/skybox.png"), side: THREE.DoubleSide})
    ];
    var skybox = new THREE.Mesh(skyboxGeometry, skyboxMaterials);

    scene.add(skybox);
}

function LoadRoad(scene)
{
    loadOBJModel("models/Road/", "1229 Road.obj", "models/Road/", "1229 Road.mtl", (mesh) => {
        mesh.scale.set(0.51, 0.01, 0.024);
        mesh.position.set(33, 0, 15);
        mesh.rotation.y = Math.PI / 2;
        scene.add(mesh)
    });

    loadOBJModel("models/Road/", "1229 Road.obj", "models/Road/", "1229 Road.mtl", (mesh) => {
        mesh.scale.set(0.51, 0.01, 0.024);
        mesh.position.set(35, 0, 15);
        mesh.rotation.y = Math.PI / 2;
        scene.add(mesh)
    });

    loadOBJModel("models/Road/", "1229 Road.obj", "models/Road/", "1229 Road.mtl", (mesh) => {
        mesh.scale.set(0.51, 0.01, 0.024);
        mesh.position.set(37, 0, 15);
        mesh.rotation.y = Math.PI / 2;
        scene.add(mesh)
    });
}

function LoadLeftWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(30, 10, 2);
    var wallMat = new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var leftWall = new THREE.Mesh(wallGeo, wallMat);
    leftWall.position.set(15, 5, -1);
    scene.add(leftWall);
}

function LoadRightWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(30, 10, 2);
    var wallMat = new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var rightWall = new THREE.Mesh(wallGeo, wallMat);

    rightWall.position.set(15, 5, 31);
    scene.add(rightWall);
}

function LoadBackWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(34, 10, 2);
    var wallMat = new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var backWall = new THREE.Mesh(wallGeo, wallMat);
    backWall.position.set(-1, 5, 15);
    backWall.rotation.y = Math.PI / 2;

    scene.add(backWall);
}

function LoadFrontLeftWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(13, 10, 2);
    var wallMat = new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var backWall = new THREE.Mesh(wallGeo, wallMat);
    backWall.position.set(31, 5, 25.5);
    backWall.rotation.y = Math.PI / 2;

    scene.add(backWall);

}

function LoadFrontRightWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(14.5, 10, 2);
    var wallMat = new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var backWall = new THREE.Mesh(wallGeo, wallMat);
    backWall.position.set(31, 5, 5.25);
    backWall.rotation.y = Math.PI / 2;

    scene.add(backWall);
}

function LoadFrontMiddleWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(6.5, 6, 2);
    var wallMat = new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var backWall = new THREE.Mesh(wallGeo, wallMat);
    backWall.position.set(31, 7, 15.75);
    backWall.rotation.y = Math.PI / 2;

    scene.add(backWall);
}

function LoadRoof(scene)
{
    var geometry = new THREE.PlaneGeometry(30, 30);
    var material = new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide });
    var plane = new THREE.Mesh(geometry, material);
    plane.rotation.x = Math.PI / 2.0;
    plane.position.set(15, 10, 15);
    scene.add(plane);
}

function LoadLogo(scene)
{ 
    var geometry = new THREE.PlaneGeometry(25, 5);
    var material = new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/Logo.png"), side: THREE.DoubleSide });
    var plane = new THREE.Mesh(geometry, material);
    plane.rotation.y = Math.PI / 2.0;
    plane.position.set(32.1, 7, 15);
    scene.add(plane);
}