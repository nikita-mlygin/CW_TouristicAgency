using System;
using System.Data;
using static Dapper.SqlMapper;

namespace NCourseWork.Persistence.Common.TypeHandlers
{
    internal class GuidTypeHandler : TypeHandler<Guid>
    {
        /// <inheritdoc/>
        public override Guid Parse(object value)
        {
            if (value is Guid guid)
            {
                return guid;
            }

            throw new DataException("Unable to convert database value to Guid.");
        }

        /// <inheritdoc/>
        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToByteArray();
        }
    }
}
