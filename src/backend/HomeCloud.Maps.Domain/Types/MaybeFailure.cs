using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Domain.Types
{
    public class MaybeFailure : OneOfBase<Successful, Failure>
    {
        protected MaybeFailure(OneOf<Successful, Failure> input)
            : base(input)
        {
        }

        public static implicit operator MaybeFailure(Successful _) => new MaybeFailure(_);
        public static implicit operator MaybeFailure(Failure _) => new MaybeFailure(_);
    }
}
