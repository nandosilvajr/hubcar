using Ardalis.SmartEnum;


namespace HubCar.Shared.Models
{
    public class FilterRequest
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; }  = string.Empty;
        public double MimimumBid { get; set; }
        public double MaximiumBid { get; set; }
        public bool IsFavorite { get; set; }
        public string FilterSort { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }

    public sealed class FilterSort : SmartEnum<FilterSort>
    {
        public static readonly FilterSort MakeAscending = new FilterSort("Make ascending", "Make", "Asc",1);
        public static readonly FilterSort MakeDescending = new FilterSort("Make descending", "Make","Desc",2);
        public static readonly FilterSort StartBidAscending = new FilterSort("Start bid ascending", "StartBid","Asc",3);
        public static readonly FilterSort StartBidDescending = new FilterSort("Start bid descending", "StartBid","Desc",4);
        public static readonly FilterSort MileageAscending  = new FilterSort("Mileage bid ascending", "Mileage","Asc",5);
        public static readonly FilterSort MileageDescending  = new FilterSort("Mileage bid descending", "Mileage","Desc",6);        
        public static readonly FilterSort AuctionDateAscending  = new FilterSort("Auction date ascending", "AuctionDateTime","Asc",7);
        public static readonly FilterSort AuctionDateDescending  = new FilterSort("Auction date descending", "AuctionDateTime","Desc",8);

        public string Property { get; set; }
        public string Sort { get; set; }
        
        private FilterSort(string name, string property, string sort,int value) : base(name, value)
        {
            Property = property;
            Sort = sort;
        }
    }
}