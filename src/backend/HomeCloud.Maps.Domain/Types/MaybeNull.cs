using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OneOf.Types.TrueFalseOrNull;

namespace HomeCloud.Maps.Domain.Types
{
    public class MaybeNull<TItem> : OneOfBase<TItem, Null> where TItem : class
    {
        protected MaybeNull(OneOf<TItem, Null> input) 
            : base(input)
        {
        }

        public static implicit operator MaybeNull<TItem>(TItem _) => new MaybeNull<TItem>(_);
        public static implicit operator MaybeNull<TItem>(Null _) => new MaybeNull<TItem>(_);

        public new TItem Value => IsT0 ? AsT0 : null;
    }
}
