using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BLL
{
    public class BaseBLL : kk.ORM.BLL.BaseBLL
    {
        public BaseBLL()
            : base(ConfigurationManager.AppSettings["ServiceUrl"])
        {

        }
    }
}
