using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveScumAgent.Models
{
    public class DefaultSettings : GameSettings
    {
        [Index(IsUnique = true)]
        private int Default { get; set; } = 1;

        //TODO: Flesh this out a lot more. Needs to have <Default> and <Current global>
    }
}
