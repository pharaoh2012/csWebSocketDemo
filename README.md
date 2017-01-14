# csWebSocketDemo
C#的websocket Demo，包含服务端和客户端

## 服务器端实现（C#，csServer目录）
1. 新建控制台C#项目。
2. 安装[websocket-sharp](https://github.com/sta/websocket-sharp):`Install-Package WebSocketSharp -Pre`
3. 创建服务端实现类 EchoServer，继承WebSocketBehavior，override OnMessage方法。发送消息：  
`this.Send("服务端返回：" + e.Data);`
4. 注册EchoSocket
```c#
    var wssv = new WebSocketServer("ws://localhost:5998");
    wssv.AddWebSocketService<EchoServer>("/Echo");
    wssv.Start();
    Console.ReadLine();
    wssv.Stop();
```    
5. 运行

## 客户端实现 (c#，csClient目录)
1. 新建控制台C#项目。
2. 安装[websocket-sharp](https://github.com/sta/websocket-sharp): `Install-Package WebSocketSharp -Pre`
3. 连接服务，发送消息。
```c#
	using (var ws = new WebSocket("ws://localhost:5998/Echo"))
	{
	    ws.OnMessage += (sender, e) =>
	        Console.WriteLine("Server says: " + e.Data);

	    ws.Connect();
	    for (int i = 0; i < 10; i++)
	    {
	        ws.Send("msg:" + i);
	        System.Threading.Thread.Sleep(500);
	    }
	    //ws.Send("BALUS 中文");
	    Console.ReadKey(true);
	}
```
4. 运行

## 客户端实现 (HTML,html目录)
1. 新建html页面，实现websocket方法。源代码见：[html/index.html](/html/index.html)
2. 运行,[在线访问](http://pharaoh168.coding.me/csWebSocketDemo/html/index.html)

## IE8，9的支持
1. 使用垫片技术，在IE8，9中用Flash实现websocket方法。
2. 选择了[web-socket-js](https://github.com/gimite/web-socket-js) ，直接将web_socket目录引用即可。 
3. 增加ie下的引用。源码：[html/ie.html](/html/ie.html)
```html
    <!-- IE8,9: -->
    <!--[if lte IE 9]>
      <script type="text/javascript" src="web_socket/swfobject.js"></script>
      <script type="text/javascript" src="web_socket/web_socket.js"></script>
      <script type="text/javascript">
        WEB_SOCKET_SWF_LOCATION = "web_socket/WebSocketMain.swf";
        //WEB_SOCKET_DEBUG = true;    
      </script>
    <![endif]-->
```    
4. 创建一个IIS虚拟目录运行，不能以文件的形式直接运行。[在线访问](http://pharaoh168.coding.me/csWebSocketDemo/html/ie.html)
