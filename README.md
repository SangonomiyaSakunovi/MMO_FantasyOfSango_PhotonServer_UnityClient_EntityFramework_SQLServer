# MMO_FantasyOfSango_PhotonServer_UnityClient_EntityFramework_SQLServer
This project include the Server with Photon, Client with Unity, SQL with EntityFramework, and other develop projects, you can see video in https://space.bilibili.com/688232699

First, thanks for download my projects, and if you like, please give me some suggestions to help me develop it. 
And you may realize that it`s so easy to develop with my architecture! You can code only with C#, but you can control all the Client/Server/SQL, just some steps, so simple!

Here is the simple steps to start:

Step1: Open the Server solution, exam all the projects and there relations;

Step2: Modify the Username and Password about SQL Server, this is very important!

Step3: In the Package Manager Console, type: "add-migration test", this line will let you create a MigrationFiles, then type: "update-database", after that you can find a new DataBase will create in your SQL Server.

Step4: In the Server Engine folder, exam the PhotonConfig, and the run the PhotonServer.

Step5: Open Unity, import my ClientAsset, then run in Unity.

Now you can test the Login and Register function.

Function has developed:
BaseEnvironment, Register, Login, PlayerWeaponInfoSQL_Server

Function under develop:
WeaponDetail, PlayerWeaponInfoSQL_Client

Attention: the WeaponDetail will be a new solution, and that means it can develop in Team Mode, and make no effect in Server!

PS: The developer only have myself, so this architecture may have some issue. And if you have questions, please tell me.

PPS: I want to find a job in Japan, I can speak Chinese and English, also studying the Japanese. If you want to find a programmer who can develop with Server, please consider my offer!

With more info, you can contact me by: 1602274566@qq.com
