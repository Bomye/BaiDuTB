using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.ORM;

namespace BaiDuTB
{
    [Table(TableName = "baidutb", DatabaseName = "fortest", Version = 1)]
    public class baidutb : SqlDataBase
    {
        [Column(IsPrimary = true, IsIdentity = false, Fieldname = "name")]
        public System.String name { get; set; }
        [Column(IsPrimary = false, IsIdentity = false, Fieldname = "content")]
        public System.String content { get; set; }
        [Column(IsPrimary = false, IsIdentity = false, Fieldname = "landLord")]
        public System.String landLord { get; set; }
        [Column(IsPrimary = false, IsIdentity = false, Fieldname = "time")]
        public DateTime? time { get; set; }
        [Column(IsPrimary = true, IsIdentity = false, Fieldname = "lastUpDate")]
        public DateTime lastUpDate { get; set; }
    }

}
