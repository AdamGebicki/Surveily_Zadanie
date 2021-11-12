using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_Url
{
    class DirMenager
    {
        private string direction;

        public DirMenager()
        {
            this.direction = null;
        }

        public DirMenager(string direction)
        {
            this.direction = direction;
        }
        public bool GetValidation(string direction)
        {
            return Directory.Exists(direction);
        }
        public bool GetValidation()
        {
            return GetValidation(direction);
        }
    }
}
