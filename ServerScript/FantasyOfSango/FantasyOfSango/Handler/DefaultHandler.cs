using Common.ServerCode;
using FantasyOfSango.Base;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handler
{
    public class DefaultHandler : BaseHandler
    {
        public DefaultHandler() 
        { 
            OpCode = OperationCode.Default;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            throw new NotImplementedException();
        }
    }
}
