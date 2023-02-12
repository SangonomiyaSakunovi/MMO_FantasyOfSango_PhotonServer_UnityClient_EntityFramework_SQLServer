using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerScript;
using FantasyOfSango.Base;
using Common.Tools;
using Common.ServerCode;

//Developer : SangonomiyaSakunovi
//Discription: ClientPeer behaviours should define here

namespace FantasyOfSango
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        //Call father class to intiate
        public ClientPeer(InitRequest initRequest):base (initRequest) 
        {
            
        }
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //Initiate a Handler
            BaseHandler handler = DictTools.GetDictValue<OperationCode, BaseHandler>
                (SangoServer._Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
            SangoServer._log.Info("The handler is "+ handler.OpCode);
            if (handler != null )
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                BaseHandler defaultHandler = DictTools.GetDictValue<OperationCode, BaseHandler>
                                                (SangoServer._Instance.HandlerDict, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }



            //Switch the Request by OperationCode
            //switch (operationRequest.OperationCode)
            //{
            //    case 1:
            //        //Recieve the message
            //        SangoServer._log.Info("Recieve a Request from Client, the OperationCode is " + operationRequest.OperationCode);
            //        Dictionary<byte, object> dataClientRequest = operationRequest.Parameters;
            //        object intValue;
            //        dataClientRequest.TryGetValue(1, out intValue);
            //        object stringValue;
            //        dataClientRequest.TryGetValue(2, out stringValue);
            //        SangoServer._log.Info("The ClientIntRequest is " + intValue.ToString());
            //        SangoServer._log.Info("The ClientStringRequest is " + stringValue.ToString());
            //        //Give the Response
            //        OperationResponse operationResponse = new OperationResponse(1);
            //        Dictionary<byte, object> dataServerResponse = new Dictionary<byte, object>();
            //        dataServerResponse.Add(1, -20);
            //        dataServerResponse.Add(2, "That`s the Response by SangoServer");
            //        operationResponse.SetParameters(dataServerResponse);
            //        SendOperationResponse(operationResponse, sendParameters);
            //        //Give the EventCode
            //        EventData eventData = new EventData(1);
            //        Dictionary<byte, object> dataServerEvent = new Dictionary<byte, object>();
            //        dataServerEvent.Add(1, 0);
            //        dataServerEvent.Add(2, "That`s the Event by SangoServer");
            //        eventData.SetParameters(dataServerEvent);
            //        SendEvent(eventData, new SendParameters());
            //        break;
            //    case 00000000:
            //        //Only to test if can use EF to add userInfo
            //        PlayerAdd.AddUserInfo("TestShowName","TestUsername","TestPassword");
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
