﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class PersonCheckModel
    {
        public Person Person {  get; set; } = new Person() {};
        public bool isChecked {  get; set; } = false;
    }
}
