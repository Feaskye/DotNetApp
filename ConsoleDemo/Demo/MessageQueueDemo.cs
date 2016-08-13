using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo.Demo
{
    class MessageQueueDemo
    {
        // Send Message
        private static void SendMessage(string txtMessage)
        {
            // Open queue
            MessageQueue queue = new MessageQueue(".\\Private$\\MSMQDemo");

            // Create message
            Message message = new Message();
            message.Body = txtMessage;
            message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            message.Recoverable = true;//避免存放消息队列的计算机重新启动而丢失消息,消息传递过程中将消息保存到磁盘上来保证消息的传递
            
            // Put message into queue
            queue.Send(message);
        }

        // Receive Message
        private static string ReceiveMessage()
        {
            // Open queue
            MessageQueue queue = new MessageQueue(".\\Private$\\MSMQDemo");

            // Receive message, 同步的Receive方法阻塞当前执行线程，直到一个message可以得到
            Message message = queue.Receive();
            message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return message.Body.ToString();
        }
    }
}
