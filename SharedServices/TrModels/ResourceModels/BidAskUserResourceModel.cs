using System.Collections.Generic;

namespace TrModels.ResourceModels
{
    /// <summary>
    /// Модель бидов и аск пользователя
    /// </summary>
    public class BidAskUserResourceModel
    {
        /// <summary>
        /// Аски
        /// </summary>
        public List<OfferUserRecourceModel> Asks { get; set; }

        /// <summary>
        /// Биды
        /// </summary>
        public List<OfferUserRecourceModel> Bids { get; set; }
    }
}
