using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProducerApp
{
    public partial class Producer : Form
    {
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private IBasicProperties properties;

        public Producer()
        {
            InitializeComponent();
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                factory = new ConnectionFactory()
                {
                    HostName = "localhost",//本地 rabbitmq 服务
                    UserName = "guest",
                    Password = "guest"
                };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: "SourceExchange", type: ExchangeType.Topic, durable: true, autoDelete: true, arguments: null);
                channel.QueueDeclare(queue: "testQueue", durable: true, exclusive: false, autoDelete: true, arguments: null);
                channel.QueueBind(queue: "testQueue", exchange: "SourceExchange", routingKey: "*.msg", arguments: null);
                properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"开启 MQ 服务失败，请重试！Error Message：{ex.Message}");
                CloseMQConnection();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CloseMQConnection();
        }

        public void CloseMQConnection()
        {
            try
            {
                channel.ExchangeDelete("SourceExchange");
                channel.QueueDelete("testQueue");
                channel.Close();
                connection.Close();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = richTextBox1.Text;
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    var body = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish(exchange: "SourceExchange", routingKey: $"{Guid.NewGuid()}.msg", basicProperties: properties, body: body);
                    MessageBox.Show("消息发送成功");
                    richTextBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("请输入要发送的内容");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"消息发送失败！Error Message: {ex.Message}");
            }
        }

    }
}
