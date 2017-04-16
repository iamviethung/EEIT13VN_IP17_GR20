using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleApp.Entities
{
    public class Article
    {
        public string ArticleName { get; set; }
        public string ArticleNumber { get; set; }
        public string Storage { get; set; }
        public string ShelfNumber { get; set; }
        public string BrandOfManufacturer { get; set; }
    }
}