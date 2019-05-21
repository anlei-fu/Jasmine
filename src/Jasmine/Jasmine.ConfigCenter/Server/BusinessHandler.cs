using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Jasmine.ConfigCenter.Common;
using Jasmine.Reflection;

namespace Jasmine.ConfigCenter.Server
{
    public class BusinessHandler : ChannelHandlerAdapter,INodeEventHandler
    {
        private const string PATH = "path";
        private const string NODE_TYPE = "nodetype";
        private const string DATA = "data";
        private ConfigeCenterContextPool _pool;
        private IConnectionManager _manager;
        private ConfigTree _tree;
        public BusinessHandler()
        {
        }
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {

            var req = message as ConfigCenterServiceRequest;

            if (req==null)
            {

            }

            var reqContext=  _pool.Rent();
            reqContext.Connection = _manager.GetConnection(context.Channel.Id.AsLongText());
            reqContext.Request = req;

            processRequest(reqContext);

            context.WriteAndFlushAsync(reqContext.Response);

            reqContext.Response.ResponseCode = ResponseCode.Suceessed;
            reqContext.Response.ResponseId = 0;
            reqContext.Response.Result = null;
            reqContext.Response.Message = string.Empty;

            _pool.Recycle(reqContext);

        }

        public Task OnChildrenCreated(string path, IEnumerable<ConnectionInfo> clients)
        {
            throw new System.NotImplementedException();
        }

        public Task OnDataChanged(string path, IEnumerable<ConnectionInfo> clients)
        {
            throw new System.NotImplementedException();
        }

        public Task OnNodeRemoved(string path, IEnumerable<ConnectionInfo> clients)
        {
            throw new System.NotImplementedException();
        }

        private void processRequest(ConfigCenterRequestContext  context)
        {
            string path = string.Empty;

            if (context.Request.Parameter.ContainsKey(PATH))
              path=context.Request.Parameter[PATH];
            else
            {
                context.Response.ResponseCode = ResponseCode.ParameterNotFound;
                context.Response.Message = $"paramter:{PATH} is not found! ";
                return;
            }

            switch (context.Request.Path)
            {
                case ConfigCenterServiceConstants.CREATE_NODE:

                    if (context.Request.Parameter.ContainsKey(NODE_TYPE))
                    {
                        if(JasmineStringValueConvertor.TryGetValue<NodeType>(context.Request.Parameter[NODE_TYPE],out var result))
                        {

                        }
                        else
                        {
                            context.Response.ResponseCode = ResponseCode.ParameterIncorrect;
                            context.Response.Message = $" cant convert {context.Request.Parameter[NODE_TYPE]} to {typeof(NodeType)}";
                        }
                    }
                    else
                    {
                        context.Response.ResponseCode = ResponseCode.ParameterNotFound;
                        context.Response.Message = $" required parameter {NODE_TYPE} is not found ";
                    }

                    context.Response.Result = _tree.CreateNode(path,NodeType.Permalent,context.Connection).ToString();
                 
                    break;
                case ConfigCenterServiceConstants.REMOVE_NODE:
                 context.Response.Result= _tree.RemoveNode(path)?
                                                         NodeOperateResult.Successced.ToString():NodeOperateResult.NotExist.ToString();
                    break;
                case ConfigCenterServiceConstants.GET_DATA:
                 context.Response.Result= _tree.GetData(path).ToString();
                    break;
                case ConfigCenterServiceConstants.SET_DATA:
                    byte[] data2 = null;

                    if(context.Request.Parameter.ContainsKey(DATA))
                    {
                        if(JasmineStringValueConvertor.TryGetValue<byte[]>(context.Request.Parameter[DATA],out var value))
                        {
                            context.Response.ResponseCode = ResponseCode.Suceessed;
                        }
                        else
                        {
                            context.Response.ResponseCode = ResponseCode.ParameterIncorrect;
                            context.Response.Message = $" the required parameter {DATA} is incorrect!";
                        }
                    }
                    else
                    {
                        context.Response.ResponseCode = ResponseCode.ParameterNotFound;
                        context.Response.Message = $" the required paramter {DATA} is not found ";
                    }
                      
                   context.Response.Result = _tree.SetData(path,data2)?
                                                    NodeOperateResult.Successced.ToString():NodeOperateResult.NotExist.ToString();
                    break;
                case ConfigCenterServiceConstants.SUBSCRIBE_DATACHANGED:

                    context.Response.Result = _tree.SubscribeChildrebCreated(path, context.Connection).ToString();

                    break;
                case ConfigCenterServiceConstants.UNSUBSCRIBE_DATACHANGED:
                    context.Response.Result = _tree.UnSubscribeDataChnaged(path, context.Connection).ToString();

                    break;
                case ConfigCenterServiceConstants.SUNSCRIBE_CHILDRENCREATED:
                    context.Response.Result = _tree.SubscribeChildrebCreated(path, context.Connection).ToString();
                    break;
                case ConfigCenterServiceConstants.UNSUBSCRIBE_CHILDRENCREATED:
                    context.Response.Result = _tree.UnSubscribeChildrenCreated(path, context.Connection).ToString();
                    break;
                case ConfigCenterServiceConstants.SUBSCRIBE_NODEREMOVED:
                    context.Response.Result = _tree.SubscribeNodeRemoved(path, context.Connection).ToString();
                    break;
                case ConfigCenterServiceConstants.UNSUBSCRIBE_NODEREMOVED:
                    context.Response.Result = _tree.UnSbscribeNodeRemoved(path, context.Connection).ToString();
                    break;
                default:
                    context.Response = ConfigCenterResponseFatory.CreateServiceNotFound(context.Request.RequestId);
                    break;
            }

        }
    }
}
