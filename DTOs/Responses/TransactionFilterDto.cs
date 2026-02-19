using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApi.DTOs.Responses
{
    public class TransactionFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Guid?>? CategoriesIds { get; set; }
        public Guid? AccountId { get; set; }
        public List<Guid?>? SubCategoriesIds { get; set; } = [];
    }
}