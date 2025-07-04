﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Models.Querying.Filters
{
    public class QueryParameters
    {
        public string? SortBy { get; set; } = "Id";
        public bool? SortDescending { get; set; } = false;

        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 1;
    }
}
