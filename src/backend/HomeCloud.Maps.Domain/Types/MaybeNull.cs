using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OneOf.Types.TrueFalseOrNull;

namespace HomeCloud.Maps.Domain.Types
{
    public class MaybeNull<TItem> : OneOfBase<TItem, Null>
    {
        protected MaybeNull(OneOf<TItem, Null> input) 
            : base(input)
        {
        }

        public static implicit operator MaybeNull<TItem>(TItem _) => new MaybeNull<TItem>(_);
        public static implicit operator MaybeNull<TItem>(Null _) => new MaybeNull<TItem>(_);

        public new TItem Value => AsT0;

        public bool IsNull => IsT1;

        public MaybeNull<TResult> Match<TResult>(Func<TItem, MaybeNull<TResult>> func)
            => Match(func, x => x);

        public override string ToString()
        {
            return IsNull ? null : Value.ToString();
        }
    }

    public class MaybeNull
    {
        public static MaybeNull<T> Create<T>(T item) where T : class
            => item is null ? new Null() : item;

        public static MaybeNull<(T1 X, T2 Y)> Merge<T1, T2>(MaybeNull<T1> t1, MaybeNull<T2> t2)
            => t1.Match(
                x => t2.Match<MaybeNull<(T1, T2)>>(y => (x, y), y => new Null()),
                x => new Null());

        public static TMaybe Create<TMaybe, TItem>(TItem item, Func<TItem, TMaybe> create,
            Func<Null, TMaybe> createNull)
            where TMaybe : MaybeNull<TItem>
            where TItem : class
            => Create(item).Match(x => create(x), x => createNull(x));

        public static TMerged Merge<TMerged, T1, T2>(MaybeNull<T1> t1, MaybeNull<T2> t2,
            Func<(T1, T2), TMerged> create, Func<Null, TMerged> createNull)
            => Merge(t1, t2).Match(x => create(x), x => createNull(x));
    }
}
