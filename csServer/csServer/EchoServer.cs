using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp.Server;

namespace csServer
{
    public class EchoServer : WebSocketBehavior
    {
        protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
        {
            this.Send("服务端返回：" + e.Data);
            base.OnMessage(e);
        }
    }
}
