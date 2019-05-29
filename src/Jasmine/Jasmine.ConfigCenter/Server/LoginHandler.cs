using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Jasmine.Common;
using Jasmine.ConfigCenter.Common;
using Jasmine.Serialization;

namespace Jasmine.ConfigCenter.Server
{
    public class LoginHandler : ChannelHandlerAdapter
    {
        private IConnectionManager _manager;
        private IValidator _validator;
        private ISerializer _serializer;
        public LoginHandler()
        {
        }
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var msg = message as Event;

            if (msg == null)
            {

            }

            if (!_manager.ConnectionRegisted(context.Channel.Id.AsLongText()))
            {
                if (msg.EventType == EventType.Login)
                {
                    if (_serializer.TryDeserialize<LoginInfo>(msg.Content, out var info))
                    {
                        if (_validator.Validate(info.User, info.Password))
                        {
                            _manager.AddConnection(context.Channel.Id.AsLongText(), new ConnectionInfo(context.Channel));
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else if (msg.EventType == EventType.HeartBeat)
            {

            }
            else if (msg.EventType == EventType.Request)
            {
                if (_serializer.TryDeserialize<ConfigCenterServiceRequest>(msg.Content, out var request))
                {

                }
                else
                {
                    context.FireChannelRead(request);
                }
            }
            else
            {

            }



        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);
        }
        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            return base.WriteAsync(context, message);
        }
        



    }
}
