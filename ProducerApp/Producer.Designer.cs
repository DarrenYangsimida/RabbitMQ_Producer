
namespace ProducerApp
{
    partial class Producer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new System.Windows.Forms.Button();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            button2 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            listBox1 = new System.Windows.Forms.ListBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            button5 = new System.Windows.Forms.Button();
            listBox2 = new System.Windows.Forms.ListBox();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            button6 = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(531, 76);
            button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(98, 24);
            button1.TabIndex = 0;
            button1.Text = "开启 MQ 服务";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new System.Drawing.Point(8, 38);
            richTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(509, 100);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(531, 38);
            button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(98, 24);
            button2.TabIndex = 2;
            button2.Text = "发送消息";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(531, 112);
            button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(98, 24);
            button3.TabIndex = 3;
            button3.Text = "关闭 MQ 服务";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(531, 362);
            button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(98, 24);
            button4.TabIndex = 5;
            button4.Text = "从 Redis 读取";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Button4_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new System.Drawing.Point(8, 362);
            listBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(509, 106);
            listBox1.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 12);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(72, 17);
            label1.TabIndex = 7;
            label1.Text = "Rabbit MQ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 145);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(77, 17);
            label2.TabIndex = 8;
            label2.Text = "Redis - 写入";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(8, 335);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(77, 17);
            label3.TabIndex = 9;
            label3.Text = "Redis - 读取";
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(531, 255);
            button5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(98, 24);
            button5.TabIndex = 11;
            button5.Text = "保存到 Redis";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button5_Click;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 17;
            listBox2.Location = new System.Drawing.Point(8, 255);
            listBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            listBox2.Name = "listBox2";
            listBox2.Size = new System.Drawing.Size(509, 72);
            listBox2.TabIndex = 12;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(98, 170);
            textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(418, 23);
            textBox1.TabIndex = 13;
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(98, 196);
            textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(418, 23);
            textBox2.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(8, 172);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(86, 17);
            label4.TabIndex = 15;
            label4.Text = "Login Name :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 199);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(82, 17);
            label5.TabIndex = 16;
            label5.Text = "Login Email :";
            // 
            // button6
            // 
            button6.Location = new System.Drawing.Point(531, 170);
            button6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(98, 24);
            button6.TabIndex = 17;
            button6.Text = "Add To List";
            button6.UseVisualStyleBackColor = true;
            button6.Click += Button6_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(8, 229);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(68, 17);
            label6.TabIndex = 18;
            label6.Text = "待写入内容";
            // 
            // Producer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(647, 478);
            Controls.Add(label6);
            Controls.Add(button6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(listBox2);
            Controls.Add(button5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            MaximizeBox = false;
            Name = "Producer";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "生产者客户端";
            Load += Producer_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;

        /// <summary>
        /// 开启 MQ 服务
        /// </summary>
        private System.Windows.Forms.Button button1;

        /// <summary>
        /// 发送消息
        /// </summary>
        private System.Windows.Forms.Button button2;

        /// <summary>
        /// 关闭 MQ 服务
        /// </summary>
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label6;
    }
}

