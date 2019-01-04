﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models
{
    public class Person
    {
        [Key]
        public int person_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int state_id { get; set; }
        public string state_code { get; set; }
        public char gender { get; set; }
        public DateTime dob { get; set; }
    }
}
