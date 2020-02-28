using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LukeVo.Blazor.LDialog.Models
{

    public class LDialogResult<T>
    {
        public bool Confirm { get; set; }
        public T Result { get; set; }
    }

}
