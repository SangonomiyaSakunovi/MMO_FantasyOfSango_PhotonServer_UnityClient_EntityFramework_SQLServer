using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyOfSango.Base;
using Photon.SocketServer;
using Common.ServerCode;
using Common.Tools;
using PlayerScript;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handler
{
    public class RegisterHandler : BaseHandler
    {
        public RegisterHandler() 
        {
            OpCode = OperationCode.Register;
        }
        
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            int changeUserInfoLineNum = 0;
            string usernameRegister = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Username);
            string passwordRegister = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Password);
            string userShowNameRegister = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.UserShowName);
            changeUserInfoLineNum = PlayerAdd.AddUserInfo(userShowNameRegister, usernameRegister, passwordRegister);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //Just need to transfer Bool, so use ReturnCode instead Parameters
            if (changeUserInfoLineNum != 0)
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
