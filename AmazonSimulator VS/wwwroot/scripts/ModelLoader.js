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
    LoadTruck(scene);
    LoadWarehouse(scene);
}

function LoadPlane(scene)
{
    var geometry = new THREE.PlaneGeometry(30, 30, 32);
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

function LoadTruck(scene)
{
    loadOBJModel("models/Truck/", "Truck.obj", "models/Truck/", "Truck.mtl", (mesh) => {
        mesh.scale.set(0.01, 0.01, 0.01);
        mesh.position.set(33, 0.23, 15);
        truck = mesh;
        scene.add(mesh);
    });
}

function LoadWarehouse(scene)
{
    loadOBJModel("models/Warehouse/", "warehouse.obj", "models/Warehouse/", "warehouse.mtl", (mesh) => {
        mesh.scale.set(1.5, 1.5, 1);
        mesh.position.set(30, 2.8, 15);
        mesh.rotation.y = Math.PI / 2;
        scene.add(mesh);
    });
}