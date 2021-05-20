using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssignmentDemo.Entities.API.AlbumDetails
{
    public class Album
    {
        
        public int userId { get; set; }
        
        public int id { get; set; }
       
        public string title { get; set; }
    }
}
