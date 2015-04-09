﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asn_23.Models
{
    public partial class RiskStatus
    {
        public virtual int RiskStatusId { get; set; }

        [MaxLength(10)]
        public virtual string Status { get; set; }

        public virtual IEnumerable<Clients> Clients { get; set; }
    }
}