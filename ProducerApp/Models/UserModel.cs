using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerApp.Models
{
    [Serializable]
    public class UserModel
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登陆邮箱
        /// </summary>
        public string LoginEmail { get; set; }

    }
}
