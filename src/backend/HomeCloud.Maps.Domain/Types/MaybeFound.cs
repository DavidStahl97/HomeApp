using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OneOf.Types.TrueFalseOrNull;

namespace HomeCloud.Maps.Domain.Types
{
    public class MaybeFound<TItem> : MaybeNull<TItem>
    {
        protected MaybeFound(OneOf<TItem, Null> input)
            : base(input)
        {
        }

        public static implicit operator MaybeFound<TItem>(TItem _) => new MaybeFound<TItem>(_);
        public static implicit operator MaybeFound<TItem>(Null _) => new MaybeFound<TItem>(_);
    }

    public static class NotFound
    {
        public static MaybeFound<T> Create<T>(T item) where T : class
            => MaybeNull.Create<MaybeFound<T>, T>(item, x => x, x => x);

        public static MaybeFound<(T1 X, T2 Y)> Merge<T1, T2>(MaybeFound<T1> t1, MaybeFound<T2> t2)
            => MaybeNull.Merge<MaybeFound<(T1, T2)>, T1, T2>(t1, t2, x => x, x => x);
    }
}
