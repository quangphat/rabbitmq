using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Dtos
{
    public class EmailDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; } 
    }
}
