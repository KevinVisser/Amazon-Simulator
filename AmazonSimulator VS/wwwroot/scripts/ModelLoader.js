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

var lights, road;

function LoadModels(scene)
{
    LoadSun(scene);
    LoadRoad(scene);
    LoadLights(scene);
    LoadPlane(scene);
    LoadSkybox(scene);
    LoadLeftWall(scene);
    LoadRightWall(scene);
    LoadBackWall(scene);
    LoadFrontLeftWall(scene);
    LoadFrontRightWall(scene);
    LoadFrontMiddleWall(scene);
    LoadRoof(scene);
    LoadLogo(scene);
}
//#region Walls
function LoadPlane(scene)
{
    var geometry = new THREE.PlaneGeometry(30, 30);
    var material = new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/floor.png"), side: THREE.DoubleSide });
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
        road = mesh;
        scene.add(road)
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
    var wallMat = new THREE.MeshPhongMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var leftWall = new THREE.Mesh(wallGeo, wallMat);
    leftWall.position.set(15, 5, -1);
    scene.add(leftWall);
}

function LoadRightWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(30, 10, 2);
    var wallMat = new THREE.MeshPhongMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var rightWall = new THREE.Mesh(wallGeo, wallMat);

    rightWall.position.set(15, 5, 31);
    scene.add(rightWall);
}

function LoadBackWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(34, 10, 2);
    var wallMat = new THREE.MeshPhongMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var backWall = new THREE.Mesh(wallGeo, wallMat);
    backWall.position.set(-1, 5, 15);
    backWall.rotation.y = Math.PI / 2;

    scene.add(backWall);
}

function LoadFrontLeftWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(13, 10, 2);
    var wallMat = new THREE.MeshPhongMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var backWall = new THREE.Mesh(wallGeo, wallMat);
    backWall.position.set(31, 5, 25.5);
    backWall.rotation.y = Math.PI / 2;

    scene.add(backWall);

}

function LoadFrontRightWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(14.5, 10, 2);
    var wallMat = new THREE.MeshPhongMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var backWall = new THREE.Mesh(wallGeo, wallMat);
    backWall.position.set(31, 5, 5.25);
    backWall.rotation.y = Math.PI / 2;

    scene.add(backWall);
}

function LoadFrontMiddleWall(scene)
{
    var wallGeo = new THREE.BoxGeometry(6.5, 6, 2);
    var wallMat = new THREE.MeshPhongMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});
    var backWall = new THREE.Mesh(wallGeo, wallMat);
    backWall.position.set(31, 7, 15.75);
    backWall.rotation.y = Math.PI / 2;

    scene.add(backWall);
}

function LoadRoof(scene)
{
    var geometry = new THREE.PlaneGeometry(30, 30);
    var material = new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide });
    var plane = new THREE.Mesh(geometry, material);
    plane.rotation.x = Math.PI / 2.0;
    plane.position.set(15, 10, 15);
    scene.add(plane);
}
//#endregion

//#region Logo
function LoadLogo(scene)
{ 
    var geometry = new THREE.PlaneGeometry(25, 5);
    var material = new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/Logo.png"), side: THREE.DoubleSide });
    var plane = new THREE.Mesh(geometry, material);
    plane.rotation.y = Math.PI / 2.0;
    plane.position.set(32.1, 7, 15);
    scene.add(plane);
}
//#endregion

//#region Lights
function LoadLights(scene)
{
    lights = new THREE.Group();

    //Het laden van het model en de materials(textures)
    var mtlLoader = new THREE.MTLLoader();
    mtlLoader.setTexturePath("models/Light/");
    mtlLoader.setPath("models/Light/");
    mtlLoader.load("JCLobbyPendant.mtl", function (materials) {
        materials.preload();

        var objLoader = new THREE.OBJLoader()
        objLoader.setMaterials(materials)
        objLoader.setPath("models/Light/")
        objLoader.load("JCLobbyPendant.obj", function (geometry) {
            //hier kunnen we later wel aanpassen hoeveel objecten er zijn(moeilijksheidgraad)
            for (var i = 0; i < 5; i++)
            {
                for(var j = 0; j < 5; j++)
                {
                    //model
                    var light = geometry.clone();
                    light.scale.set(1, 1, 1);
                    light.position.set(5 * (i + 1), 9.7, 5 * (j + 1));
                    
                    //light
                    var pointLight = new THREE.PointLight(0xFFFFFF, 0.4, 20, 2);
                    pointLight.position.set(5 * (i + 1), 9.7, 5 * (j + 1));
                    

                    //add to scene
                    lights.add(light);
                    lights.add(pointLight);
                }
            }
        });
    });

    var mtlLoader = new THREE.MTLLoader();
    mtlLoader.setTexturePath("models/Light/");
    mtlLoader.setPath("models/Light/");
    mtlLoader.load("StreetLight.mtl", function (materials) {
        materials.preload();

        var objLoader = new THREE.OBJLoader()
        objLoader.setMaterials(materials)
        objLoader.setPath("models/Light/")
        objLoader.load("StreetLight.obj", function (pole) {
            for(var j = 0; j < 5; j++)
            {
                //model
                var light = pole.clone();
                light.scale.set(1, 2, 1);
                light.rotation.y = Math.PI;
                light.position.set(38, 2, 20 * j - 30);
                
                //light
                var pointLight = new THREE.PointLight(0xFFFFFF, 1, 50);
                pointLight.position.set(37.5, 2, 20 * j - 30);
                pointLight.rotation.y = Math.PI;
                pointLight.target = road;

                //add to scene
                lights.add(light);
                lights.add(pointLight);
            }
        });
    });

    scene.add(lights);    
}

function LoadSun(scene)
{
    var directionalLight = new THREE.DirectionalLight( 0xffffff, 0.5 );
    scene.add(directionalLight);
}
//#endregion