using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ndeal.SharedKernel;

public interface IDateTimeProver
{
    DateTime UtcNow { get; }
}
