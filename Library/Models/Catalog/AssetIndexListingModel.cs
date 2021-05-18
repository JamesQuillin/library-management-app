using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Catalog
{
    // this viewmodel puts together data to send to the view, which includes
    // but isn't limited to parts of the data taken from the models we get 
    // from the db
    public class AssetIndexListingModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string AuthorOrDirector { get; set; }
        public string Type { get; set; }
        public string Dewey { get; set; }
        public string NumberOfCopies { get; set; }
    }
}
