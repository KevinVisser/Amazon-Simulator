/**
 * Het laden van een .obj en .mtl bestand
 * @param  {} modelPath - Het pad waar het model staat
 * @param  {} modelName - Hoe het .obj bestand heet
 * @param  {} texturePath - Het pad waar het model staat
 * @param  {} textureName - Hoe het .mtl bestand heet
 * @param  {} onload - Wanneer het bestand laadt
 */
function loadOBJModel(modelPath, modelName, texturePath, textureName, onload)
{
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

/**
 * Het laden van alle modellen en geometry
 * @param  {} scene - De THREE.js scene
 */
function LoadModels(scene)
{
    LoadSun(scene);
    LoadRoad(scene);
    LoadLights(scene);
    LoadSkybox(scene);
    LoadBuilding(scene);
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
        scene.add(mesh)
    });

    loadOBJModel("models/Road/", "1229 Road.obj", "models/Road/", "1229 Road.mtl", (mesh) => {
        mesh.scale.set(0.51, 0.01, 0.024);
        mesh.position.set(37, 0, 15);
        mesh.rotation.y = Math.PI / 2;
        scene.add(mesh)
    });
}

/**
 * @param  {} scene
 */
function LoadBuilding(scene)
{
    //Geometry
    var floorGeo = new THREE.PlaneGeometry(30, 30);
    var sideWallGeo = new THREE.PlaneGeometry(30, 10, 2);
    var frontRightGeo = new THREE.PlaneGeometry(13, 10, 2);
    var frontLeftGeo = new THREE.PlaneGeometry(11, 10, 2);
    var frontMiddleGeo = new THREE.PlaneGeometry(6.5, 6, 2);

    //Materials
    var floorMat = new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/floor.png"), side: THREE.DoubleSide });
    var wallMat = new THREE.MeshPhongMaterial({map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide});

    //Meshes
    var leftWall = new THREE.Mesh(sideWallGeo, wallMat);
    var rightWall = new THREE.Mesh(sideWallGeo, wallMat);
    var backWall = new THREE.Mesh(sideWallGeo, wallMat);
    var frontRightWall = new THREE.Mesh(frontRightGeo, wallMat);
    var frontLeftWall = new THREE.Mesh(frontLeftGeo, wallMat);
    var frontMiddleWall = new THREE.Mesh(frontMiddleGeo, wallMat);
    var floor = new THREE.Mesh(floorGeo, floorMat);
    var ceiling = new THREE.Mesh(floorGeo, floorMat);

    
    //Set positions
    leftWall.position.set(15, 5, 0);
    rightWall.position.set(15, 5, 30);
    backWall.position.set(0, 5, 15);
    frontRightWall.position.set(30, 5, 6.25);
    frontLeftWall.position.set(30, 5, 24.5);
    frontMiddleWall.position.set(30, 7, 15.75);
    floor.position.set(15, 0, 15);
    ceiling.position.set(15, 10, 15);
    
    //Set rotations
    backWall.rotation.y = Math.PI / 2;
    frontRightWall.rotation.y = Math.PI / 2;
    frontLeftWall.rotation.y = Math.PI / 2;
    frontMiddleWall.rotation.y = Math.PI / 2;
    floor.rotation.x = Math.PI / 2.0;
    ceiling.rotation.x = Math.PI / 2.0;
    
    //Add to scene
    scene.add(leftWall);
    scene.add(rightWall);
    scene.add(backWall);
    scene.add(frontRightWall);
    scene.add(frontLeftWall);
    scene.add(frontMiddleWall);
    scene.add(floor);
    scene.add(ceiling);
}
//#endregion

//#region Logo
function LoadLogo(scene)
{ 
    var geometry = new THREE.PlaneGeometry(25, 5);
    var material = new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/Logo.png"), side: THREE.DoubleSide });
    var plane = new THREE.Mesh(geometry, material);
    plane.rotation.y = Math.PI / 2.0;
    plane.position.set(30.1, 7, 15);
    scene.add(plane);
}
//#endregion

//#region Lights
function LoadLights(scene)
{
    var lights = new THREE.Group();

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