using FantasyOfSango.Base;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ServerCode;
using Common.Tools;
using PlayerScript;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handler
{
    public class LoginHandler : BaseHandler
    {
        
        public LoginHandler() 
        {
            OpCode = OperationCode.Login;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            bool isValidUserInfo = false;
            string usernameLogin = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Username);
            string passwordLogin = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Password);
            isValidUserInfo = PlayerLookUp.VerifyUserInfo(usernameLogin, passwordLogin);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //Just need to transfer Bool, so use ReturnCode instead Parameters
            if (isValidUserInfo)
            {
                response.ReturnCode = (short)ReturnCode.Success;
            }
            else
            {
                response.ReturnCode = (short)ReturnCode.Fail;
            }
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
