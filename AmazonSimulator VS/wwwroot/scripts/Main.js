window.onload = function () 
{
    var camera, scene, renderer;
    var cameraControls;

    /**
     */
    function init()
    {
        camera = new THREE.PerspectiveCamera(70, window.innerWidth / window.innerHeight, 1, 1000);
        cameraControls = new THREE.OrbitControls(camera);
        camera.position.set(60, 25, 15);
        cameraControls.update();
        scene = new THREE.Scene();
        
        renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setPixelRatio(window.devicePixelRatio);
        renderer.setSize(window.innerWidth, window.innerHeight + 5);
        document.body.appendChild(renderer.domElement);
        
        window.addEventListener('resize', onWindowResize, false);
        
        LoadModels(scene);
        InterpretServer(scene);
    }
    
    /**
     */
    function onWindowResize()
    {
        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();
        renderer.setSize(window.innerWidth, window.innerHeight);
    }
    
    /**
     */
    function animate()
    {
        requestAnimationFrame(animate);
        cameraControls.update();
        renderer.render(scene, camera);
    }

    init();
    animate();
    
}