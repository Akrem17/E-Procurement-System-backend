﻿using System.ComponentModel.DataAnnotations;

namespace E_proc.Models
{


    public partial class Connections
        {   
            [Key]
            public int Id { get; set; }
            public int PersonId { get; set; }
            public string SignalrId { get; set; }
            public DateTime TimeStamp { get; set; }
        }
    
}
