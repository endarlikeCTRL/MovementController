# MovementController
Simple Movement Controller incl. WallRun L|R, cr.: @/Plai




Unity Docs. to understand the Code:

  MonoBehaviour.FixedUpdate()
    https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
  AddForce
    https://docs.unity3d.com/2019.3/Documentation/ScriptReference/Rigidbody.AddForce.html 
  Vector3
    https://docs.unity3d.com/ScriptReference/Vector3-normalized.html
    
Keyboard Binds: https://docs.unity3d.com/ScriptReference/KeyCode.html




  Newer Unity version Fixes, a list:
  If the "Stuck to wall fix" doesn't work for you, especially with Unity 2020.3 as I've noticed, on the Physic material you add on the player set the "Friction combine" to "Minimum". - cr. @/Oxrod
  Jump "Slow fall" fix: Edit > Project Settings > Physics (Gravity Y) to something around -25 - cr. @/Seestral
  ![image](https://user-images.githubusercontent.com/69750826/156898014-b357be17-d4d3-44c0-862d-bc80a07ce25e.png) 
  Mouse Rotation Fix:
  for those having trouble with rotation, note that you have to update this line :
    cam.transform.rotation = Quaternion.Euler(xRotation, 0, 0);
    to this : cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
  so it also follow horizontal rotations. - cr. @/Warren Noth

  


Nearly all credits go to user @/Plai-Dev
