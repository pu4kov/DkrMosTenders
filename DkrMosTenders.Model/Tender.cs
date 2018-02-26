using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DkrMosTenders.Model
{
    public class Tender
    {
        public int Id { get; set; }
        /// <summary>
        /// Номер закупки в ДКР
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(20)]
        public string DkrNumber { get; set; }

        /// <summary>
        /// Номер закупки на ЭТП
        /// </summary>
        [MaxLength(20)]
        public string EtpNumber { get; set; }
        /// <summary>
        /// Наименование закупки
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Перечень объектов, по которым проводится закупка
        /// </summary>
        public virtual ICollection<TenderObject> Objects { get; set; }

        /// <summary>
        /// Дата создания извещения о проведении закупки
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Дата публикации извещения о проведении закупки
        /// </summary>
        public DateTime? Published { get; set; }

        /// <summary>
        /// Дата изменения извещения о проведении закупки
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Дата окончания подачи заявок
        /// </summary>
        public DateTime? CollectingEnd { get; set; }

        /// <summary>
        /// Дата рассмотрения и оценки заявок
        /// </summary>
        public DateTime? Scoring
        { get; set; }

        /// <summary>
        /// Дата и время проведения торгов
        /// </summary>
        public DateTime? Bidding
        { get; set; }

        /// <summary>
        /// Начальная цена
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Размер обеспечения заявки
        /// </summary>
        public decimal ApplicationGuarantee
        { get; set; }
        [DataType(DataType.Url)]
        public string UrlDkr { get; set; }
        [DataType(DataType.Url)]
        public string UrlEtp { get; set; }
    }
}
