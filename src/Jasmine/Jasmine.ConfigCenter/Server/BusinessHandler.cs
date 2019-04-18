using DotNetty.Transport.Channels;
using Jasmine.ConfigCenter.Common;

namespace Jasmine.ConfigCenter.Server
{
    public class BusinessHandler : ChannelHandlerAdapter
    {
        private const string PATH = "path";
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

            ProcessRequest(reqContext);

            _pool.Recycle(reqContext);

            


        }

        private void ProcessRequest(ConfigCenterRequestContext  context)
        {
            string path=null;
            switch (context.Request.Path)
            {
                case ConfigCenterServiceConstants.CREAT_NODE:
                    path = context.Request.Parameter[PATH];
                    _tree.CreateNode(path,NodeType.permalent,context.Connection);
                    break;
                case ConfigCenterServiceConstants.REMOVE_NODE:
                    path = context.Request.Parameter[PATH];
                    _tree.RemoveNode(path);
                    break;
                case ConfigCenterServiceConstants.GET_DATA:
                    break;
                case ConfigCenterServiceConstants.SET_DATA:
                    break;
                case ConfigCenterServiceConstants.SUBSCRIBE_DATACHANGED:
                    break;
                case ConfigCenterServiceConstants.UNSUBSCRIBE_DATACHANGED:
                    break;
                case ConfigCenterServiceConstants.SUNSCRIBE_CHILDRENCREATED:
                    break;
                case ConfigCenterServiceConstants.UNSUBSCRIBE_CHILDRENCREATED:
                    break;
                case ConfigCenterServiceConstants.SUBSCRIBE_NODEREMOVED:
                    break;
                case ConfigCenterServiceConstants.UNSUBSCRIBE_NODEREMOVED:
                    break;
                default:
                    context.Response = ConfigCenterResponseFatory.CreateServiceNotFound(context.Request.RequestId);
                    break;
            }

        }
    }
}
