using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_Url
{
    class DIRValidator : IDIRValidator
    {
        private string direction;

        public DIRValidator()
        {
            this.direction = null;
        }
        public DIRValidator(string direction)
        {
            this.direction = direction;
        }
        private bool GetValidation(string direction)
        {
            return Directory.Exists(direction);
        }
        public bool GetValidation()
        {
            return GetValidation(direction);
        }
    }
}
