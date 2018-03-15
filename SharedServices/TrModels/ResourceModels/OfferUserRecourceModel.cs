using System;

namespace TrModels.ResourceModels
{
    /// <summary>
    /// Модель предложения пользователя
    /// </summary>
    public class OfferUserRecourceModel : OfferRecourceModel
    {
        /// <summary>
        /// Ид предложения
        /// </summary>
        public Guid OfferId { get; set; }
    }
}
