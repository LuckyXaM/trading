using System.Collections.Generic;

namespace TrModels.ResourceModels
{
    /// <summary>
    /// Модель бидов и аск
    /// </summary>
    public class BidAskResourceModel
    {
        /// <summary>
        /// Аски
        /// </summary>
        public List<OfferRecourceModel> Asks { get; set; }

        /// <summary>
        /// Биды
        /// </summary>
        public List<OfferRecourceModel> Bids { get; set; }
    }
}
