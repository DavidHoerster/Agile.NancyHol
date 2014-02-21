﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyRunner.Models
{
    public class QuoteDetail : BaseQuote
    {
        public String Id { get; set; }
        public String Title { get; set; }
        public String ArticleBody { get; set; }
        public Int32 Year { get; set; }
        public String Abstract { get; set; }
        public String Source { get; set; }
        public IList<QuoteDetail> SimilarItems { get; set; }
    }
}
