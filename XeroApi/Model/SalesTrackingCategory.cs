using System;

namespace XeroApi.Model
{
    public class SalesTrackingCategory : EndpointModelBase
    {
        public string TrackingCategoryName { get; set; }

        public string TrackingOptionName { get; set; }
    }





    public class SalesTrackingCategories : ModelList<SalesTrackingCategory>
    {
    }
}
