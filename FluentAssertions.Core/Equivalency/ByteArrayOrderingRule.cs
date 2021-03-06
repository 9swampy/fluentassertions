using System.Collections.Generic;

using FluentAssertions.Common;

namespace FluentAssertions.Equivalency
{
    /// <summary>
    /// Ordering rule that ensures that byte arrays are always compared in strict ordering since it would cause a 
    /// severe performance impact otherwise.
    /// </summary>
    internal class ByteArrayOrderingRule : IOrderingRule
    {
        public bool AppliesTo(ISubjectInfo subjectInfo)
        {
            return subjectInfo.CompileTimeType.Implements<IEnumerable<byte>>();
        }

        public override string ToString()
        {
            return "Be strict about the order of items in byte arrays";
        }
    }
}