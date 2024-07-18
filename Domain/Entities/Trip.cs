using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
     public class Trip
    {
        
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string Time { get; set; }
            public Plan Plan { get; set; }
            public ICollection<Place> Places { get; set; }
        

    }
}
