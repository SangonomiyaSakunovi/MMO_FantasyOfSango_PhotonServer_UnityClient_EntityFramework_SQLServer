# MMO_FantasyOfSango_PhotonServer_UnityClient_EntityFramework_SQLServer
This project include the Server with Photon, Client with Unity, SQL with EntityFramework, and other develop projects, you can see video in https://space.bilibili.com/688232699

Attention!!! This project now used as a demo to find job, so it will be set as private, and the Update folder will upload some RAR. If you have the password, you can download and then do the test. If you not, you also can down load the original version, all my projects were stated from them, thanks you!

First, thanks for download my projects, and if you like, please give me some suggestions to help me develop it. 
And you may realize that it`s so easy to develop with my architecture! You can code only with C#, but you can control all the Client/Server/SQL, just some steps, so simple!

Here is the simple steps to start:

Step1: Open the Server solution, exam all the projects and there relations;

Step2: Modify the Username and Password about SQL Server, this is very important!

Step3: In the Package Manager Console, type: "add-migration test", this line will let you create a MigrationFiles, then type: "update-database", after that you can find a new DataBase will create in your SQL Server.

Step4: In the Server Engine folder, exam the PhotonConfig, and then run the PhotonServer.

Step5: Open Unity, import my ClientAsset, then run in Unity.

Now you can test the Login and Register function.

Function has developed:
BaseEnvironment, Register, Login, PlayerWeaponInfoSQL_Server， WeaponDetail，AttackSystem

Function under develop:
ElementSystem

Attention: the WeaponDetail is a new solution, and that means it can develop in Team Mode, and make no effect in Server! The more exciting function is I give a API to help Numercial Developer use less code, only no more than 10 lines to convert your ExcelInfo into the SQL Server! You may noticed that all the Info is Enum, it`s so convinent for the Message transfer between Client and Server!

Again, no more than 10 lines! 

For this function, I give a file.excel, you can test it in my Develop.Item.Server projects.

PS: The developer is only myself, so this architecture may have some issue. And if you have questions, please tell me.

With more info, you can contact me by: 1602274566@qq.com
