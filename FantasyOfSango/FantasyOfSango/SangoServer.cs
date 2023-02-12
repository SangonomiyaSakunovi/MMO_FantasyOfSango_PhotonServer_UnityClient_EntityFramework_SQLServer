using Common.ServerCode;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using FantasyOfSango.Base;
using FantasyOfSango.Handler;
using log4net.Config;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription: The main cs of PhotonServer, give the Config

namespace FantasyOfSango
{
    public class SangoServer : ApplicationBase
    {
        //There will be only one _log exist, so the permission readonly
        public static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        public static SangoServer _Instance
        {
            get; private set;
        }

        public Dictionary<OperationCode,BaseHandler> HandlerDict = new Dictionary<OperationCode,BaseHandler>();

        //Build connectioner peer
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            _log.Info("New Client connected");
            return new ClientPeer(initRequest);
        }

        //Call when the server initiating
        protected override void Setup()
        {
            _Instance = this;

            //Configure customize log path, the applicationLogPath need to specify
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"]
                = Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_Win64"), "log");            
            //Configure the configFile position
            FileInfo logConfigFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (logConfigFileInfo.Exists)
            {
                //Set Factory and Read
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(logConfigFileInfo);
            }
            //Use _log to show customize info
            _log.Info("Sango Server is Running");

            //Initiate the Handler
            InitiateHandler();
            _log.Info("Handler is Initiate");
        }

        //Call when the server shutDown
        protected override void TearDown()
        {
            _log.Info("Sango Server is Shut Down");
        }

        public void InitiateHandler()
        {
            DefaultHandler defaultHandler = new DefaultHandler();
            HandlerDict.Add(defaultHandler.OpCode, defaultHandler);
            LoginHandler loginHandler = new LoginHandler();           
            HandlerDict.Add(loginHandler.OpCode, loginHandler);
            RegisterHandler registerHandler = new RegisterHandler();
            HandlerDict.Add(registerHandler.OpCode, registerHandler);
        }
    }
}
