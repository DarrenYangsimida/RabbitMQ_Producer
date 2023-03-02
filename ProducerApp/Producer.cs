using Newtonsoft.Json;
using ProducerApp.AppUtils;
using ProducerApp.Models;
using RabbitMQ.Client;
using StackExchange.Redis;
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
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private IBasicProperties _properties;
        private readonly RedisHelper _redisHelper;

        public Producer(RedisHelper redisHelper)
        {
            InitializeComponent();
            _redisHelper = redisHelper;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        ~Producer()
        {
            _redisHelper.Dispose();
        }

        /// <summary>
        /// 窗体初始加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Producer_Load(object sender, EventArgs e)
        {
            LoadRedisData();
        }

        #region Rabbit MQ 操作

        /// <summary>
        /// 向 Rabbit MQ Server 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = richTextBox1.Text;
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    var body = Encoding.UTF8.GetBytes(msg);
                    _channel.BasicPublish(exchange: "test-Exchange", routingKey: "test-message.msg", basicProperties: _properties, body: body);
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

        /// <summary>
        /// 开启 Rabbit MQ 服务连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                _factory = new ConnectionFactory()
                {
                    HostName = "192.168.253.128",//rabbitmq server 地址，默认为本地 "localhost"
                    UserName = "admin",
                    Password = "123",
                    Port = 5672,
                };
                _connection = _factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "test-Exchange", type: ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);
                _channel.QueueDeclare(queue: "test-Queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                _channel.QueueBind(queue: "test-Queue", exchange: "test-Exchange", routingKey: "test-message.msg", arguments: null);
                _properties = _channel.CreateBasicProperties();
                _properties.DeliveryMode = 2;

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

        /// <summary>
        /// 关闭 Rabbit MQ 服务连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            CloseMQConnection();
        }

        /// <summary>
        /// 关闭 Rabbit MQ 服务连接
        /// </summary>
        public void CloseMQConnection()
        {
            try
            {
                _channel.Close();
                _connection.Close();
                _connection.Dispose();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        #endregion

        #region Redis 操作

        /// <summary>
        /// 添加数据到 ListBox 列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()) || string.IsNullOrWhiteSpace(textBox2.Text.Trim()))
            {
                MessageBox.Show("Login Name 和 Login Email 均不能为空，请重试");
            }
            else
            {
                listBox2.Items.Add($"{listBox2.Items.Count + 1}、LoginName: {textBox1.Text.Trim()}, LoginEmail: {textBox2.Text.Trim()}");
                textBox1.Text = textBox2.Text = "";
            }
        }

        /// <summary>
        /// 写入 Redis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            var str1 = "LoginName:";
            var str2 = ", LoginEmail:";
            List<RedisValue> data = new List<RedisValue>();
            foreach (var item in listBox2.Items)
            {
                var itemStr = item.ToString();
                if (!string.IsNullOrWhiteSpace(itemStr))
                {
                    var model = new UserModel
                    {
                        LoginName = itemStr.Substring(itemStr.IndexOf(str1) + str1.Length,
                                                      itemStr.IndexOf(str2) - itemStr.IndexOf(str1) - str1.Length).Trim(),
                        LoginEmail = itemStr[(itemStr.IndexOf(str2) + str2.Length)..].Trim()
                    };
                    data.Add(JsonConvert.SerializeObject(model));
                }
            }
            if (data.Count > 0)
            {
                _redisHelper.SetListValue("UserList", data.ToArray());
                listBox2.Items.Clear();
                LoadRedisData();
                MessageBox.Show("写入成功");
            }
            else
            {
                MessageBox.Show("请输入需要写入的内容");
            }
        }

        /// <summary>
        /// 读取 Redis 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            LoadRedisData();
        }

        /// <summary>
        /// 加载 Redis 数据
        /// </summary>
        private void LoadRedisData()
        {
            listBox1.Items.Clear();

            var userOid = _redisHelper.GetValue("UserOid");
            if (!string.IsNullOrWhiteSpace(userOid))
            {
                listBox1.Items.Add($"{listBox1.Items.Count + 1}、UserOid: {userOid}");
            }

            var user = _redisHelper.GetModelByIndex<UserModel>("UserList", new Random().Next());
            if (user != null && !string.IsNullOrWhiteSpace(user.LoginName))
            {
                listBox1.Items.Add($"{listBox1.Items.Count + 1}、LoginName: {user.LoginName}, LoginEmail: {user.LoginEmail}");
            }

            var userList = _redisHelper.GetList<UserModel>("UserList", 0, int.MaxValue);
            userList.ForEach(item =>
            {
                listBox1.Items.Add($"{listBox1.Items.Count + 1}、LoginName: {item.LoginName}, LoginEmail: {item.LoginEmail}");
            });
        }

        #endregion

    }
}
